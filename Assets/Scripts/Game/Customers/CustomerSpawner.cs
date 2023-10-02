using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private CharacterReferences _characterReferences;
    [SerializeField] private WorldZone[] _spawnZones;
    [SerializeField] private CustomersContent _customers;
    [SerializeField] private GraveList _gravesContainer;

    private Coroutine _spawning;

    private readonly List<Customer> _spawnedCustomers = new List<Customer>();
    private int _crematoryQueueCapacity => _characterReferences.CrematoryQueue.Capacity;
    private int _graveyardQueueCapacity => _characterReferences.GraveyardQueue.Capacity;
    private int _maxCustomersAmount => _crematoryQueueCapacity + _graveyardQueueCapacity;
    public int SpawnedCount => _spawnedCustomers.Count;

    public event Action<Customer> Spawned;

    private void Start() =>
        _spawning = StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => _spawnedCustomers.Count < _maxCustomersAmount);

        if (_customers.AvailableTypesCount == 2)
        {
            TrySpawnCustomer(_customers.TryGetRandomCustomer(CustomerType.Human));
            yield return new WaitForSeconds(1f);
            Customer customer = TrySpawnCustomer(_customers.TryGetRandomCustomer(CustomerType.Creature));
            
            if(customer != null)
                customer.CreateNewPurchaseList(3);
        }

        yield return new WaitUntil(() => _spawnedCustomers.Count == 0);

        for (int i = 0;; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            yield return new WaitUntil(() => _characterReferences.ShopQueues.NotFull);

            TrySpawnCustomer(_customers.TryGetRandomCustomer(CustomerType.Creature));

            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => _characterReferences.ShopQueues.NotFull);

            if(Random.Range(0f, 1f) < 2f / _gravesContainer.Count)
                TrySpawnCustomer(_customers.TryGetRandomCustomer(CustomerType.Human));

            if (_characterReferences.ShopQueues.NotFull == false)
                yield return new WaitUntil(() => _characterReferences.ShopQueues.Empty);
        }
    }


    private Customer TrySpawnCustomer(Customer customer)
    {
        if (_spawnedCustomers.Count == _maxCustomersAmount)
            return null;

        return CanSpawnOfType(customer) == false ? null : SpawnCustomer(customer);
    }

    private bool CanSpawnOfType(Customer customer)
    {
        switch (customer.Type)
        {
            case CustomerType.Human when _crematoryQueueCapacity == SpawnedOfType(CustomerType.Human):
            case CustomerType.Creature when _graveyardQueueCapacity == SpawnedOfType(CustomerType.Creature):
                return false;
            default:
                return true;
        }
    }

    private Customer SpawnCustomer(Customer customer)
    {
        Vector3 spawnPosition = _spawnZones[Random.Range(0, _spawnZones.Length)].GetPoint();

        customer = Instantiate(customer, spawnPosition, Quaternion.identity);
        customer.transform.parent = transform;
        customer.Init(_characterReferences);
        customer.Run();
        _spawnedCustomers.Add(customer);
        customer.Left += OnCustomerLeft;
        Spawned?.Invoke(customer);

        return customer;
    }

    private bool GetValidCustomer(out Customer customer)
    {
        customer = null;

        if (_crematoryQueueCapacity == SpawnedOfType(CustomerType.Human))
            customer = _customers.TryGetRandomCustomer(CustomerType.Creature);
        else if (_graveyardQueueCapacity == SpawnedOfType(CustomerType.Creature))
            customer = _customers.TryGetRandomCustomer(CustomerType.Human);
        else
            customer = _customers.GetRandomCustomer();

        return customer != null;
    }

    private int SpawnedOfType(CustomerType customerType) =>
        _spawnedCustomers.Count(customer => customer.Type == customerType);

    private void OnCustomerLeft(Customer customer)
    {
        customer.Left -= OnCustomerLeft;
        _spawnedCustomers.Remove(customer);
    }
}