using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BabyStack.Model;
using System;
using System.Linq;

public class StackPresenter : MonoBehaviour, IModificationListener<int>
{
    [SerializeField] private StackView _stackView;
    [SerializeField] private StackUIView _stackUIView;
    [SerializeField] private int _stackCapacity;
    [SerializeField] private List<StackableTypes> _allTypesThatCanBeAdded;

    private StackStorage _stack;

    public event UnityAction<Stackable> Added;
    public event UnityAction<Stackable> Removed;
    public event UnityAction BecameEmpty;

    public event Action SetInputType;

    public IEnumerable<Stackable> Data => _stack.Data;
    public bool IsFull => _stack.Count == _stack.Capacity;
    public int Count => _stack.Count;
    public int Capacity => _stack.Capacity;

    private void Awake()
    {
        _stack = new StackStorage(_stackCapacity, _allTypesThatCanBeAdded);
    }

    private void OnEnable()
    {
        _stackView.MoveEnded += OnStackableMoveEnded;
    }

    private void OnDisable()
    {
        _stackView.MoveEnded -= OnStackableMoveEnded;
    }

    private void Start()
    {
        _stackUIView?.Init(_stack, _stackView);
    }

    public bool CanAddToStack(StackableType stackableType)
    {
        return _stack.CanAdd(stackableType);
    }

    public void AddToStack(Stackable stackable)
    {
        if (CanAddToStack(stackable.Type) == false)
            throw new InvalidOperationException();

        _stack.Add(stackable);
        _stackView.Add(stackable);
    }

    public bool CanRemoveFromStack(StackableType stackableType)
    {
        return _stack.Contains(stackableType);
    }

    public IEnumerable<Stackable> RemoveAll()
    {
        var data = _stack.Data.ToArray();
        foreach (var stackable in data)
            RemoveFromStack(stackable);
        
        return data;
    }

    public void RemoveFromStack(Stackable stackable)
    {
        _stack.Remove(stackable);
        _stackView.Remove(stackable);
        Removed?.Invoke(stackable);

        if (_stack.Count == 0)
            BecameEmpty?.Invoke();
    }

    public Stackable RemoveFromStack(StackableType stackableType)
    {
        if (CanRemoveFromStack(stackableType) == false)
            throw new InvalidOperationException();

        var lastStackable = _stack.FindLast(stackableType);

        _stack.Remove(lastStackable);
        _stackView.Remove(lastStackable);
        Removed?.Invoke(lastStackable);

        if (_stack.Count == 0)
            BecameEmpty?.Invoke();

        return lastStackable;
    }

    public void ChangeCapacity(int value)
    {
        _stack.ChangeCapacity(value);
    } 

    public int CalculateCount(StackableType stackableType)
    {
        return _stack.CalculateCount(stackableType);
    }

    public int CalculateCount(StackableType[] stackableType)
    {
        var count = 0;
        foreach (var type in stackableType)
            count += _stack.CalculateCount(type);

        return count;
    }

    private void OnStackableMoveEnded(Stackable stackable)
    {
        Added?.Invoke(stackable);
        SetInputType?.Invoke();
    }

    public void OnModificationUpdate(int value)
    {
        ChangeCapacity(value);
    }
}
