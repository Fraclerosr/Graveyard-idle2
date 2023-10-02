using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class CustomerQueue : MonoBehaviour
{
    [SerializeField] private int _capacity;
    [SerializeField] private float _offset;

    public event Action<Customer> FirstCustomerChanged;
    
    private List<Customer> _customers = new List<Customer>();
    public bool NotFull => _customers.Count < _capacity;
    public bool Empty => _customers.Count == 0;
    public int Capacity => _capacity;

    public void Enqueue(Customer customer)
    {
        if (NotFull == false)
            throw new InvalidOperationException();
        
        _customers.Add(customer);
        
        if(_customers.Count == 1)
            FirstCustomerChanged?.Invoke(customer);
    }

    public Customer Dequeue()
    {
        if (Empty)
            throw new InvalidOperationException();
        
        Customer customer = _customers[0];
        _customers.RemoveAt(0);

        if (_customers.Count > 0)
            FirstCustomerChanged?.Invoke(_customers[0]);
        
        return customer;
    }

    public Customer Peek()
    {
        if (_customers.Count == 0)
            throw new InvalidOperationException();
        
        return _customers[0];
    }

    public void Remove(Customer parent)
    {
        if (!_customers.Contains(parent))
            throw new InvalidOperationException();

        _customers.Remove(parent);
    }

    public Vector3 GetPosition(Customer parent) => 
        GetPositionOfIndex(_customers.IndexOf(parent));

    private Vector3 GetPositionOfIndex(int i) => 
        transform.TransformPoint(new Vector3(0, 0, i * _offset));


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < _capacity; i++)
        {
            var position = GetPositionOfIndex(i);
            Gizmos.DrawSphere(position, 0.2f);
        }
    }

#endif
}