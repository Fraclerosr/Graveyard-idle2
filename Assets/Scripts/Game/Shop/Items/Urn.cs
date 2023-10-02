using UnityEngine;

public class Urn : Stackable
{
    [field: SerializeField] public RevivedCustomer RevivedCustomer { get; private set; }
    
    public override StackableType Type => StackableType.Urn;
}