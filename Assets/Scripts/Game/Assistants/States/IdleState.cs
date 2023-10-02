using UnityEngine;

public class IdleState : State
{
    [SerializeField] private Assistant _parent;
    [SerializeField] private AIMovement _movement;
    [SerializeField] private Collider _selfCollider;

    private Vector3 _target;

    private void OnEnable()
    {
        var shift = Random.insideUnitCircle;
        var point = GetNearestWaitPoint();
        _target = point + new Vector3(shift.x, 0, shift.y);
        
        _movement.Enable();

        Enabled();
        _selfCollider.enabled = false;
    }

    private void Update() => 
        _movement.Move(_target);

    private void OnDisable() => 
        _selfCollider.enabled = true;

    private Vector3 GetNearestWaitPoint()
    {
        Transform nearestPoint = null;
        var minDistance = float.MaxValue;
        
        foreach (var point in _parent.WaitPoints)
        {
            var distance = Vector3.Distance(transform.position, point.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPoint = point;
            }
        }

        return nearestPoint.position;
    }

    protected virtual void Enabled() { }
}
