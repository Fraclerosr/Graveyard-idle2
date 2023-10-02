public static class PurchaseListExtensions
{
    public static PurchaseList TryAdd(this PurchaseList purchaseList, UnlockableStackableItem stackableItem, int count)
    {
        if (stackableItem != null)
            purchaseList.Add(stackableItem.StackableType, count);

        return purchaseList;
    } 
}