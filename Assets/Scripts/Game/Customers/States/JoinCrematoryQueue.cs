using System.Collections.Generic;
using UnityEngine;

public class JoinCrematoryQueue : JoinQueue
{
    [SerializeField] private StackPresenterTrigger _stackPresenterTrigger;
    [SerializeField] private Collider _selfCollider;
    
    protected override IEnumerable<CustomerQueue> Queues
    {
        get { yield return Parent.References.CrematoryQueue; }
    }

    public override IntentionType JoinQueueIntention => IntentionType.GoToCrematory;

    private void Awake() => 
        SetActive(false);

    protected override void JoinedQueue() => 
        SetActive(true);

    protected override void Enable() => 
        _selfCollider.enabled = false;

    protected override void Disable() => 
        SetActive(false);

    private void SetActive(bool active) => 
        _stackPresenterTrigger.gameObject.SetActive(active);
}