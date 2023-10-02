using UnityEngine;

public class PlayerStackPresenterTrigger : StackPresenterTrigger
{
    protected override int Layer => LayerMask.NameToLayer("PlayerTrigger");
}
