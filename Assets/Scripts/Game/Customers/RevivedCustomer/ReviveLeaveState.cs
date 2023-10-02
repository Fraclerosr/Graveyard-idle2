using UnityEngine;

public class ReviveLeaveState : State
{
    [SerializeField] protected AICharacter _parent;
    [SerializeField] private AIMovement _movement;
    [SerializeField] private MoneyPayer _moneyPayer;

    private void OnEnable()
    {
        _moneyPayer.Pay();
        _movement.Enable();
        _movement.Move(_parent.References.ExitPoint.position).OnComplete(() => Destroy(_parent.gameObject));
    }
}