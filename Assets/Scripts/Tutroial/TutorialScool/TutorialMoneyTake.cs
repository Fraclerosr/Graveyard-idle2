using System;
using UnityEngine;

public class TutorialMoneyTake : MonoBehaviour, ITutorialAnalyticsEventSource, ITutorialObjectEventSource
{
    [SerializeField] private MoneyCollector _moneyCollector;
    [SerializeField] private int _targetMoneyAmount;
    [SerializeField] private Transform _pointPosition;


    private TutorialCondition _collectedTargetAmount;
    private int _collectedMoney;

    public event Action<string> EventSended;

    public Transform Point => _pointPosition;
    public ITutorialStepCondition CollectedTargetAmount => _collectedTargetAmount;

    private void Awake()
    {
        _collectedTargetAmount = new TutorialCondition();
    }

    public void Enable()
    {
        _moneyCollector.Collected += OnCollected;
    }

    public void Disable()
    {
        _moneyCollector.Collected -= OnCollected;
    }

    private void OnCollected(int value)
    {
        _collectedMoney += value;

        if (_collectedMoney >= _targetMoneyAmount)
        {
            _collectedTargetAmount.Complete();
            EventSended?.Invoke("collected_money");
            _moneyCollector.Collected -= OnCollected;
        }
    }
}
