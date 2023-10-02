using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemView<T> : MonoBehaviour where T : IShopItem<MonoBehaviour>
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;

    public event UnityAction<ShopItemView<T>> Clicked;

    public T Data { get; private set; }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
    }

    public void Init(T motherItem)
    {
        Data = motherItem;
    }

    public void RenderAviable()
    {
        _price.text = Data.Price.ToString();
        _name.text = Data.Name;
        _icon.sprite = Data.Icon;
    }

    public void RenderBuyed()
    {
        _price.text = "Buyed";
        _name.text = Data.Name;
        _icon.sprite = Data.Icon;
        _buyButton.interactable = false;
    }

    public void RenderLocked()
    {
        _price.text = Data.Price.ToString();
        _name.text = Data.Name;
        _icon.sprite = Data.Icon;
        _buyButton.interactable = false;
    }

    private void OnBuyButtonClicked()
    {
        Clicked?.Invoke(this);
    }
}
