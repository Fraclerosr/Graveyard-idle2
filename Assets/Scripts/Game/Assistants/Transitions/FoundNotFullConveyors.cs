using UnityEngine;

public class FoundNotFullConveyors : Transition
{
    [SerializeField] private Assistant _parent;

    private void Update()
    {
        foreach (Conveyor conveyor in _parent.References.ConveyorsContainer.Conveyors)
            if (conveyor.Locked == null && !conveyor.FullInput)
                NeedTransit = true;
    }
}