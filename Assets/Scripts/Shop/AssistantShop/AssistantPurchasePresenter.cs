using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AssistantPurchasePresenter : MonoBehaviour
{
    [SerializeField] private string _placement = "RewardAssistant";
    [SerializeField] private AssistantShopData _assistants;
    [SerializeField] private MoneyHolderTrigger _trigger;
    [SerializeField] private MoneyHolder _moneyHolder;
    [SerializeField] private AssistantPurchaseView _purchaseView;

    private AssistantInventory _inventory;
    private AssistantData _purchasableAssistant;

    public event UnityAction<string> ShowRewardPlacement;
    public event UnityAction<AssistantData> Purchased;

    private void OnEnable()
    {
        _inventory = new AssistantInventory(_assistants);
        _inventory.Load();

        _purchaseView.OnTryBuy += OnBuyClicked;
        _purchaseView.OnFreeBuy += AddAssistant;

        _moneyHolder.BalanceChanged += OnBalanceChanged;
        _trigger.Enter += OnPlayerTriggerEnter;
        _trigger.Exit += OnPlayerTriggerExit;
    }

    private void Start()
    {
        if (TryDisplayFreeAssistant() == false)
            UpdatePurchasableAssistant();
        
        UpdateView();
    }

    private void OnDisable()
    {
        _purchaseView.OnTryBuy -= OnBuyClicked;
        _purchaseView.OnFreeBuy -= AddAssistant;

        _moneyHolder.BalanceChanged -= OnBalanceChanged;
        _trigger.Enter -= OnPlayerTriggerEnter;
        _trigger.Exit -= OnPlayerTriggerExit;
    }

    private void OnPlayerTriggerExit(MoneyHolder moneyHolder)
    {
        _purchaseView.Disable();
    }

    private void OnPlayerTriggerEnter(MoneyHolder moneyHolder)
    {
        ShowRewardPlacement?.Invoke(_placement);
        _purchaseView.Enable();
        UpdateView();
    }

    private bool TryDisplayFreeAssistant()
    {
        foreach (var assistantData in GetLockedAssistants())
        {
            if (assistantData.Price != 0)
                continue;

            _purchasableAssistant = assistantData;
            _purchaseView.DisplayFreeAssistant();

            return true;
        }

        return false;
    }

    private void UpdatePurchasableAssistant()
    {
        foreach (var assistantData in GetLockedAssistants())
        {
            _purchasableAssistant = assistantData;
            _purchaseView.Init(assistantData.Price, _moneyHolder.Value);
            _purchaseView.DisplayPaidAssistant();

            return;
        }
    }

    private IEnumerable<AssistantData> GetLockedAssistants()
    {
        foreach (var assistantData in _assistants.Data)
        {
            if (_inventory.Contains(assistantData) == false)
                yield return assistantData;
        }
    }

    private void OnBuyClicked()
    {
        if (_purchasableAssistant != null && _purchasableAssistant.Price > 0)
            _moneyHolder.SpendMoney(_purchasableAssistant.Price);

        AddAssistant();
    }

    private void AddAssistant()
    {
        _inventory.Add(_purchasableAssistant);
        _inventory.Save();
        Purchased?.Invoke(_purchasableAssistant);
        _purchasableAssistant = null;

        UpdatePurchasableAssistant();
        UpdateView();
    }

    private void OnBalanceChanged(int balance)
    {
        UpdateView();
    }

    private void UpdateView()
    {
        if (_purchasableAssistant == null)
        {
            _trigger.gameObject.SetActive(false);
            _purchaseView.gameObject.SetActive(false);
            
            return;
        }
        
        if (_purchasableAssistant.Price == 0)
            return;

        _purchaseView.UpdatePaidAssistant(_moneyHolder.Value, _purchasableAssistant != null);
    }
}