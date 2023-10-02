using UnityEngine;

public class MoneyZoneTrigger : Trigger<MoneyZone>
{
    protected override int Layer => LayerMask.NameToLayer("Default");
}
