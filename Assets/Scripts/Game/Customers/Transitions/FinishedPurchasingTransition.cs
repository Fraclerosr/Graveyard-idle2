using System;
using UnityEngine;

public class FinishedPurchasingTransition : Transition
{
    [SerializeField] private Customer _customer;
    
    protected override void Enable()
    {
        _customer.PurchaseList.BecameEmpty += OnBecameEmpty;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void OnBecameEmpty()
    {
        Unsubscribe();
        NeedTransit = true;
    }

    private void Unsubscribe()
    {
        _customer.PurchaseList.BecameEmpty -= OnBecameEmpty;
    }
}