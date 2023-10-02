using System.Linq;
using UnityEngine;

public class CashDeskTombstoneInteractable : TimerStackInteractableZone
{
    [SerializeField] private StackPresenter _stack;
    [SerializeField] private CashDesk _cashDesk;

    public override void InteractAction(StackPresenter enteredStack)
    {
        Stackable stackable;

        stackable = TryRemoveStackable(enteredStack, StackableType.Tombstone);

        if (stackable == null)
            stackable = TryRemoveStackable(enteredStack, StackableType.UpgradedTombstone);
        
        _stack.AddToStack(stackable);
        _cashDesk.Deliver(stackable.Type);
    }

    private Stackable TryRemoveStackable(StackPresenter enteredStack, StackableType type)
    {
        Stackable stackable = null;

        if (CanInteract(enteredStack, type))
            stackable = enteredStack.RemoveFromStack(type);

        return stackable;
    }

    public override bool CanInteract(StackPresenter enteredStack) =>
        CanInteract(enteredStack, StackableType.Tombstone) ||
        CanInteract(enteredStack, StackableType.UpgradedTombstone);

    private bool CanInteract(StackPresenter enteredStack, StackableType stackableType)
    {
        return _cashDesk.Active && enteredStack.CanRemoveFromStack(stackableType) &&
               _stack.CanAddToStack(stackableType) &&
               _cashDesk.PurchaseTypes.Any(type => type == stackableType);
    }
}