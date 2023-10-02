using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

public class AnalyticsShopEventSender : MonoBehaviour
{
    [SerializeField] private List<AssistantPurchasePresenter> _assistantsShop;
    [SerializeField] private List<MonoBehaviour> _modifications;
    
    private Analytics _analytics;

    private void Awake()
    {
        _analytics = Singleton<Analytics>.Instance;
    }

    private void OnEnable()
    {
        _assistantsShop.ForEach(presenter => presenter.Purchased += OnAssistantUnlocked);
        _modifications.ForEach(modification => (modification as IModificationPlacement).Upgrading += OnModificationUpgrading);
    }

    private void OnDisable()
    {
        _assistantsShop.ForEach(presenter => presenter.Purchased -= OnAssistantUnlocked);
        _modifications.ForEach(modification => (modification as IModificationPlacement).Upgrading -= OnModificationUpgrading);
    }

    private void OnAssistantUnlocked(AssistantData asistantData)
    {
        _analytics.OnSoftSpent("Assistant", asistantData.Name, asistantData.Price);
    }

    private void OnModificationUpgrading(Type modificationType, int price)
    {
        _analytics.OnSoftSpent("Upgrade", modificationType.Name, price);
    }

#if UNITY_EDITOR
    [ContextMenu("Initialize")]
    public void Initialize()
    {
        _assistantsShop = FindObjectsOfType<AssistantPurchasePresenter>(true).ToList();

        var rewardedPlacements = FindObjectsOfType<MonoBehaviour>(true).OfType<IModificationPlacement>().Cast<MonoBehaviour>();
        _modifications.Clear();
        _modifications.AddRange(rewardedPlacements);

        EditorUtility.SetDirty(gameObject);
    }
#endif
}
