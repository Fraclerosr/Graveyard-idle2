using UnityEngine;

public class TrashBoxInteractable : TimerStackInteractableZone
{
    [SerializeField] private TrashBox _trashBox;

    public override bool CanInteract(StackPresenter enteredStack)
    {
        return TryGetTrashType(out StackableType trashType, enteredStack);
    }

    public override void InteractAction(StackPresenter enteredStack)
    {
        if (TryGetTrashType(out StackableType trashType, enteredStack))
        {
            var trash = enteredStack.RemoveFromStack(trashType);
            _trashBox.Add(trash);
        }
    }

    private bool TryGetTrashType(out StackableType trashType, StackPresenter stackPresenter)
    {
        trashType = default;

        if (stackPresenter.Count == 0)
            return false;

        foreach (var type in _trashBox.TrashTypes.Value)
        {
            if (stackPresenter.CanRemoveFromStack(type))
            {
                trashType = type;
                return true;
            }
        }

        return false;
    }
}
