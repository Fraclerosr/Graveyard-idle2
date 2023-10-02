using System;
using System.Collections;
using UnityEngine;

public class Grave : LockedGameObject
{
    private const float DelayBeforeBury = 2.0f;

    [SerializeField] private GameObject _dottedSquare;
    [SerializeField] private GraveEarth _earth;
    [SerializeField] private StackPresenter _graveStack;
    [SerializeField] private StackPresenter _stoneStack;
    [SerializeField] private GameObject _stackTriggers;
    [SerializeField] private CreatureRevive _creatureRevive;
    [field: SerializeField] public Transform PlacePoint { get; private set; }
    [field: SerializeField] public Transform ServePoint { get; private set; }

    private Coroutine _interaction;
    private CharacterReferences _characterReferences;
    private bool CanDig => !HasCoffin && !Dug;
    private bool CanBury => HasCoffin && Dug;

    public event Action<Grave> BecameEmpty;
    public event Action<Grave> Updated;

    public bool Dug { get; private set; }
    public bool HasCoffin => _graveStack.Count != 0;
    public bool CanInteract => _interaction == null && (CanBury);

    public void Init(CharacterReferences characterReferences)
    {
        _characterReferences = characterReferences;
    }

    private void OnEnable() => 
        _graveStack.Added += OnCoffinAdded;

    public void Start()
    {
        StartCoroutine(DiggingGround(2));
    }

    private void OnDisable() => 
        _graveStack.Added += OnCoffinAdded;

    public void ClearGrave(Urn urn) =>
        StartCoroutine(Clear(urn));

    public void StartInteraction(float interactionTime)
    {
        if (CanBury)
            _interaction = StartCoroutine(Bury(interactionTime));
    }

    public void StopInteraction() =>
        _interaction = null;

    private void OnCoffinAdded(Stackable _) =>
        _dottedSquare.SetActive(Dug && CanInteract);

    private IEnumerator DiggingGround(float interactionTime)
    {
        _earth.DigUp(interactionTime);
        SetDug(true);

        yield return new WaitForSeconds(interactionTime);

        BecameEmpty?.Invoke(this);
    }

    private IEnumerator Bury(float interactionTime)
    {
        SetDug(false);
        _earth.DigIn(interactionTime);
        yield return new WaitForSeconds(interactionTime);
        Updated?.Invoke(this);
    }

    private IEnumerator Clear(Urn urn)
    {
        yield return null;

        _stoneStack.Clear();
        _graveStack.Clear();
        _creatureRevive.Init(_characterReferences, urn.RevivedCustomer);
        yield return StartCoroutine(_creatureRevive.Revive());
        SetDug(true);

        _dottedSquare.SetActive(false);
    }

    private void SetDug(bool active)
    {
        Dug = active;
        _stackTriggers.SetActive(active);
        _dottedSquare.SetActive(Dug && CanInteract);
    }
}