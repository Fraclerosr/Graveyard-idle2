using UnityEngine;
using BabyStack.Model;

public abstract class StackUIView : MonoBehaviour
{
    private StackStorage _stack;
    private IStackableContainer _stackableContainer;

    private void OnEnable()
    {
        Enable();
    }

    private void OnDisable()
    {
        if (_stack != null)
        {
            _stackableContainer.Added -= OnAdded;
            _stackableContainer.Removed -= OnRemoved;
            _stack.CapacityChanged -= OnCapacityChanged;
        }

        Disable();
    }

    public void Init(StackStorage stack, IStackableContainer stackableContainer)
    {
        _stack = stack;
        _stackableContainer = stackableContainer;

        _stackableContainer.Added += OnAdded;
        _stackableContainer.Removed += OnRemoved;
        _stack.CapacityChanged += OnCapacityChanged;

        Render(_stack.Count, _stack.Capacity, _stackableContainer.FindTopPositionY());
    }

    protected abstract void Render(int currentCount, int capacity, float topPositionY);
    protected virtual void Enable() { }
    protected virtual void Disable() { }

    private void OnAdded(Stackable stackable)
    {
        Render(_stack.Count, _stack.Capacity, _stackableContainer.FindTopPositionY());
    }

    private void OnRemoved()
    {
        Render(_stack.Count, _stack.Capacity, _stackableContainer.FindTopPositionY());
    }

    private void OnCapacityChanged()
    {
        Render(_stack.Count, _stack.Capacity, _stackableContainer.FindTopPositionY());
    }
}
