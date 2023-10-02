using UnityEngine;

public class LocationContainerInputInteractable : TimerStackInteractableZone
{
    [SerializeField] private LocationItemsContainer _itemsContainer;

    public override bool CanInteract(StackPresenter enteredStack)
    {
        return _itemsContainer.CanRemoveItem && enteredStack.CanAddToStack(_itemsContainer.NextItemType);
    }

    public override void InteractAction(StackPresenter enteredStack)
    {
        var item = _itemsContainer.RemoveItem();
        enteredStack.AddToStack(item);
    }
}
