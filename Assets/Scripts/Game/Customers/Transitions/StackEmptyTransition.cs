using System;
using UnityEngine;

public class StackEmptyTransition : Transition
{
    [SerializeField] private StackPresenter _stack;

    private void Update()
    {
        NeedTransit = _stack.Count == 0;
    }
}