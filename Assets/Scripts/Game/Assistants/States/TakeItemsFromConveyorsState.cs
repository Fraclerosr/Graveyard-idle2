using System.Collections;
using System.Linq;
using UnityEngine;

public class TakeItemsFromConveyorsState : State
{
    [SerializeField] private Assistant _parent;
    [SerializeField] private StackPresenter _selfStack;
    [SerializeField] private AIMovement _movement;
    [SerializeField] private Collider _selfCollider;
   
    private Coroutine _process;
    private CashDesk _servingDesk;

    private void OnEnable() => 
        _process = StartCoroutine(Process());

    private void OnDisable()
    {
        StopCoroutine(_process);
        
        if(_servingDesk == null)
            return;
        
        ReleaseTable();
    }

    private IEnumerator Process()
    {
        while (true)
        {
            CashDesk servingDesk = GetRandomDesk();

            _servingDesk = servingDesk;
            
            if (_servingDesk == null)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }
            
            _servingDesk.Lock(_parent);

            var requiredItems = (from item in _servingDesk.PurchaseList.Items
                select item.Key).ToList();
            
            foreach (StackableType item in requiredItems)
            {
                yield return null;
                
                if (_selfStack.IsFull)
                    break;

                Conveyor machine = ConveyorByType(item);

                if(machine == null || machine.TakePoint == null)
                    continue;
                
                _movement.Move(machine.TakePoint.position).OnComplete(() => _selfCollider.enabled = true);

                var beforeCount = _selfStack.Count;
                yield return new WaitUntil(() => _selfStack.Count != beforeCount);

                _selfCollider.enabled = false;
            }

            if (_selfStack.Count == 0)
            {
                ReleaseTable();
                yield return null;
                continue;
            }

            _movement.Move(_servingDesk.ServePoint.position).OnComplete(() => _selfCollider.enabled = true);
            yield return new WaitUntil(() => CanInteract(_servingDesk) == false);
            _selfCollider.enabled = false;
            ReleaseTable();
        }
    }

    private void ReleaseTable()
    {
        _servingDesk.Unlock();
        _servingDesk = null;
    }

    private bool CanInteract(CashDesk desk) => 
        desk.PurchaseTypes.Any(type => _selfStack.CanRemoveFromStack(type));

    private Conveyor ConveyorByType(StackableType type) => 
        _parent.References.ConveyorsContainer.GetByProducibleType(type);


    private CashDesk GetRandomDesk()
    {
        var requiredDesks = (from cashDesk in _parent.References.CashDesks.Data
            where cashDesk.Locked == null && cashDesk.Active
            select cashDesk).ToList();
        
        if (requiredDesks.Count == 0)
            return null;

        return requiredDesks[Random.Range(0, requiredDesks.Count)];
    }
}