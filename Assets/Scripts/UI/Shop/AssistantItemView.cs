using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AssistantItemView : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _checkMark;

    public event UnityAction<AssistantItemView> Clicked;

    public AssistantData Data { get; private set; }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
    }

    public void Init(AssistantData motherItem)
    {
        Data = motherItem;
    }

    public void RenderAviable()
    {
        _buyButton.gameObject.SetActive(true);
        _name.text = Data.Name;
        _icon.sprite = Data.Icon;
        _price.text = Data.Price.ToString();
    }

    public void RenderBuyed()
    {
        _name.text = Data.Name;
        _icon.sprite = Data.Icon;
        _buyButton.gameObject.SetActive(false);
        _checkMark.enabled = true;
    }

    public void RenderLocked()
    {
        _price.text = Data.Price.ToString();
        _name.text = Data.Name;
        _icon.sprite = Data.Icon;
        _buyButton.gameObject.SetActive(true);
        _buyButton.interactable = false;
    }

    private void OnBuyButtonClicked()
    {
        Clicked?.Invoke(this);
    }
}
