using System;
using UnityEngine;

public class TutorialItemProducer : MonoBehaviour, ITutorialAnalyticsEventSource, ITutorialObjectEventSource
{
    [SerializeField] private TimerStackableProducer _itemProducer;

    private TutorialCondition _gaveCondition;

    public event Action<string> EventSended;

    [field: SerializeField] public GameObject GameObject { get; private set; }
    [field: SerializeField] public Transform Point { get; private set; }
    
    public string CameraTrigger => CameraAnimatorParameters.ShowItemProducer;
    public ITutorialStepCondition GaveCondition => _gaveCondition;

    private void Awake()
    {
        _gaveCondition = new TutorialCondition();
    }

    public void Enable()
    {
        _itemProducer.ItemGave += OnItemGave;
    }

    public void Disable()
    {
        _itemProducer.ItemGave -= OnItemGave;
    }

    private void OnItemGave(Stackable item)
    {
        EventSended?.Invoke("player_took_wood");
        _gaveCondition.Complete();
    }
}
