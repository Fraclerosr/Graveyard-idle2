using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Interactions : MonoBehaviour
{
    [SerializeField] private TimerStackableProducer _stackProducer;
    [SerializeField] private ParticleSystem _chopEffect;

    private Animator _animator;

    private void OnEnable()
    {
        _stackProducer.StackedInteraction += Chop;
    }

    private void OnDisable()
    {
        _stackProducer.StackedInteraction -= Chop;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private IEnumerator Aniation()
    {
        _animator.SetBool("isChop", true);
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("isChop", false);
    }

    public void Chop()
    {
        StartCoroutine(Aniation());
        _chopEffect.Play();
    }
}
