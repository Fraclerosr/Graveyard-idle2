using System;
using System.Collections;
using UnityEngine;

public class RevivedCustomer : AICharacter
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int ReviveHash = Animator.StringToHash("Revive");
    private static readonly int Disappear = Animator.StringToHash("Disappear");
    
    [SerializeField] private MoneyPayer _moneyPayer;
    [SerializeField] private Animator _animator;
    [SerializeField] protected GameObject Model;
    [SerializeField] private float _runDelay;

    protected GraveEarth GraveEarth;
    private Animator _coffinAnimator;
    private CharacterReferences _references;

    public event Action<RevivedCustomer> Left;

    public void Pay(MoneyZone moneyZone, int totalPrice)
    {
        if(gameObject.activeInHierarchy)
            _moneyPayer.Pay(moneyZone, totalPrice);
    }

    public void Leave() => 
        Left?.Invoke(this);

    public void Init(GraveEarth graveEarth, Animator coffinAnimator, CharacterReferences references)
    {
        _references = references;
        _coffinAnimator = coffinAnimator;
        GraveEarth = graveEarth;
    }

    public IEnumerator StartRevive()
    {
        yield return StartCoroutine(Revive());
    }

    private IEnumerator Revive()
    {
        Model.gameObject.SetActive(false);
        yield return DigCoffin();
        _animator.SetTrigger(ReviveHash);
        yield return new WaitForSeconds(_runDelay);
        Init(_references);
        yield return OnRun();
    }

    protected virtual IEnumerator OnRun()
    {
        yield return null;
        Run();
        _coffinAnimator.SetTrigger(Disappear);
    }

    protected virtual IEnumerator DigCoffin()
    {
        GraveEarth.DigUp(3);
        _coffinAnimator.gameObject.SetActive(true);
        _coffinAnimator.SetTrigger(Idle);
        yield return new WaitForSeconds(2f);
        _coffinAnimator.SetTrigger(Open);
        yield return new WaitForSeconds(1f);
        Model.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
    }
}