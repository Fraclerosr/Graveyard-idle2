using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasShopPresenter))]
public class ShopUnlocker : MonoBehaviour
{
    [SerializeField] private Button _shopButton;
    [SerializeField] private UnlockRule _unlockRule;

    private CanvasShopPresenter _shopPresenter;
    public bool IsUnlocked { get; private set; } = false;

    public event Action Unlocked;

    private void Awake()
    {
        _shopPresenter = GetComponent<CanvasShopPresenter>();
        _shopPresenter.enabled = false;
    }

    private void OnEnable()
    {
        _unlockRule.AddUpdateListener(OnUnlocked);
    }

    private void OnDisable()
    {
        _unlockRule.RemoveUpdateListener(OnUnlocked);
    }

    private void Start()
    {
        if (_unlockRule.CanUnlock)
            OnUnlocked();
    }

    private void OnUnlocked()
    {
        if (IsUnlocked)
            return;

        IsUnlocked = _unlockRule.CanUnlock;
        
        if(IsUnlocked)
            Unlocked?.Invoke();

        UpdateActive();
    }

    private void UpdateActive()
    {
        _shopPresenter.enabled = IsUnlocked;
        _shopButton.gameObject.SetActive(IsUnlocked);
    }
}
