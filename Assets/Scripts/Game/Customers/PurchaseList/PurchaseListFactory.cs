using UnityEngine;

public abstract class PurchaseListFactory : MonoBehaviour
{
    public abstract PurchaseList Create();
    public abstract PurchaseList Create(int count);
}