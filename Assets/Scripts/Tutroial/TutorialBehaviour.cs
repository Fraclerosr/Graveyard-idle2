using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialBehaviour : MonoBehaviour
{
    [field: Header("Settings")]
    [field: SerializeField] protected TutorialObjectArrow ObjectArrow { get; private set; }
    [field: SerializeField] protected TutorialPlayerArrow PlayerArrow { get; private set; }
    [field: SerializeField] protected TutorialCamera Camera { get; private set; }
    [field: SerializeField] protected float DelayBeforeOpen { get; private set; } = 0.35f;
    [field: SerializeField] protected float OpenDuration { get; private set; } = 1f;

    public event Action SequanceStepsEnded;

    private List<ITutorialObjectEventSource> _objectEventSources;

    public void Enable()
    {      
        _objectEventSources ??= GetTutorialObjectEventSources();
        foreach (var sorce in _objectEventSources)        
            sorce.Enable();        
    }

    public void Disable()
    {
        foreach (var sorce in _objectEventSources)
            sorce.Disable();
    }

    public ITutorialStep CreateTutorialSequenceSteps()
    {
        var tutorialSequanceSteps = SequenceSteps.Create(
            GetTutorialSequanceSteps(),
            new ActionStep(() => SequanceStepsEnded?.Invoke())
            );
        return tutorialSequanceSteps;
    }

    public abstract void Initialize(Action onInitialized = null);

    protected abstract ITutorialStep GetTutorialSequanceSteps();
    protected abstract List<ITutorialObjectEventSource> GetTutorialObjectEventSources();

    protected void WaitUntil(ITutorialStepCondition condition, Action action)
    {
        StartCoroutine(StartWaitUntil(condition, action));
    }

    private IEnumerator StartWaitUntil(ITutorialStepCondition condition, Action action)
    {
        yield return new WaitUntil(() => condition.Completed);
        action?.Invoke();
    }
}