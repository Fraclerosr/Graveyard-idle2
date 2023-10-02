using UnityEngine;

public class DeadBody : Stackable
{
    [field: SerializeField] public Urn UrnTemplate { get; private set; }
    public override StackableType Type => StackableType.DeadBody;
}