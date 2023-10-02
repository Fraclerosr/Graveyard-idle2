using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AnalyticsBuyZoneEventSender : MonoBehaviour
{
    [SerializeField] private List<BuyZonePresenter> _buyZones;
    
    private Analytics _analytics;

    private void Awake()
    {
        _analytics = Singleton<Analytics>.Instance;
    }

    private void OnEnable()
    {
        foreach (var zone in _buyZones)
            zone.FirstTimeUnlocked += OnZoneUnlocked;
    }

    private void OnDisable()
    {
        foreach (var zone in _buyZones)
            zone.FirstTimeUnlocked -= OnZoneUnlocked;
    }

    private void OnZoneUnlocked(BuyZonePresenter zone)
    {
        _analytics.OnSoftSpent("Buying an object", zone.UnlockableObject.Type.ToString(), zone.TotalCost);
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(SetupRewardedPlacements))]
    private void SetupRewardedPlacements()
    {
        var rewardedPlacements = FindObjectsOfType<BuyZonePresenter>(true);

        _buyZones.Clear();
        _buyZones.AddRange(rewardedPlacements);
        EditorUtility.SetDirty(gameObject);
    }
#endif
}
