using System;
using System.Collections;
using UnityEngine;

public class DigGravesState : State
{
    [SerializeField] private Assistant _parent;
    [SerializeField] private AIMovement _movement;
    [SerializeField] private Collider _selfCollider;
    [SerializeField] private Gravedigger _gravedigger;

    private Coroutine _process;

    private void OnEnable()
    {
        _selfCollider.enabled = false;
        _process = StartCoroutine(Process());
    }

    private void OnDisable() =>
        StopCoroutine(_process);

    private IEnumerator Process()
    {
        while (true)
        {
            yield return new WaitUntil(() => _parent.References.GravesContainer.HasGrave(CanInteract()));

            if(!_parent.References.GravesContainer.HasGrave(CanInteract()))
                continue;
            
            Grave grave = _parent.References.GravesContainer.TakeGrave(CanInteract());

            grave.Lock(_parent);
            _movement.Move(grave.ServePoint.position).OnComplete(() =>
            {
                _selfCollider.enabled = true;
                _movement.Look(grave.ServePoint.forward);
            });

            yield return new WaitUntil(() => _selfCollider.enabled);
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => !_gravedigger.Interacting);
            grave.Unlock();
            _selfCollider.enabled = false;
        }
    }

    private static Func<Grave, bool> CanInteract() =>
        grave => grave.CanInteract && grave.Locked == null;
}