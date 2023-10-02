using UnityEngine;

public abstract class StackInteractableZoneBase : MonoBehaviour
{
    [SerializeField] private Trigger<StackPresenter> _trigger;
    
    protected StackPresenter EnteredStack;

    private void OnEnable()
    {
        _trigger.Enter += OnEnter;
        _trigger.Stay += OnStay;
        _trigger.Exit += OnExit;
        
        Enabled();
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnEnter;
        _trigger.Stay -= OnStay;
        _trigger.Exit -= OnExit;

        EnteredStack = null;
        Disabled();
    }

    private void OnEnter(StackPresenter enteredStack)
    {
        if (EnteredStack != null)
            return;
        
        EnteredStack = enteredStack;
        
        Entered(enteredStack);
    }

    private void OnStay(StackPresenter enteredStack)
    {
        if (EnteredStack == null)
            OnEnter(enteredStack);
    }

    private void OnExit(StackPresenter otherStack)
    {
        if (otherStack == EnteredStack)
        {
            Exited(otherStack);
            EnteredStack = null;
        }
    }

    public virtual void Entered(StackPresenter enteredStack) { }
    public virtual void Exited(StackPresenter otherStack) { }
    public virtual void Enabled() { }
    public virtual void Disabled() { }
}