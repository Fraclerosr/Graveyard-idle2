using UnityEngine;

public class HasNoCustomersTransition : Transition
{
    [SerializeField] private Assistant _parent;

    private void Update() => 
        NeedTransit = _parent.References.ShopQueues.Empty;
}