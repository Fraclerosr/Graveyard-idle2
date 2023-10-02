using UnityEngine;

public class CrematoryNotEmptyQueueTransition : Transition
{
    [SerializeField] private Assistant _parent;

    private void Update() => 
        NeedTransit = !_parent.References.CrematoryQueue.Empty;
}