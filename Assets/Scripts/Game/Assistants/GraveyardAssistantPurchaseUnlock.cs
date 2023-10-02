using System;
using UnityEngine;

public class GraveyardAssistantPurchaseUnlock : MonoBehaviour
{
    [SerializeField] private UnlockRule _unlockRule;
    [SerializeField] private ShopUnlockView _unlockView;
    [SerializeField] private GravesContainer _gravesContainer;

    private void OnEnable()
    {
        if (_unlockRule.CanUnlock)
        {
            _unlockView.OnUnlocked();
            return;
        }

        _gravesContainer.Cleared += OnCleared;
    }

    private void OnCleared()
    {
        _unlockView.OnUnlocked();
        _gravesContainer.Cleared -= OnCleared;
    }
}