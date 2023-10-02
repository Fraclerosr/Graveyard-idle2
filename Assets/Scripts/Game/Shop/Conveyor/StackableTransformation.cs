using UnityEngine;

public abstract class StackableTransformation : MonoBehaviour
{
    public abstract StackableType Type { get; }

    public abstract Stackable Transform(Stackable removedItem);
}