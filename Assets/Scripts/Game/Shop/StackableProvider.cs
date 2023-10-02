using UnityEngine;

public abstract class StackableProvider : MonoBehaviour
{
    public abstract Stackable InstantiateStackable();
    public abstract Stackable GetStackable();
}