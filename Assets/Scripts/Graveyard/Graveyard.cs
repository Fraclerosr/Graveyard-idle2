using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Graveyard : MonoBehaviour, IIntentionSource
{
    private const int ClearDelay = 3;
    
    [SerializeField] private Animator _leverAnimator;
    
    private Animator _animator;

    public event Action<IntentionType> IntentionUpdated;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        IntentionUpdated?.Invoke(IntentionType.NullIntention);
    }

    private IEnumerator UpdateGrave()
    {
        yield return new WaitForSeconds(ClearDelay);
        _animator.SetBool("isUpdateButtonClick", true);
        yield return new WaitForSeconds(4);
        _animator.SetBool("isUpdateButtonClick", false);
    }

    private IEnumerator LeverUp()
    {
        _leverAnimator.SetBool("isClear", true);
        yield return new WaitForSeconds(1);
        _leverAnimator.SetBool("isClear", false);
    }

    public void SetLeverEffects(bool active) => 
        IntentionUpdated?.Invoke(active ? IntentionType.Available : IntentionType.NullIntention);

    public void UpdatingGraves()
    {
        StartCoroutine(LeverUp());
        //StartCoroutine(UpdateGrave());
        SetLeverEffects(false);
    }
}
