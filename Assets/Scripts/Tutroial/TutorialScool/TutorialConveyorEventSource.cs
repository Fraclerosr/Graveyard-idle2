using System;
using UnityEngine;

public class TutorialConveyorEventSource : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private StackPresenter _startStack;
    [SerializeField] private StackPresenter _endStack;

    public event Action InStartStackAdded;
    public event Action InEndStackAdded;
    public event Action FromEndStackRemoved;

    public Transform StartPoint => _startPoint;
    public Transform EndPoint => _endPoint;

    private void OnEnable()
    {
        _startStack.Added += OnStartAdded;
        _endStack.Added += OnEndAdded;
        _endStack.Removed += OnEndRemoved;
    }

    private void OnDisable()
    {
        _startStack.Added -= OnStartAdded;
        _endStack.Added -= OnEndAdded;
        _endStack.Removed -= OnEndRemoved;
    }

    private void OnStartAdded(Stackable stackable)
    {
        InStartStackAdded?.Invoke();
    }

    private void OnEndAdded(Stackable stackable)
    {
        InEndStackAdded?.Invoke();
    }

    private void OnEndRemoved(Stackable stackable)
    {
        FromEndStackRemoved?.Invoke();
    }
}
