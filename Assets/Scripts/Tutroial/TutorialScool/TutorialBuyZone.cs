using System;
using UnityEngine;

public class TutorialBuyZone : MonoBehaviour, ITutorialStepCondition, ITutorialAnalyticsEventSource, ITutorialObjectEventSource
{
    [SerializeField] private BuyZonePresenter _buyZonePresenter;

    public event Action<string> EventSended;

    [field: SerializeField] public Transform Point { get; private set; }
    public bool Completed { get; private set; }
    public GameObject GameObject => _buyZonePresenter.gameObject;
    public string CameraTrigger => CameraAnimatorParameters.ShowBuyZone;
    public ITutorialStepCondition Condition => this;

    public void Enable()
    {
        _buyZonePresenter.Unlocked += OnUnlocked;
    }

    public void Disable()
    {
        if (Completed == false)
            _buyZonePresenter.Unlocked -= OnUnlocked;
    }

    public void OnUnlocked(BuyZonePresenter buyZonePresenter)
    {
        _buyZonePresenter.Unlocked -= OnUnlocked;

        Completed = true;
        EventSended?.Invoke("unlock_desk");
    }
}
