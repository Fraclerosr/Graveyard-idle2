using UnityEngine;

public class MoneyTrigger : Trigger<DroppableDollar>
{
    protected override int Layer => LayerMask.NameToLayer("Default");
}
