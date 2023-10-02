using UnityEngine;

public class HasGravesToDigTransition : Transition
{
    [SerializeField] private Assistant _parent;

    private void Update() => 
        NeedTransit = _parent.References.GravesContainer.HasGrave(grave => grave.CanInteract);
}