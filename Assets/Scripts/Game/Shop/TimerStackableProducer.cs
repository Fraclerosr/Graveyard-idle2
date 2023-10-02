using System;
using BabyStack.Model;
using UnityEngine;
using UnityEngine.Events;

public class TimerStackableProducer : TimerStackInteractableZone, IModificationListener<float>
{
    [SerializeField] private SingleTypeStackableProvider _stackableProvider;
    [SerializeField] private int _maxCount = 10;
    [SerializeField] private JoystickInput _joystickInput;

    private float _interactSpeedRate = 1f;

    public event Action StackedInteraction;
    public event UnityAction<Stackable> ItemGave;
    public event UnityAction<StackPresenter> Enter;
    public event UnityAction<StackPresenter> Exit;

    public StackableType Type => _stackableProvider.Type;
    public int MaxCount => _maxCount;
    protected override float InteracionTime => base.InteracionTime / _interactSpeedRate;

    public override void Entered(StackPresenter enteredStack)
    {
        base.Entered(enteredStack);
        Enter?.Invoke(enteredStack);
    }

    public override void Exited(StackPresenter otherStack)
    {
        base.Exited(otherStack);
        Exit?.Invoke(otherStack);
    }

    public override bool CanInteract(StackPresenter enteredStack)
    {
        return enteredStack.CanAddToStack(_stackableProvider.Type) && 
               enteredStack.CalculateCount(_stackableProvider.Type) < _maxCount;
    }

    public override void InteractAction(StackPresenter enteredStack)
    {
        var stackable = _stackableProvider.InstantiateStackable();
        enteredStack.AddToStack(stackable);

        StackedInteraction?.Invoke();
        ItemGave?.Invoke(stackable);
    }

    public void OnModificationUpdate(float value)
    {
        _interactSpeedRate = value;
    }
}