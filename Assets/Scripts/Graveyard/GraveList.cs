using System.Collections;
using UnityEngine;

public class GraveList : ReferenceObjectList<Grave>
{
    private const float AddToContainerDelay = 1.0f;
    
    [SerializeField] private ListProgress _gravesProgress;
    [SerializeField] private GravesContainer _gravesContainer;
    [SerializeField] private CharacterReferences _characterReferences;

    protected override void AfterUnlocked(Grave reference, bool onLoad, string guid)
    {
        StartCoroutine(AddToContainer(reference));

        reference.Init(_characterReferences);
        
        if (_gravesProgress.Contains(guid))
            return;

        _gravesProgress.Add(guid);
        _gravesProgress.Save();
    }

    private IEnumerator AddToContainer(Grave reference)
    {
        yield return new WaitForSeconds(AddToContainerDelay);
        _gravesContainer.Add(reference);
    }
}