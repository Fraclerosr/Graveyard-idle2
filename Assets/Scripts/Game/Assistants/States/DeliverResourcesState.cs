using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class DeliverResourcesState : State
{
    [SerializeField] private Assistant _parent;
    [SerializeField] private StackPresenter _selfStack;
    [SerializeField] private AIMovement _movement;
    [SerializeField] private Collider _selfCollider;
    
    private Coroutine _process;

    private void OnEnable() => 
        _process = StartCoroutine(Process());

    private void OnDisable() => 
        StopCoroutine(_process);

    private IEnumerator Process()
    {
        while (true)
        {
            var requiredItems = _parent.References.ConveyorsContainer.GetResourceTypes().ToList();
            
            foreach (StackableType item in requiredItems)
            {
                yield return null;

                Conveyor conveyor = ConveyorByResourceType(item);
                
                if(conveyor.FullInput || conveyor.PlacePoint == null)
                    continue;
                
                var producer = _parent.References.Producers.FirstOrDefault(producer => producer.Type == item);
                
                if (producer == null)
                    continue;
                
                _movement.Move(producer.transform.position).OnComplete(() => _selfCollider.enabled = true);
                
                yield return new WaitUntil(() => _selfStack.IsFull);

                _selfCollider.enabled = false;
                yield return null;
                _movement.Move(conveyor.PlacePoint.position).OnComplete(() => _selfCollider.enabled = true);

                yield return new WaitUntil(() => !_selfStack.CanRemoveFromStack(item) || !conveyor.CanAddToStack(item));

                foreach (Stackable stackable in _selfStack.RemoveAll())
                    Destroy(stackable.gameObject);
                
                _selfCollider.enabled = false;
            }
        }
    }

    private Conveyor ConveyorByResourceType(StackableType type) => 
        _parent.References.ConveyorsContainer.GetByResourceType(type);
}