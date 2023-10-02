using System.Collections;
using UnityEngine;

public class ButtonUpdateGraveyard : Trigger<PlayerInteraction>
{
    [SerializeField] private GravesContainer _gravesContainer;

    protected override void OnStay(PlayerInteraction triggered)
    {
        _gravesContainer.TryClear();    
    }
}
