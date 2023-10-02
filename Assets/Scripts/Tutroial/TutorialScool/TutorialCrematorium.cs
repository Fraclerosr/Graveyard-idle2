using System;
using UnityEngine;

public class TutorialCrematorium : MonoBehaviour, ITutorialAnalyticsEventSource, ITutorialObjectEventSource
{
    [SerializeField] private TutorialConveyorEventSource _conveyor;

    private TutorialCondition _inCondition;
    private TutorialCondition _outAddedCondition;
    private TutorialCondition _outRemovedCondition;

    public event Action<string> EventSended;

    public GameObject GameObject => _conveyor.transform.parent.gameObject;
    public Transform InPoint => _conveyor.StartPoint;
    public Transform OutPoint => _conveyor.EndPoint;
    public ITutorialStepCondition InCondition => _inCondition;
    public ITutorialStepCondition OutAddedCondition => _outAddedCondition;
    public ITutorialStepCondition OutRemovedCondition => _outRemovedCondition;

    private void Awake()
    {
        _inCondition = new TutorialCondition();
        _outAddedCondition = new TutorialCondition();
        _outRemovedCondition = new TutorialCondition();
    }
    
    public void Enable()
    {
        _conveyor.InStartStackAdded += OnInStartStackAdded;
        _conveyor.InEndStackAdded += OnInEndStackAdded;
        _conveyor.FromEndStackRemoved += OnFromEndStackRemoved;
    }

    public void Disable()
    {
        _conveyor.InStartStackAdded -= OnInStartStackAdded;
        _conveyor.InEndStackAdded -= OnInEndStackAdded;
        _conveyor.FromEndStackRemoved -= OnFromEndStackRemoved;
    }

    private void OnInStartStackAdded()
    {
        _inCondition.Complete();
        EventSended?.Invoke("put_coffin_in_crematorium");
    }

    private void OnInEndStackAdded()
    {
        _outAddedCondition.Complete();
    }

    private void OnFromEndStackRemoved()
    {
        _outRemovedCondition.Complete();
        EventSended?.Invoke("took_urn_from_crematorium");
    }
}