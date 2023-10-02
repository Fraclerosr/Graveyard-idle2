using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GraveEarth : MonoBehaviour
{
    private Animator _animator;
    private int _durationFactor = 5;

    private void Awake() => 
        _animator = GetComponent<Animator>();

    private IEnumerator Buring()
    {
        _animator.SetBool("isDigUp", false);
        _animator.SetBool("isBuring", true);
        yield return new WaitForSeconds(5);
        _animator.SetBool("isBuring", false);
    }

    public void DigIn(float interactionTime)
    {
        SetAnimatorSpeed(interactionTime);
        _animator.SetTrigger("isBuring");
    }

    public void DigUp(float interactionTime)
    {
        SetAnimatorSpeed(interactionTime);
        _animator.SetTrigger("isDigUp");
    }

    private void SetAnimatorSpeed(float interactionTime) => 
        _animator.speed = _durationFactor / interactionTime;
}
