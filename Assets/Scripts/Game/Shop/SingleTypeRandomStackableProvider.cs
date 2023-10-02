using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleTypeRandomStackableProvider : SingleTypeStackableProvider
{
    [SerializeField] private StackableType _type;
    [SerializeField] private List<Stackable> _stackableList;

    public override StackableType Type => _type;

    private void OnValidate()
    {
        if (_stackableList == null)
            return;

        for (var i = 0; i < _stackableList.Count; i++)
            if (_stackableList[i]?.Type != _type)
                _stackableList[i] = null;
    }

    public override Stackable InstantiateStackable() => 
        Instantiate(GetStackable(), transform);
    
    public override Stackable GetStackable() => 
        _stackableList[Random.Range(0, _stackableList.Count)];
}