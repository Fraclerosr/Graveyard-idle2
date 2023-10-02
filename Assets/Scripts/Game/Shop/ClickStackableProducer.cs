using System;
using UnityEngine;

public class ClickStackableProducer : ClickStackInteractableZone
{
    [SerializeField] private SingleTypeStackableProvider _stackableProvider;

    public event Action StackedInteraction;
    
    public StackableType ProducibleType => _stackableProvider.Type;

    public override bool CanInteract(StackPresenter enteredStack)
    {
        return enteredStack.CanAddToStack(_stackableProvider.Type);
    }

    public override void InteractAction(StackPresenter enteredStack)
    {
        Stackable stackable = _stackableProvider.InstantiateStackable();
        stackable.transform.position = transform.position;
        enteredStack.AddToStack(stackable);
        StackedInteraction?.Invoke();
    }
}