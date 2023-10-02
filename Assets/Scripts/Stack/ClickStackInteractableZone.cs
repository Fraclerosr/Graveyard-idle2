using UnityEngine;

public abstract class ClickStackInteractableZone : StackInteractableZoneBase
{
    [SerializeField] private JoystickInput _joystickInput;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !_joystickInput.StoppedMoving && !_joystickInput.Moving)
            TryInteract();
    }

    public void TryInteract()
    {
        if (EnteredStack && CanInteract(EnteredStack))
            Interact();
    }

    private void Interact()
    {
        InteractAction(EnteredStack);
    }

    public abstract void InteractAction(StackPresenter enteredStack);
    public abstract bool CanInteract(StackPresenter enteredStack);
}