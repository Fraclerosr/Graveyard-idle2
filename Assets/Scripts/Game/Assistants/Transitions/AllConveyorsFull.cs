using UnityEngine;

public class AllConveyorsFull : Transition
{
    [SerializeField] private Assistant _parent;

    private void Update()
    {
        var needTransit = true;
        
        foreach (Conveyor conveyor in _parent.References.ConveyorsContainer.Conveyors)
            if (conveyor.Locked == null && !conveyor.FullInput)
                needTransit = false;

        NeedTransit = needTransit;
    }
}