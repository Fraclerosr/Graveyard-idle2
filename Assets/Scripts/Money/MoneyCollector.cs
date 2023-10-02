using System;
using UnityEngine;

public class MoneyCollector : MonoBehaviour
{
    [SerializeField] private MoneyMagnit _magnit;
    [SerializeField] private MoneyHolder _moneyHolder;
    [SerializeField] private Trigger<DroppableDollar> _trigger;
    [SerializeField] private Trigger<MoneyZone> _moneyZoneTrigger;

    public event Action<int> Collected;


    private void OnEnable()
    {
        _magnit.Attracted += OnDollarAttracted;
        _trigger.Stay += OnStay;
        _moneyZoneTrigger.Stay += OnStay;
    }

    private void OnDisable()
    {
        _magnit.Attracted -= OnDollarAttracted;
        _trigger.Stay -= OnStay;
        _moneyZoneTrigger.Stay -= OnStay;
    }

    private void OnStay(DroppableDollar droppableDollar)
    {
        if (droppableDollar.CanTake == false)
            return;

        Dollar dollar = droppableDollar.Take();
        _magnit.Attract(dollar);
    }

    private void OnStay(MoneyZone moneyZone)
    {
        if (moneyZone.Dollars == 0)
            return;

        for (int i = 0; i < 5; i++)
        {
            var dollar = moneyZone.Remove();
            _magnit.Attract(dollar);

            if (moneyZone.Dollars == 0)
                break;
        }
    }

    private void OnDollarAttracted(int dollarValue)
    {
        _moneyHolder.AddMoney(dollarValue);
        Collected?.Invoke(dollarValue);
    }
}