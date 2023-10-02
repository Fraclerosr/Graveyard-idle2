using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShopBehaviour : TutorialBehaviour, ITutorialAnalyticsEventSource
{
    [Header("Tutorial Objects")] [SerializeField]
    private TutorialCustomer _customer;

    [SerializeField] private TutorialItemProducer _itemProducer;
    [SerializeField] private TutorialConveyor _conveyor;
    [SerializeField] private TutorialCrematorium _crematorium;
    [SerializeField] private TutorialMoneyTake _moneyTake;
    [SerializeField] private TutorialGrave _grave;
    [SerializeField] private float _dropMoneyDelay = 2f;

    public event Action<string> EventSended;

    public override void Initialize(Action onInitialized = null)
    {
        WaitUntil(_customer.SpawnedCondition, () =>
        {
            Camera.InitializeCustomerCamera(_customer.FirstCustomer.transform);
            onInitialized?.Invoke();
        });

        WaitUntil(_customer.SecondCustomerSpawnedCondition,
            () => Camera.InitializeSecondCustomerCamera(_customer.SecondCustomer.transform));
    }

    protected override List<ITutorialObjectEventSource> GetTutorialObjectEventSources()
    {
        return new List<ITutorialObjectEventSource>()
            {_customer, _itemProducer, _conveyor, _grave, _crematorium};
    }

    protected override ITutorialStep GetTutorialSequanceSteps()
    {
        var tutorialSequanceSteps = SequenceSteps.Create(
            SequenceSteps.Create(
                new DelayStep(1.0f),
                new ShowObjectStep(Camera, _customer.CameraTrigger),
                new WaitConditionStep(_customer.WaitCondition)
            ),
            SequenceSteps.Create(
                new PointerStep(_itemProducer.Point, ObjectArrow, PlayerArrow),
                new ShowObjectStep(Camera, _itemProducer.CameraTrigger),
                new DelayStep(2.0f),
                new ShowObjectStep(Camera, CameraAnimatorParameters.ShowPlayer),
                new WaitConditionStep(_itemProducer.GaveCondition)
            ),
            SequenceSteps.Create(
                CreateOpenObjectSequanceSteps(_conveyor.GameObject, _conveyor.InPoint, _conveyor.CameraTrigger),
                new WaitConditionStep(_conveyor.InCondition),
                new PointerStep(null, ObjectArrow, PlayerArrow),
                new WaitConditionStep(_conveyor.OutAddedCondition)
            ),
            SequenceSteps.Create(
                new PointerStep(_conveyor.OutPoint, ObjectArrow, PlayerArrow),
                new ShowObjectStep(Camera, _conveyor.CameraTrigger),
                new DelayStep(OpenDuration),
                new ShowObjectStep(Camera, CameraAnimatorParameters.ShowPlayer),
                new WaitConditionStep(_conveyor.OutRemovedCondition)
            ),
            SequenceSteps.Create(
                new PointerStep(_customer.Point, ObjectArrow, PlayerArrow),
                new WaitConditionStep(_customer.GotItem),
                new PointerStep(null, ObjectArrow, PlayerArrow),
                new ShowObjectStep(Camera, _customer.SecondCameraTrigger),
                new DelayStep(2),
                new ShowObjectStep(Camera, CameraAnimatorParameters.ShowPlayer)
            ),
            SequenceSteps.Create(
                new WaitConditionStep(_customer.CustomerGoToGraveyard),
                new ShowObjectStep(Camera, _customer.SecondCameraTrigger),
                new DelayStep(5),
                new ShowObjectStep(Camera, _grave.CameraTrigger),
                new PointerStep(_grave.Point, ObjectArrow, PlayerArrow),
                new DelayStep(2),
                new ShowObjectStep(Camera, CameraAnimatorParameters.ShowPlayer),
                new WaitConditionStep(_grave.BuriedFirst),
                new PointerStep(null, ObjectArrow, PlayerArrow),
                new WaitConditionStep(_grave.BuriedAll),
                new DelayStep(3)
            ),
            SequenceSteps.Create(
                new ShowObjectStep(Camera, _customer.CameraTrigger),
                new PointerStep(_customer.Point, ObjectArrow, PlayerArrow),
                new DelayStep(3),
                new ShowObjectStep(Camera, CameraAnimatorParameters.ShowPlayer),
                new WaitConditionStep(_customer.GaveItem)
                //new PointerStep(null, ObjectArrow, PlayerArrow)
            ),
            SequenceSteps.Create(
                new PointerStep(_crematorium.InPoint, ObjectArrow, PlayerArrow),
                new WaitConditionStep(_crematorium.InCondition),
                new PointerStep(null, ObjectArrow, PlayerArrow),
                new WaitConditionStep(_crematorium.OutAddedCondition),
                new PointerStep(_crematorium.OutPoint, ObjectArrow, PlayerArrow),
                new WaitConditionStep(_crematorium.OutRemovedCondition)
            ),
            new PointerStep(null, ObjectArrow, PlayerArrow),
            new ActionStep(() => EventSended?.Invoke("tutorial_complete"))
        );

        return tutorialSequanceSteps;
    }

    private SequenceSteps CreateOpenObjectSequanceSteps(GameObject gameObject, Transform point, string cameraTrigger)
    {
        return SequenceSteps.Create(
            new PointerStep(point, ObjectArrow, PlayerArrow),
            new DelayStep(DelayBeforeOpen),
            new OpenObjectStep(gameObject, OpenDuration),
            new DelayStep(DelayBeforeOpen)
        );
    }
}