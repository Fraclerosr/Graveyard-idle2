using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Create IntentionTypeViewsContainer", fileName = "IntentionTypeViewsContainer", order = 0)]
public class IntentionTypeViewsContainer : ScriptableObject
{
    [SerializeField] private List<IntentionTypeView> _typeViews;
    
    public IntentionTypeView GetForType(IntentionType nextIntentionType)
    {
        if (_typeViews.Exists(view => view.IntentionType == nextIntentionType))
            return _typeViews.First(view => view.IntentionType == nextIntentionType);

        throw new InvalidOperationException();
    }
}