using UnityEngine;

public class FixedTransformation : StackableTransformation
{
    [SerializeField] private Stackable _stackable;

    public override StackableType Type => _stackable.Type;

    public override Stackable Transform(Stackable removedItem) => 
        _stackable;
}