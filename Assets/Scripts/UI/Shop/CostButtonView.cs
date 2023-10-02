using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostButtonView : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _price;

    private int _cost;

    public event Action Clicked;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
    }

    public void Init(int cost, int balance)
    {
        _cost = cost;
        DisplayForBalance(balance);
    }

    public void DisplayForBalance(int balance)
    {
        _price.text = _cost.ToString();
        _buyButton.interactable = balance >= _cost;
    }

    private void OnBuyButtonClicked()
    {
        Clicked?.Invoke();
    }
}