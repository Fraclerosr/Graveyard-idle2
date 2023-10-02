using System;
using System.Linq;
using UnityEngine;

public class HasEmptyGraveTransition : Transition
{
    [SerializeField] private Customer _customer;
    
    protected override void Enable()
    {
        if(_customer.References.GravesContainer.HasEmptyGrave) 
            NeedTransit = true;

        _customer.References.GravesContainer.BecameEmpty += OnBecameEmpty;
    }

    private void OnDisable() => 
        Unsubscribe();

    private void OnBecameEmpty(Grave grave)
    {
        if (_customer.References.GraveyardQueue.Peek() != _customer)
            return;

        Unsubscribe();
        NeedTransit = true;
    }

    private void Unsubscribe() => 
        _customer.References.GravesContainer.BecameEmpty -= OnBecameEmpty;
}