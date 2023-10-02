using System;
using UnityEngine;

public class TutorialMoneyZone : MonoBehaviour, ITutorialStepCondition, ITutorialAnalyticsEventSource, ITutorialObjectEventSource
{
    [SerializeField] private int _targetMoney = 20;
    [SerializeField] private MoneyZone _moneyZone;
    [SerializeField] private MoneyHolder _moneyHolder;

    private bool _moneyCollected;

    public event Action<string> EventSended;

    [field: SerializeField] public Transform Point { get; private set; }
    public bool Completed { get; private set; }
    public GameObject GameObject => _moneyZone.gameObject;
    public string CameraTrigger => CameraAnimatorParameters.ShowMoneyZone;
    public ITutorialStepCondition Condition => this;

    public void Enable()
    {
        _moneyZone.Removed += OnMoneyZoneRemoved;
        _moneyHolder.BalanceChanged += OnBalanceChanged;
    }

    public void Disable()
    {
        _moneyZone.Removed -= OnMoneyZoneRemoved;
        _moneyHolder.BalanceChanged -= OnBalanceChanged;
    }

    private void OnMoneyZoneRemoved()
    {
        _moneyCollected = true;

        if (_moneyZone.Dollars == 0)
            EventSended?.Invoke("money_collect");
    }

    private void OnBalanceChanged(int balance)
    {
        if (_moneyCollected && balance >= _targetMoney)
            Completed = true;
    }
}