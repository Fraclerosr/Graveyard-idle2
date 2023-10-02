using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private MoneyHolder _playerMoney;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _playerMoney.BalanceChanged += OnBalanceChanged;
    }

    private void OnDisable()
    {
        _playerMoney.BalanceChanged -= OnBalanceChanged;
    }

    private void Start()
    {
        _text.text = _playerMoney.Value.ToString();
    }

    private void OnBalanceChanged(int balance)
    {
        _text.text = balance.ToString();
    }
}
