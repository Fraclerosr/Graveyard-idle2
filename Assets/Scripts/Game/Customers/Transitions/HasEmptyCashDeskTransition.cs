using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class HasEmptyCashDeskTransition : Transition
{
    [SerializeField] private Customer _customer;
    
    protected override void Enable()
    {
        NeedTransit = CanTransit();
        
        foreach (CashDesk desk in _customer.References.CashDesks.Data) 
            desk.BecameFree += OnBecameEmpty;

        _customer.References.CashDesks.Unlocked += OnDeskUnlocked;
    }

    private bool CanTransit() => 
        _customer.References.CashDesks.Data.Any(desk => desk.Free);

    private void OnDeskUnlocked(CashDesk desk, bool _, string guid)
    {
        StartCoroutine(Transit());
    }

    private IEnumerator Transit()
    {
        yield return new WaitForSeconds(2);
        
        if(CanTransit())
            OnBecameEmpty();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void OnBecameEmpty()
    {
        if (_customer.References.ShopQueues.Queues.Any(queue => queue.Peek() != _customer))
            return;

        Unsubscribe();
        NeedTransit = true;
    }

    private void Unsubscribe()
    {
        foreach (CashDesk desk in _customer.References.CashDesks.Data) 
            desk.BecameFree -= OnBecameEmpty;
        
        _customer.References.CashDesks.Unlocked -= OnDeskUnlocked;
    }
}