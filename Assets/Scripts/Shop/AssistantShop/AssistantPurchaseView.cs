using System;
using UnityEngine;
using UnityEngine.UI;

public class AssistantPurchaseView : MonoBehaviour
{
    [SerializeField] private CostButtonView _costButtonView;
    [SerializeField] private Button _rewardAssistantButton;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _freeAssistantButton;
    [SerializeField] private Animation _appearanceAnimation;
    [SerializeField] private GameObject _exclamationMark;

    private FadeAnimation _fadeAnimation;
    private float _fadeDuration = 0.23f;

    public event Action OnTryBuy;
    public event Action OnTryShowRewarded;
    public event Action OnFreeBuy;

    private void OnEnable()
    {
        _costButtonView.Clicked += TryBuy;
        _exclamationMark.gameObject.SetActive(true);
        _freeAssistantButton.onClick.AddListener(OnFreeBuyClicked);
        _rewardAssistantButton.onClick.AddListener(OnRewarded);
    }

    private void Start()
    {
        _fadeAnimation = new FadeAnimation(_canvasGroup);
    }

    private void OnDisable()
    {
        _costButtonView.Clicked -= TryBuy;
        _exclamationMark.gameObject.SetActive(false);
        _freeAssistantButton.onClick.RemoveListener(OnFreeBuyClicked);
        _rewardAssistantButton.onClick.RemoveListener(OnRewarded);
    }

    public void Init(int assistantDataPrice, int moneyHolderValue)
    {
        _costButtonView.Init(assistantDataPrice, moneyHolderValue);
    }

    public void DisplayFreeAssistant()
    {
        _freeAssistantButton.gameObject.SetActive(true);
        _costButtonView.gameObject.SetActive(false);
        _rewardAssistantButton.gameObject.SetActive(false);
    }

    public void DisplayPaidAssistant()
    {
        _freeAssistantButton.gameObject.SetActive(false);
    }

    public void UpdatePaidAssistant(int balance, bool active)
    {
        _costButtonView.gameObject.SetActive(active);
        _rewardAssistantButton.gameObject.SetActive(active);
        _costButtonView.DisplayForBalance(balance);
    }

    public void Disable()
    {
        _fadeAnimation.Disable(_fadeDuration);
    }

    public void Enable()
    {
        _appearanceAnimation.Play();
        _fadeAnimation.Enable(_fadeDuration);
    }

    private void OnFreeBuyClicked()
    {
        OnFreeBuy?.Invoke();
    }

    private void TryBuy()
    {
        OnTryBuy?.Invoke();
    }

    private void OnRewarded()
    {
        OnTryShowRewarded?.Invoke();
    }
}