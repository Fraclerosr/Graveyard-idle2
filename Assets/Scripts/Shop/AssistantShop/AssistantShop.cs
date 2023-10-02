using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

public class AssistantShop : MonoBehaviour
{
    [SerializeField] private AssistantShopData _assistants;
    [SerializeField] private AssistantItemView _viewTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private MoneyHolder _moneyHolder;

    private List<AssistantItemView> _views;
    private AssistantInventory _inventory;

    public event UnityAction<AssistantData> Unlocked;

    private void OnEnable()
    {
        _views = new List<AssistantItemView>();
        _inventory = new AssistantInventory(_assistants);
        _inventory.Load();

        _moneyHolder.BalanceChanged += OnBalanceChanged;

        foreach (var assistantData in _assistants.Data)
        {
            var view = Instantiate(_viewTemplate, _container);
            view.Init(assistantData);
            view.Clicked += OnViewClicked;

            _views.Add(view);
        }

        UpdateViews();
    }

    private void OnViewClicked(AssistantItemView view)
    {
        if (view.Data.Price > _moneyHolder.Value)
            return;

        _moneyHolder.SpendMoney(view.Data.Price);
        _inventory.Add(view.Data);
        _inventory.Save();

        Unlocked?.Invoke(view.Data);

        UpdateViews();
    }

    private void OnBalanceChanged(int balance)
    {
        UpdateViews();
    }

    private void UpdateViews()
    {
        foreach (var view in _views)
        {
            if (_inventory.Contains(view.Data))
                view.RenderBuyed();
            else if (view.Data.Price > _moneyHolder.Value)
                view.RenderLocked();
            else
                view.RenderAviable();
        }
    }

    private void OnDisable()
    {
        _moneyHolder.BalanceChanged -= OnBalanceChanged;

        foreach (var view in _views)
            Destroy(view.gameObject);

        _views.Clear();
    }
}
