using System;
using UnityEngine;

public class TutorialConveyor : MonoBehaviour, ITutorialAnalyticsEventSource, ITutorialObjectEventSource
{
    [SerializeField] private UnlockableReference _conveyorReference;

    private TutorialConveyorEventSource _conveyor;
    private TutorialCondition _inCondition;
    private TutorialCondition _outAddedCondition;
    private TutorialCondition _outRemovedCondition;

    public event Action<string> EventSended;

    public GameObject GameObject => _conveyor.transform.parent.gameObject;
    public Transform InPoint => _conveyor.StartPoint;
    public Transform OutPoint => _conveyor.EndPoint;
    public string CameraTrigger => CameraAnimatorParameters.ShowConveyor;
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
        _conveyorReference.Unlocked += OnConveyorUnlocked;
    }

    public void Disable()
    {
        if (_conveyor == null)
        {
            _conveyorReference.Unlocked -= OnConveyorUnlocked;
        }
        else
        {
            _conveyor.InStartStackAdded -= OnInStartStackAdded;
            _conveyor.InEndStackAdded -= OnInEndStackAdded;
            _conveyor.FromEndStackRemoved -= OnFromEndStackRemoved;
        }
    }

    private void OnConveyorUnlocked(MonoBehaviour conveyour, bool onLoad, string guid)
    {
        _conveyor = conveyour.GetComponent<TutorialConveyorEventSource>();
        _conveyorReference.Unlocked -= OnConveyorUnlocked;

        GameObject.SetActive(false);

        _conveyor.InStartStackAdded += OnInStartStackAdded;
        _conveyor.InEndStackAdded += OnInEndStackAdded;
        _conveyor.FromEndStackRemoved += OnFromEndStackRemoved;
    }

    private void OnInStartStackAdded()
    {
        _inCondition.Complete();
        EventSended?.Invoke("put_wood_in_conveyor");
    }

    private void OnInEndStackAdded()
    {
        _outAddedCondition.Complete();
    }

    private void OnFromEndStackRemoved()
    {
        _outRemovedCondition.Complete();
        EventSended?.Invoke("took_coffin_from_conveyor");
    }
}
