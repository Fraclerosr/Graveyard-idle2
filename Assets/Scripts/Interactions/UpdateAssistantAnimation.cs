using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UpdateAssistantAnimation : MonoBehaviour
{
    [SerializeField] private StackPresenter _updateTable;

    private Animator _animator;

    private void OnEnable()
    {
        _updateTable.Added += OnAdded;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        _updateTable.Added -= OnAdded;
    }

    private IEnumerator Work()
    {
        _animator.SetBool("isWork", true);
        yield return new WaitForSeconds(2);
        _animator.SetBool("isWork", false);
    }

    private IEnumerator Working()
    {
        _animator.SetBool("isWork", true);

        yield return new WaitForSeconds(2);
        _animator.SetBool("isWork", false);
    }

    private void OnAdded(Stackable _)
    {
        StartCoroutine(Work());
    }
}
