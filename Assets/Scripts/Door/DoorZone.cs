using System;
using System.Collections;
using UnityEngine;

public class DoorZone : Trigger<DoorOpener>
{
    private const string Open = nameof(Open);
    private const string Close = nameof(Close);

    [SerializeField] private Animator _animator;
    [SerializeField] private float _delayBeforeClose = 1.5f;

    private bool _isOpen;
    private Coroutine _waitClose;

    public virtual bool CanOpen => true;
    protected override int Layer => LayerMask.NameToLayer("Default");

    protected override void OnEnter(DoorOpener triggered)
    {
        if (CanOpen == false)
            return;

        if (_waitClose != null)
            StopCoroutine(_waitClose);

        if (_isOpen)
            return;

        _isOpen = true;
        _animator.SetTrigger(Open);
    }

    protected override void OnStay(DoorOpener triggered)
    {
        if (CanOpen && _isOpen == false)
            OnEnter(triggered);
    }

    protected override void OnExit(DoorOpener triggered)
    {
        if (_waitClose != null)
            StopCoroutine(_waitClose);

        _waitClose = StartCoroutine(WaitBefore(_delayBeforeClose, () =>
        {
            if (_isOpen == false)
                return;

            _isOpen = false;
            _animator.SetTrigger(Close);
        }));
    }

    private IEnumerator WaitBefore(float duration, Action action)
    {
        yield return new WaitForSeconds(duration);

        action?.Invoke();
    }
}
