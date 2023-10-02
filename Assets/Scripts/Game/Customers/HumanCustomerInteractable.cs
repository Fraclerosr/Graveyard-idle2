using BabyStack.Model;
using UnityEngine;

public class HumanCustomerInteractable : TimerStackInteractableZone, IModificationListener<float>
{
    [SerializeField] private StackPresenter _selfStack;

    private float _speedRate = 1f;

    protected override float InteracionTime => base.InteracionTime / _speedRate;

    public override bool CanInteract(StackPresenter enteredStack)
    {
        bool canRemove = CanRemoveFromStack(enteredStack, StackableType.FlowerCoffin);
        canRemove |= CanRemoveFromStack(enteredStack, StackableType.Coffin);
        
        return canRemove;
    }

    private bool CanRemoveFromStack(StackPresenter enteredStack, StackableType stackableType)
    {
        return enteredStack.CanAddToStack(stackableType) && 
               _selfStack.CanRemoveFromStack(stackableType);
    }

    public override void InteractAction(StackPresenter enteredStack)
    {
        if (CanInteract(enteredStack) == false)
            return;

        Stackable stackable;
        
        stackable = TryRemove(enteredStack, StackableType.FlowerCoffin);
        
        if(stackable == null)
            stackable = TryRemove(enteredStack, StackableType.Coffin);
        
        enteredStack.AddToStack(stackable);
    }

    private Stackable TryRemove(StackPresenter enteredStack, StackableType stackableType)
    {
        Stackable stackable = null;
        
        if (CanRemoveFromStack(enteredStack, stackableType))
            stackable = _selfStack.RemoveFromStack(stackableType);
        
        return stackable;
    }

    public void OnModificationUpdate(float value)
    {
        _speedRate = value;
    }
}
