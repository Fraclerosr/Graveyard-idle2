using UnityEngine;
using System.Collections;

public class MoneyMagnetHaptics : BaseHaptics
{
    [SerializeField] private MoneyMagnit _moneyMagnet;

    private void OnEnable()
    {
        _moneyMagnet.Attracted += OnAttracted;
    }

    private void OnDisable()
    {
        _moneyMagnet.Attracted -= OnAttracted;
    }

    private void OnAttracted(int count)
    {
        Vibrate();
    }
}
