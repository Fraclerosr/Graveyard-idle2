using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TakeItemsFromCustomersState : State
{
    [SerializeField] private Assistant _parent;
    [SerializeField] private StackPresenter _selfStack;
    [SerializeField] private AIMovement _movement;
    [SerializeField] private Collider _selfCollider;

    private Coroutine _process;
    private Conveyor _crematoryConveyor;

    private void OnEnable()
    {
        _process = StartCoroutine(Process());
    }

    private void OnDisable()
    {
        StopCoroutine(_process);
    }

    private IEnumerator Process()
    {
        CustomerQueue crematoryQueue = _parent.References.CrematoryQueue;

        while (true)
        {
            yield return null;

            if (crematoryQueue.Empty == false && _parent.References.CrematoryConveyor.FullInput == false)
            {
                Customer customer = crematoryQueue.Peek();

                if (customer.PurchaseList.Empty == false)
                    continue;

                yield return TakeItems(crematoryQueue);
                yield return FillConveyor();

                if (crematoryQueue.Empty == false && _crematoryConveyor.EndCount == 0)
                {
                    _crematoryConveyor.Unlock();
                    continue;
                }

                _selfCollider.enabled = false;
                _crematoryConveyor.Unlock();

                yield return new WaitForSeconds(2f);
            }
            else if (!_parent.References.UrnStack.IsFull)
            {
                yield return DeliverUrn(_crematoryConveyor);
            }
        }
    }

    private IEnumerator TakeItems(CustomerQueue crematoryQueue)
    {
        GoToQueue(crematoryQueue);

        yield return new WaitUntil(() => _selfStack.IsFull || crematoryQueue.Empty);

        _selfCollider.enabled = false;
    }

    private void GoToQueue(CustomerQueue crematoryQueue)
    {
        Vector3 queueDirection = crematoryQueue.transform.position - transform.position;
        var targetPosition = transform.position + queueDirection - 1.5f * queueDirection.normalized;

        _movement.Move(targetPosition).OnComplete(() => _selfCollider.enabled = true);
    }

    private IEnumerator FillConveyor()
    {
        Conveyor crematoryConveyor = _parent.References.CrematoryConveyor;
        _crematoryConveyor = crematoryConveyor;
        _crematoryConveyor.Lock(_parent);

        _movement.Move(_crematoryConveyor.PlacePoint.position).OnComplete(() => _selfCollider.enabled = true);

        yield return new WaitUntil(() => _crematoryConveyor.FullInput || _selfStack.Count == 0);
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator DeliverUrn(Conveyor crematoryConveyor)
    {
        _movement.Move(crematoryConveyor.TakePoint.position).OnComplete(() => _selfCollider.enabled = true);

        yield return new WaitUntil(() => crematoryConveyor.EndCount == 0 || _selfStack.IsFull);

        _selfCollider.enabled = false;

        _movement.Move(_parent.References.UrnStorage.position).OnComplete(() => _selfCollider.enabled = true);

        yield return new WaitUntil(() => _selfStack.Count == 0);

        _selfCollider.enabled = false;

        GoToQueue(_parent.References.CrematoryQueue);
    }
}