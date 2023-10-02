using UnityEngine;

public class ConveyorList : ReferenceObjectList<Conveyor>
{
    [SerializeField] private ListProgress _conveyorsProgress;
    [SerializeField] private CharacterReferences _characterReferences;

    protected override void AfterUnlocked(Conveyor reference, bool onLoad, string guid)
    {
        _characterReferences.ConveyorsContainer.Add(reference);
        
        if (_conveyorsProgress.Contains(guid))
            return;

        _conveyorsProgress.Add(guid);
        _conveyorsProgress.Save();
    }
}
