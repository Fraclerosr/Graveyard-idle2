using System.Linq;
using UnityEngine;

public class ConveyorInputInteractable : TimerStackInteractableZone
{
    [SerializeField] private StackableTypes _inputTypes;
    [SerializeField] private StackPresenter _conveyorStack;

    public override void InteractAction(StackPresenter enteredStack)
    {
        Stackable child = enteredStack.RemoveFromStack(GeStackItem(enteredStack).Type);
        _conveyorStack.AddToStack(child);
    }

    private Stackable GeStackItem(StackPresenter enteredStack) => 
        enteredStack.Data.FirstOrDefault(stackable => _inputTypes.Contains(stackable.Type));

    public override bool CanInteract(StackPresenter enteredStack)
    {
        Stackable item = GeStackItem(enteredStack);
        
        if (item == null)
            return false;
        
        return enteredStack.CanRemoveFromStack(item.Type) &&
               _conveyorStack.CanAddToStack(item.Type);
    }
}
