using System.Collections.Generic;

public class JoinShopQueue : JoinQueue
{
    protected override IEnumerable<CustomerQueue> Queues => Parent.References.ShopQueues.Queues;
    public override IntentionType JoinQueueIntention => IntentionType.GoToShop;

    protected override void Disable()
    {
        Parent.CreateNewPurchaseList();
    }
}