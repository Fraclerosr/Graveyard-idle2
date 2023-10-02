using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnalytics : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _analyticsEventSources = new List<MonoBehaviour>();
    
    private Analytics _analytics;

    private bool _enabled;

    private void OnValidate()
    {
        for (int i = 0; i < _analyticsEventSources.Count; i++)
        {
            if (_analyticsEventSources[i] is ITutorialAnalyticsEventSource)
                continue;

            _analyticsEventSources[i] = null;
        }
    }

    private void Start()
    {
        _analytics = Singleton<Analytics>.Instance;
        if (_enabled == false)
            return;

        OnAnalyticsEventSended("tutorial_start");
    }

    public void Enable()
    {
        _enabled = true;
        foreach (ITutorialAnalyticsEventSource source in _analyticsEventSources)
            source.EventSended += OnAnalyticsEventSended;
    }

    public void Disable()
    {
        _enabled = false;
        foreach (ITutorialAnalyticsEventSource source in _analyticsEventSources)
            source.EventSended -= OnAnalyticsEventSended;
    }

    private void OnAnalyticsEventSended(string eventName)
    {
        _analytics.FireEvent(eventName);
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(SetupAnalyticsEventSenders))]
    private void SetupAnalyticsEventSenders()
    {
        var analyticsEventSenders = GetComponentsInChildren<MonoBehaviour>(true).OfType<ITutorialAnalyticsEventSource>().Cast<MonoBehaviour>();

        _analyticsEventSources.Clear();
        _analyticsEventSources.AddRange(analyticsEventSenders);
    }
#endif
}
