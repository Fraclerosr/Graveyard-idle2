using UnityEngine.Events;
using UnityEngine;
using UnityEngine.AI;
using BabyStack.Model;
using System;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour, IModificationListener<float>
{
    [SerializeField] private float Speed = 5f;

    private float _speedRate = 1f;

    private NavMeshAgent _agent;
    private UnityAction _completeAction;

    public bool Completed { get; private set; }
    public float NormalizedSpeed => _agent.velocity.magnitude / 5f;
    public float RemainingDistance => _agent.remainingDistance;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        enabled = false;
        Completed = false;

        _agent.speed = Speed * _speedRate;
        Singleton<AvoidancePrioritySettings>.Instance.SetPriority(_agent);
    }

    private void Update()
    {
        if (_agent.pathPending ||
            _agent.pathStatus == NavMeshPathStatus.PathInvalid ||
            _agent.path.corners.Length == 0)
            return;

        if (_agent.remainingDistance < _agent.stoppingDistance + float.Epsilon)
        {
            _completeAction?.Invoke();
            _completeAction = null;

            Completed = true;
            enabled = false;
        }
    }

    public float CalculateRemainingDistance()
    {
        var points = _agent.path.corners;
        if (points.Length < 2) return 0;
        float distance = 0;
        for (int i = 0; i < points.Length - 1; i++)
            distance += Vector3.Distance(points[i], points[i + 1]);
        return distance;
    }

    public void Warp(Vector3 position)
    {
        _agent.Warp(position);
    }

    public AIMovement Move(Vector3 target)
    {
        Completed = false;
        _completeAction = null;

        _agent.ResetPath();
        _agent.SetDestination(target);
        enabled = true;

        return this;
    }

    public void Stop()
    {
        _completeAction = null;
        _agent.ResetPath();
    }

    public void OnComplete(UnityAction completeAction)
    {
        _completeAction = completeAction;
    }

    public void Enable()
    {
        _agent.enabled = true;
    }

    public void Disable()
    {
        _agent.enabled = false;
    }

    public void SetPriority(int value)
    {
        _agent.avoidancePriority = value;
    }

    public void OnModificationUpdate(float value)
    {
        _speedRate = value;
        _agent.speed = Speed * _speedRate;
    }

    public void SetBonusSpeedRate(float rate)
    {
        if (rate <= 0)
            throw new ArgumentOutOfRangeException(nameof(rate));

        _agent.speed = Speed * _speedRate * rate;
    }

    public void Look(Vector3 direction)
    {
        transform.DOLookAt(transform.position + direction, 1f);
    }
}
