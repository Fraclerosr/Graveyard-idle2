using UnityEngine;

public class HasCustomersTransition : Transition
{
    [SerializeField] private Assistant _parent;

    private void Update()
    {
        foreach (CashDesk cashDesk in _parent.References.CashDesks.Data)
            if (cashDesk.Locked == null && cashDesk.Active)
                NeedTransit = true;
    }
}