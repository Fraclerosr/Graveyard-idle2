using BabyStack.Model;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModificationPresenter<T1, T2> : MonoBehaviour, IModificationPlacement where T1 : Modification<T2>, new()
{
    [SerializeField] private string _placement = "RewardUpgrade";
    [SerializeField] private ModificationView _view;
    [SerializeField] private MoneyHolder _moneyHolder;

    private readonly List<IModificationListener<T2>> _listeners = new List<IModificationListener<T2>>();
    private Modification<T2> _modification;

    public event Action<Type, int> Upgrading;

    public virtual string AdditionalID => string.Empty;
    
    private void OnEnable()
    {
        if (AdditionalID != string.Empty)
            _modification = Activator.CreateInstance(typeof(T1), AdditionalID) as T1;
        else
            _modification = new T1();
        
        _modification.Load();

        UpdateListeners(_modification.CurrentModificationValue);

        _view.TryBuy += OnTryBuy;
        _view.Opened += OnViewOpened;
        _moneyHolder.BalanceChanged += OnBalanceChanged;
        

        Enabled();
    }

    private void OnDisable()
    {
        _view.TryBuy -= OnTryBuy;
        _view.Opened -= OnViewOpened;
        _moneyHolder.BalanceChanged -= OnBalanceChanged;

        Disabled();
    }

    private void Start()
    {
        BeforeStart();

        UpdateView();
        UpdateListeners(_modification.CurrentModificationValue);
    }

    protected void AddListener(IModificationListener<T2> listener)
    {
        _listeners.Add(listener);
        listener.OnModificationUpdate(_modification.CurrentModificationValue);
    }


    private void OnViewOpened()
    {
        UpdateView();
    }

    private void OnTryBuy()
    {
        if (_modification.TryGetNextModification(out ModificationData<T2> next))
        {
            if (_moneyHolder.Value >= next.Price)
            {
                _moneyHolder.SpendMoney(next.Price);
                UpgradeModification(next.Price);
            }
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    private void UpgradeModification(int price)
    {
        Upgrading?.Invoke(typeof(T1), price);

        _modification.Upgrade();
        _modification.Save();

        UpdateView();
        UpdateListeners(_modification.CurrentModificationValue);
    }

    private void UpdateView()
    {
        if (_modification.TryGetNextModification(out ModificationData<T2> next))
        {
            _view.Render(next, _modification.CurrentModificationLevel);
            if (_moneyHolder.Value < next.Price)
                _view.RenderLocked();
        }
        else
        {
            _view.RenderCompleted(_modification.CurrentModificationLevel);
        }
    }

    private void UpdateListeners(T2 value)
    {
        foreach (var listener in _listeners)
            listener.OnModificationUpdate(value);
    }

    private void OnBalanceChanged(int balance)
    {
        UpdateView();
    }

    protected virtual void BeforeStart() { }
    protected virtual void Enabled() { }
    protected virtual void Disabled() { }
}
