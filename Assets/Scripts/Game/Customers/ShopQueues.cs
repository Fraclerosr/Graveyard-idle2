using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopQueues : MonoBehaviour
{
    [SerializeField] private List<CustomerQueue> _queues = new List<CustomerQueue>();

    public IEnumerable<CustomerQueue> Queues => _queues;
    public bool NotFull => _queues?.FirstOrDefault(queue => queue.NotFull) != null;
    public bool Empty => _queues?.FirstOrDefault(queue => !queue.Empty) == null;

    public void Add(CustomerQueue queue)
    {
        if (queue == null)
            throw new InvalidOperationException();

        _queues.Add(queue);
    }
}