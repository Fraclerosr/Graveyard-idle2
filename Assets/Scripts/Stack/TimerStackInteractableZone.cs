using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using BabyStack.Model;

public abstract class TimerStackInteractableZone : StackInteractableZoneBase
{
    [SerializeField] private float _interactionTime;
    [SerializeField] private StackInteractableZoneView _view;

    private Timer _timer = new Timer();
    private Coroutine _waitCoroutine;

    public ITimer Timer => _timer;
    protected virtual float InteracionTime => _interactionTime;

    private void OnValidate()
    {
        _interactionTime = Mathf.Clamp(_interactionTime, 0f, float.MaxValue);
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    public override void Enabled()
    {
        base.Enabled();
        _timer.Completed += OnTimeOver;
    }

    public override void Disabled()
    {
        base.Disabled();
        _timer.Completed -= OnTimeOver;
        
        if (_waitCoroutine != null)
            StopCoroutine(_waitCoroutine);
    }

    public override void Entered(StackPresenter enteredStack)
    {
        _view?.Enter();
        
        if (CanInteract(enteredStack))
            _timer.Start(InteracionTime);
        else
            _waitCoroutine = StartCoroutine(WaitUntilCanInteract(() => _timer.Start(InteracionTime)));
    }

    public override void Exited(StackPresenter otherStack)
    {
        if (_waitCoroutine != null)
            StopCoroutine(_waitCoroutine);

        _view?.Exit();

        _timer.Stop();
    }

    private void OnTimeOver()
    {
        InteractAction(EnteredStack);

        if (CanInteract(EnteredStack))
            _timer.Start(InteracionTime);
        else
            _waitCoroutine = StartCoroutine(WaitUntilCanInteract(() => _timer.Start(InteracionTime)));
    }

    private IEnumerator WaitUntilCanInteract(UnityAction finalAction)
    {
        yield return new WaitUntil(() => CanInteract(EnteredStack));
        finalAction?.Invoke();
    }

    public abstract void InteractAction(StackPresenter enteredStack);
    public abstract bool CanInteract(StackPresenter enteredStack);
}
