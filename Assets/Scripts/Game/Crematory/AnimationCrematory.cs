using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationCrematory : MonoBehaviour
{
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem _fire;

    private Animator _animator;
    private const string _input = "Input";

    private void OnEnable()
    {
        _stackPresenter.SetInputType += SetInputType;
    }

    private void OnDisable()
    {
        _stackPresenter.SetInputType -= SetInputType;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private IEnumerator Input()
    {
        _smoke.Play();
        _fire.Play();
        _animator.SetBool(_input, true);
        yield return new WaitForSeconds(1);
        _animator.SetBool(_input, false);
        _smoke.Stop();
        _fire.Stop();
    }

    public void SetInputType()
    {
        StartCoroutine(Input());
    }
}
