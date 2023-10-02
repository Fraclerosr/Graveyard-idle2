using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StackableIcons", menuName = "Stackables/StackableIcons", order = 51)]
public class IconByStackable : ScriptableObject
{
    [SerializeField] private List<StackableIcon> _icons = new List<StackableIcon>();

    public Sprite GetIconByType(StackableType type)
    {
        foreach (var pair in _icons)
            if (pair.Type == type)
                return pair.Icon;

        return null;
    }
}

[Serializable]
public class StackableIcon
{
    [SerializeField] private StackableType _type;
    [SerializeField] private Sprite _icon;

    public StackableType Type => _type;
    public Sprite Icon => _icon;
}
