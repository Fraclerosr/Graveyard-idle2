using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CashDeskInteractable : TimerStackInteractableZone
{
    [SerializeField] private List<StackPresenter> _stacks;
    [SerializeField] private CashDesk _cashDesk;

    public override void InteractAction(StackPresenter enteredStack)
    {
        foreach (StackableType type in _cashDesk.PurchaseTypes)
        {
            if (CanInteract(enteredStack, type))
            {
                Stackable stackable = enteredStack.RemoveFromStack(type);

                foreach (var stack in _stacks.Where(stack => CanAddToStack(type, stack)))
                {
                    if (stack.CanRemoveFromStack(type))
                        continue;

                    stack.AddToStack(stackable);
                    _cashDesk.Deliver(type);

                    return;
                }

                return;
            }
        }
    }

    public override bool CanInteract(StackPresenter enteredStack) =>
        _cashDesk.Active && _cashDesk.PurchaseTypes.Any(type => CanInteract(enteredStack, type));

    private bool CanInteract(StackPresenter enteredStack, StackableType type) =>
        _cashDesk.Active && enteredStack.CanRemoveFromStack(type) && CanAddToStack(type);

    private bool CanAddToStack(StackableType type)
    {
        foreach (var stack in _stacks)
        {
            if (CanAddToStack(type, stack))
                return true;
        }

        return false;
    }

    private static bool CanAddToStack(StackableType type, StackPresenter stack)
    {
        if (stack.CanAddToStack(type) && stack.CanRemoveFromStack(type) == false)
        {
            if ((type == StackableType.Bouquet || type == StackableType.UpgradedBouquet) &&
                (stack.CanRemoveFromStack(StackableType.FlowerCoffin)))
                return false;

            if (type == StackableType.Coffin && (stack.CanRemoveFromStack(StackableType.FlowerCoffin)))
                return false;

            return true;
        }

        return false;
    }
}