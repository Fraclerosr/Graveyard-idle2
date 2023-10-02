using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using BabyStack.Model;

public class ModificationView : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _freeButton;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _maxText;
    [SerializeField] private TMP_Text _modificationLevel;
    [SerializeField] private string _levelText = "Lvl";

    public event UnityAction TryBuy;
    public event UnityAction TryFreeBuy;
    public event UnityAction Opened;

    private void OnEnable()
    {
        Opened?.Invoke();
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
        _freeButton?.onClick.AddListener(OnFreeButtonClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        _freeButton?.onClick.RemoveListener(OnFreeButtonClicked);
    }

    public void Render<T>(ModificationData<T> modificationData, int level)
    {
        _price.text = modificationData.Price.ToString();
        _modificationLevel.text = $"{_levelText} {level}";

        if (_buyButton.interactable == false)
            _buyButton.interactable = true;
        
        _buyButton.gameObject.SetActive(true);
        _freeButton?.gameObject.SetActive(true);
        _maxText.enabled = false;
    }

    public void RenderLocked()
    {
        _buyButton.gameObject.SetActive(true);
        _buyButton.interactable = false;
        _freeButton?.gameObject.SetActive(true);
    }

    public void RenderCompleted(int level)
    {
        _modificationLevel.text = $"{_levelText} {level}";
        _buyButton.gameObject.SetActive(false);
        _freeButton?.gameObject.SetActive(false);
        _maxText.enabled = true;
    }

    private void OnBuyButtonClicked()
    {
        TryBuy?.Invoke();
    }

    private void OnFreeButtonClicked()
    {
        TryFreeBuy?.Invoke();
    }
}
