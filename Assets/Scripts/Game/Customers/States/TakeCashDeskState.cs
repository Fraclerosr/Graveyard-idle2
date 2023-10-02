using System;
using System.Collections;
using UnityEngine;

public class TakeCashDeskState : State
{
    [SerializeField] private Customer _parent;
    [SerializeField] private AIMovement _movement;
    [SerializeField] private StackPresenter _selfStack;
    
    private CashDesk _desk;

    public event Action TookAllItems;
    public event Action<Customer> TookTable;
    
    private void OnEnable()
    {
        _movement.Enable();
        _desk = _parent.References.CashDesks.GetFreeDesk();
        Transform waitPoint = _desk.Take(_parent);

        _movement.Move(waitPoint.position).OnComplete(() =>
            {
                _movement.Look(waitPoint.forward);
                _desk.Place(_parent.Stack.RemoveAll());
                TookTable?.Invoke(_parent);
            }
        ); 
        
        _parent.PurchaseList.BecameEmpty += OnBecameEmpty;
    }

    private void OnDisable() => 
        Unsubscribe();

    private void OnBecameEmpty()
    {
        Unsubscribe();
        StartCoroutine(Leave());
    }

    private void Unsubscribe() => 
        _parent.PurchaseList.BecameEmpty -= OnBecameEmpty;

    private IEnumerator Leave()
    {
        yield return new WaitForSeconds(1.5f);

        foreach (Stackable stackable in _desk.TakeAllItems())
        {
            _selfStack.AddToStack(stackable);
            yield return new WaitForSeconds(0.25f);
        }

        TookAllItems?.Invoke();
        _desk.Leave();
    }
}