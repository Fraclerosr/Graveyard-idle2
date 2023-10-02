using UnityEngine;

public class StackableItemView : ScriptableObject
{
    [field: SerializeField] public StackableType Type { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}