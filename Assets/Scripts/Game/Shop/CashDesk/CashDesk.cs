using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class CashDesk : LockedGameObject
{
    private const float NewCustomerSetDelay = 1.0f;

    [SerializeField] private CustomerTrigger _customerTrigger;
    [SerializeField] private PurchaseListView _purchaseListView;
    [SerializeField] private MoneyZone _moneyZone;
    [SerializeField] private Transform _servePoint;
    [SerializeField] private Transform _customerWaitPoint;
    [SerializeField] private StackPresenter _tombstoneStack;
    [SerializeField] private List<StackPresenter> _coffinStacks;

    private Customer _activeCustomer;

    public event Action BecameFree;

    public bool Active { get; private set; }
    public bool Free { get; private set; } = true;
    public IEnumerable<StackableType> PurchaseTypes => _activeCustomer.PurchaseList.Items.Keys;
    public PurchaseList PurchaseList => _activeCustomer.PurchaseList;
    public Transform ServePoint => _servePoint;

    private void OnEnable() =>
        _customerTrigger.Enter += OnCustomerEnter;

    private void Start() =>
        SetActive(false);

    private void OnDisable() =>
        _customerTrigger.Enter -= OnCustomerEnter;

    private void OnCustomerEnter(Customer customer)
    {
        if (customer != _activeCustomer)
            return;

        StartCoroutine(NewCustomerActivationDelay());
        _purchaseListView.Init(_activeCustomer.PurchaseList);
    }

    private IEnumerator NewCustomerActivationDelay()
    {
        yield return new WaitForSeconds(NewCustomerSetDelay);
        SetActive(true);
    }

    public Transform Take(Customer customer)
    {
        _activeCustomer = customer;
        Free = false;
        return _customerWaitPoint;
    }

    public void Leave()
    {
        SetActive(false);
        Free = true;
        BecameFree?.Invoke();
    }

    public void Deliver(StackableType type)
    {
        if (Active == false)
            throw new InvalidOperationException();

        _activeCustomer.PurchaseList.Remove(type);
        _purchaseListView.UpdateView();
    }

    public IEnumerable<Stackable> TakeAllItems()
    {
        IEnumerable<Stackable> stackables = new List<Stackable>();
        stackables = _coffinStacks.Aggregate(stackables, (current, stack) => current.Concat(stack.RemoveAll()));
        stackables = stackables.Concat(_tombstoneStack.RemoveAll());

        _activeCustomer.Pay(_moneyZone, _activeCustomer.PurchaseList.TotalPrice);
        _moneyZone.SetActive(false);
        StartCoroutine(MoneyZoneActivationDelay());

        return stackables;
    }

    public void Place(IEnumerable<Stackable> stackData)
    {
        for (int i = 0; i < stackData.Count(); i++)
            _coffinStacks[i].AddToStack(stackData.ElementAt(i));
    }

    private void SetActive(bool active) =>
        Active = active;

    private IEnumerator MoneyZoneActivationDelay()
    {
        yield return new WaitForSeconds(NewCustomerSetDelay);
        _moneyZone.SetActive(true);
    }
}