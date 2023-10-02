using System;
using UnityEngine;

public class TutorialCustomer : MonoBehaviour, ITutorialAnalyticsEventSource, ITutorialObjectEventSource
{
    [SerializeField] private CustomerSpawner _spawner;
    [SerializeField] private GameObject _graveyardWall;

    private Customer _firstCustomer;
    private Customer _secondCustomer;
    private TutorialCondition _spawnedCondition;
    private TutorialCondition _waitConditions;
    private TutorialCondition _gotItem;
    private TutorialCondition _customerGoToGraveyard;
    private TutorialCondition _secondCustomerSpawnedCondition;
    private TakeCashDeskState _takeCashDeskState;
    private TakeCashDeskState _secondCustomerState;
    private TutorialCondition _gaveItem;
    private StackPresenter _firstCustomerStack;

    public event Action<string> EventSended;
    public Transform Point { get; private set; }
    public GameObject FirstCustomer => _firstCustomer.gameObject;
    public GameObject SecondCustomer  => _secondCustomer.gameObject;
    public string CameraTrigger => CameraAnimatorParameters.ShowCustomer;
    public string SecondCameraTrigger => CameraAnimatorParameters.ShowSecondCustomer;
    public ITutorialStepCondition SpawnedCondition => _spawnedCondition;
    public ITutorialStepCondition SecondCustomerSpawnedCondition => _secondCustomerSpawnedCondition;
    public ITutorialStepCondition WaitCondition => _waitConditions;
    public ITutorialStepCondition GotItem => _gotItem;
    public ITutorialStepCondition GaveItem => _gaveItem;
    public ITutorialStepCondition CustomerGoToGraveyard => _customerGoToGraveyard;

    private void Awake()
    {
        _spawnedCondition = new TutorialCondition();
        _secondCustomerSpawnedCondition = new TutorialCondition();
        _waitConditions = new TutorialCondition();
        _gotItem = new TutorialCondition();
        _gaveItem = new TutorialCondition();
        _customerGoToGraveyard = new TutorialCondition();
    }
    
    public void Enable()
    {
        _spawner.Spawned += OnStudentSpawned;
        _graveyardWall.gameObject.SetActive(true);
    }

    public void Disable()
    {
        _spawner.Spawned -= OnStudentSpawned;

        if (_takeCashDeskState == null)
            return;
        
        _takeCashDeskState.TookTable += TookTable;
        _takeCashDeskState.TookAllItems += GotItems;
    }

    private void OnStudentSpawned(Customer customer)
    {
        if (_spawner.SpawnedCount == 1) 
            FirstCustomerSpawned(customer);

        if (_spawner.SpawnedCount == 2)
            SecondCustomerSpawned(customer);
    }

    private void SecondCustomerSpawned(Customer customer)
    {
        _secondCustomer = customer;
        _secondCustomerState = customer.GetComponentInChildren<TakeCashDeskState>();
        _secondCustomerState.TookAllItems += SecondCustomerTookItems;
        
        _secondCustomerSpawnedCondition.Complete();
    }

    private void SecondCustomerTookItems()
    {
        _secondCustomerState.TookAllItems -= SecondCustomerTookItems;
        _customerGoToGraveyard.Complete();
        _graveyardWall.gameObject.SetActive(false);
    }

    private void FirstCustomerSpawned(Customer customer)
    {
        if (_firstCustomer != null) 
            return;
        
        _firstCustomer = customer;

        _takeCashDeskState = _firstCustomer.GetComponentInChildren<TakeCashDeskState>();
        _firstCustomerStack = customer.GetComponentInChildren<StackPresenter>();
        
        var pointObject = new GameObject("Point");
        pointObject.transform.SetParent(customer.transform);
        pointObject.transform.localPosition = Vector3.up * 2f;

        Point = pointObject.transform;
        
        _takeCashDeskState.TookTable += TookTable;
        _takeCashDeskState.TookAllItems += GotItems;
        _spawnedCondition.Complete();

        EventSended?.Invoke("first_customer_spawned");
    }

    private void TookTable(Customer student)
    {
        EventSended?.Invoke("first_customer_made_order");
        Invoke(nameof(Complete), 1.0f);
    }

    private void GotItems()
    {
        _gotItem.Complete();
        EventSended?.Invoke("first_customer_took_items");
        _firstCustomerStack.Removed += OnFirstCustomerItemRemoved;
    }

    private void OnFirstCustomerItemRemoved(Stackable _)
    {
        _firstCustomerStack.Removed -= OnFirstCustomerItemRemoved;
        _gaveItem.Complete();
    }

    private void Complete()
    {
        _waitConditions.Complete();
    }
}