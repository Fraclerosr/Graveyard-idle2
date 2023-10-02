using System;
using System.Collections;
using UnityEngine;

public class TutorialGrave : MonoBehaviour, ITutorialAnalyticsEventSource, ITutorialObjectEventSource
{
    [SerializeField] private Grave _grave;
    [SerializeField] private Transform _pointPosition;
    [SerializeField] private GravesContainer _gravesContainer;
    
    private TutorialCondition _buriedFirst;
    private TutorialCondition _buriedAll;
    private int _collectedMoney;

    public event Action<string> EventSended;

    public Transform Point => _pointPosition;
    public string CameraTrigger => CameraAnimatorParameters.ShowGrave;
    public ITutorialStepCondition BuriedFirst => _buriedFirst;
    public ITutorialStepCondition BuriedAll => _buriedAll;

    private void Awake()
    {
        _buriedFirst = new TutorialCondition();
        _buriedAll = new TutorialCondition();
    }

    public void Enable()
    {
        _grave.Updated += OnGraveUpdated;
    }

    public void Disable()
    {
        _grave.Updated -= OnGraveUpdated;
    }

    private void OnGraveUpdated(Grave grave)
    {
        if(grave.CanInteract)
            return;
        
        _buriedFirst.Complete();
        StartCoroutine(WaitForAllGraves());
        EventSended?.Invoke("grave_dug");
    }

    private IEnumerator WaitForAllGraves()
    {
        yield return new WaitUntil(() => _gravesContainer.AllGraves(grave => !grave.Dug && grave.HasCoffin));
        _buriedAll.Complete();
    }
}