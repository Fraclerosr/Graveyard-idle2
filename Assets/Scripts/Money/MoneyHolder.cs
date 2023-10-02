using BabyStack.Model;
using UnityEngine;
using UnityEngine.Events;

public class MoneyHolder : MonoBehaviour
{
    [SerializeField] private IntProgress _totalMoneyProgress;

    private MoneyBalance _money;

    public event UnityAction<int> BalanceChanged;

    public int Value => _money.Value;
    public bool HasMoney => _money.Value > 0;

    private void OnEnable()
    {
        _money = new MoneyBalance();
        _money.Load();

        _money.Changed += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _money.Changed -= OnMoneyChanged;
        _money.Save();
    }

    public void AddMoney(int value)
    {
        _money.Add(value);

        _totalMoneyProgress.Add(value);
        _totalMoneyProgress.Save();
    }

    public void SpendMoney(int value)
    {
        _money.Spend(value);
    }

    private void OnMoneyChanged()
    {
        BalanceChanged?.Invoke(_money.Value);
        _money.Save();
    }
}
