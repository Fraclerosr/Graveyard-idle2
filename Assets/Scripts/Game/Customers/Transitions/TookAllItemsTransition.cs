using System;
using UnityEngine;

public class TookAllItemsTransition : Transition
{
    [SerializeField] private TakeCashDeskState _takeCashDeskState;
    
    protected override void Enable()
    {
        _takeCashDeskState.TookAllItems += OnTookAllItems;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void OnTookAllItems()
    {
        Unsubscribe();
        NeedTransit = true;
    }

    private void Unsubscribe()
    {
        _takeCashDeskState.TookAllItems -= OnTookAllItems;
    }
}