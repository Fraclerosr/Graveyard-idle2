using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ChildrenIntentionSourceListener : MonoBehaviour, IIntentionSource
{
    [SerializeField] private List<MonoBehaviour> _intentionSourceBehaviours;
    
    private IntentionType _lastIntention;

    public event Action<IntentionType> IntentionUpdated;
    
    private IEnumerable<IIntentionSource> _intentionSources => 
        _intentionSourceBehaviours.Cast<IIntentionSource>();
    
    private void OnEnable()
    {
        foreach (IIntentionSource intentionSource in _intentionSources)
            intentionSource.IntentionUpdated += OnIntentionUpdated;
    }

    private void OnDisable()
    {
        foreach (IIntentionSource intentionSource in _intentionSources)
            intentionSource.IntentionUpdated += OnIntentionUpdated;
    }

    private void OnIntentionUpdated(IntentionType intentionType)
    {
        if(intentionType == _lastIntention)
            return;
        
        IntentionUpdated?.Invoke(intentionType);
        _lastIntention = intentionType;
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        if(_intentionSourceBehaviours == null)
            return;
        
        for (int i = 0; i < _intentionSourceBehaviours.Count; i++)
        {
            if (_intentionSourceBehaviours[i] is IIntentionSource)
                continue;

            _intentionSourceBehaviours[i] = null;
        }
    }

    [ContextMenu(nameof(SetupSources))]
    private void SetupSources()
    {
        _intentionSourceBehaviours = new List<MonoBehaviour>();

        _intentionSourceBehaviours.AddRange(GetComponentsInChildren<MonoBehaviour>()
            .Where(component => component is IIntentionSource && component != this));

        EditorUtility.SetDirty(gameObject);
    }
#endif
}