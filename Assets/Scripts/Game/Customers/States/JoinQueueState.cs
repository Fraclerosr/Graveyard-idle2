using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class JoinQueue : State, IIntentionSource
{
    [SerializeField] protected Customer Parent;
    [SerializeField] protected AIMovement Movement;
    
    private CustomerQueue _customerQueue;

    protected abstract IEnumerable<CustomerQueue> Queues { get; }

    public abstract IntentionType JoinQueueIntention { get; }
    public event Action<IntentionType> IntentionUpdated;

    protected virtual void OnEnable()
    {
        Movement.Enable();
        
        foreach (CustomerQueue queue in Queues)
        {
            if (queue.NotFull)
            {
                _customerQueue = queue;
                _customerQueue.Enqueue(Parent);
                GoToPositionInQueue();
                IntentionUpdated?.Invoke(JoinQueueIntention);

                if (_customerQueue.Peek() != Parent)
                    _customerQueue.FirstCustomerChanged += OnFirstCustomerChanged;
                
                break;
            }
        }
        
        Enable();
    }

    private void OnFirstCustomerChanged(Customer customer)
    {
        GoToPositionInQueue();
    }

    private void GoToPositionInQueue()
    {
        Movement.Move(_customerQueue.GetPosition(Parent));
        Movement.OnComplete(() =>
        {
            Movement.Look(-_customerQueue.transform.forward);
            IntentionUpdated?.Invoke(IntentionType.NullIntention);
            JoinedQueue();
        });
    }

    private void OnDisable()
    {
        if (_customerQueue == null)
            return;
        
        if(_customerQueue.Peek() == Parent)
            _customerQueue.Dequeue();
        else
            _customerQueue.Remove(Parent);


        Movement.Disable();
        _customerQueue.FirstCustomerChanged -= OnFirstCustomerChanged;
        IntentionUpdated?.Invoke(IntentionType.NullIntention);

        Disable();
    }

    protected virtual void JoinedQueue(){}
    protected virtual void Enable(){}
    protected virtual void Disable(){}

    protected void UpdateIntention(IntentionType type)
    {
        IntentionUpdated?.Invoke(type);
    }
}