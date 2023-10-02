using UnityEngine;

public class LeaveState : State
{
    [SerializeField] protected Customer _parent;
    [SerializeField] private AIMovement _movement;

    private void OnEnable()
    {
        _movement.Enable();
        _movement.Move(_parent.References.ExitPoint.position).OnComplete(() =>
        {
            _parent.Leave();
            Destroy(_parent.gameObject);
        });
    }
}