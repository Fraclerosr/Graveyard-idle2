using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravesContainer : MonoBehaviour
{
    [SerializeField] private Graveyard _graveyard;
    [SerializeField] private IntProgress _gravesDugProgress;
    [SerializeField] private CharacterReferences _characterReferences;
    [SerializeField] private StackPresenter _urnContainer;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private Animator _urnAnimator;

    private List<Grave> _graves;
    private Coroutine _clear;

    public event Action<Grave> BecameEmpty;
    public event Action Cleared;

    public bool HasEmptyGrave => _graves.Any(GraveEmpty);

    private void Awake()
    {
        _graves = GetComponentsInChildren<Grave>().ToList();
        
        foreach (Grave grave in _graves) 
            grave.Init(_characterReferences);
    }

    private void OnEnable()
    {
        foreach (Grave grave in _graves)
        {
            grave.BecameEmpty += OnGraveBecameEmpty;
            grave.Updated += OnGraveUpdated;
        }
    }

    private void OnDisable()
    {
        foreach (Grave grave in _graves)
        {
            grave.BecameEmpty -= OnGraveBecameEmpty;
            grave.Updated -= OnGraveUpdated;
        }
    }

    public void TryClear()
    {
        if(!AllGraves(GraveBuried) || _urnContainer.Count == 0 || _clear != null)
            return;

        Cleared?.Invoke();
        _clear = StartCoroutine(UrnEffect());
        _graveyard.UpdatingGraves();
    }

    public Grave TakeEmptyGrave()
    {
        if (HasEmptyGrave == false)
            throw new InvalidOperationException();

        return _graves.First(GraveEmpty);
    }

    public bool HasGrave(Func<Grave, bool> func) => 
        _graves.Any(func);

    public bool AllGraves(Func<Grave, bool> func) => 
        _graves.All(func);

    public Grave TakeGrave(Func<Grave, bool> func) =>
        _graves.First(func);

    public void Add(Grave reference)
    {
        _graves.Add(reference);
        reference.BecameEmpty += OnGraveBecameEmpty;
        reference.Updated += OnGraveUpdated;
        OnGraveUpdated(reference);
    }

    private void OnGraveUpdated(Grave grave) => 
        _graveyard.SetLeverEffects(_graves.All(GraveBuried));

    private bool GraveEmpty(Grave grave) => 
        !grave.HasCoffin && grave.Dug && grave.Locked == null;
    
    private bool GraveBuried(Grave grave) => 
        grave.HasCoffin && !grave.Dug;

    private void OnGraveBecameEmpty(Grave grave)
    {
        _gravesDugProgress.Add();
        _gravesDugProgress.Save();
        BecameEmpty?.Invoke(grave);
    }

    private IEnumerator UrnEffect()
    {
        _urnAnimator.SetBool("isUp", true);
        yield return new WaitForSeconds(1);
        _urnAnimator.SetBool("isUp", false);

        Urn urn = (Urn)_urnContainer.RemoveFromStack(StackableType.Urn);

        foreach (Grave grave in _graves)
            grave.ClearGrave(urn);

        _smoke.Play();
        Destroy(urn.gameObject);
        _clear = null;
    }
}