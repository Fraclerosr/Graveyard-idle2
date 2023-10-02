using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGraveyardQueue : JoinQueue
{
    [SerializeField] private Collider _selfCollider;
    [SerializeField] private StackPresenter _selfStack;
    
    private Coroutine _processing;
    private Grave _grave;
    private bool _hasEmptyGrave => Parent.References.GravesContainer.HasEmptyGrave;
    private CustomerQueue _graveyardQueue => Parent.References.GraveyardQueue;

    protected override IEnumerable<CustomerQueue> Queues
    {
        get { yield return _graveyardQueue; }
    }

    protected override void OnEnable()
    {
        _graveyardQueue.FirstCustomerChanged += OnFirstCustomerChanged;
        base.OnEnable();
        _selfCollider.enabled = false;
    }

    private void OnFirstCustomerChanged(Customer customer)
    {
        if(customer != Parent)
            return;
        
        _processing = StartCoroutine(Process());
    }

    protected override void Disable()
    {
        _graveyardQueue.FirstCustomerChanged -= OnFirstCustomerChanged;
        _selfCollider.enabled = true;

        if (_processing != null) 
            StopCoroutine(_processing);
        
        ReleaseGrave();
    }

    private IEnumerator Process()
    {
        var waitPoint = _graveyardQueue.GetPosition(Parent);
        
        while (enabled)
        {
            yield return TakeGrave();
            GoToGrave();
            yield return InteractWithGrave();

            if(!_hasEmptyGrave)
                ReturnToQueue(waitPoint);
            
            ReleaseGrave();
        }        
    }

    private void ReleaseGrave()
    {
        if (_grave != null)
            _grave.Unlock();
    }

    private void GoToGrave()
    {
        _selfCollider.enabled = false;
        Movement.Enable();
        Movement.Move(_grave.PlacePoint.position).OnComplete(() =>
        {
            Movement.Look(_grave.PlacePoint.forward);
            UpdateIntention(IntentionType.NullIntention);
            _selfCollider.enabled = true;
        });
    }

    private IEnumerator InteractWithGrave()
    {
        int beforeCount = _selfStack.Count;
        yield return new WaitUntil(() => _selfStack.Count != beforeCount);
        yield return new WaitForSeconds(1f);
        _selfCollider.enabled = false;

        _grave.Unlock();
    }

    private IEnumerator TakeGrave()
    {
        yield return new WaitUntil(() => _hasEmptyGrave);

        Grave grave = Parent.References.GravesContainer.TakeEmptyGrave();
        _grave = grave;
        _grave.Lock(Parent);
    }

    private void ReturnToQueue(Vector3 waitPoint) => 
        Movement.Move(waitPoint).OnComplete(() => Movement.Look(-_graveyardQueue.transform.forward));

    public override IntentionType JoinQueueIntention => IntentionType.GoToGrave;
}