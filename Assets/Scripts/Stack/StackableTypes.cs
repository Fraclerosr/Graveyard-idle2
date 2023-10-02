using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stackable Types", menuName = "Stackable/Types", order = 65)]
public class StackableTypes : ScriptableObject
{
    [SerializeField] private List<StackableType> _stackableTypes;

    public IReadOnlyList<StackableType> Value => _stackableTypes;

    public bool Contains(StackableType type)
    {
        return _stackableTypes.Contains(type);
    }
}
