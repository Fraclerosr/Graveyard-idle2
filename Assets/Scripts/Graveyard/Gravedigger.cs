using System;
using BabyStack.Model;
using UnityEngine;

public class Gravedigger : MonoBehaviour
{
    [SerializeField] private DoctorAnimation _doctorAnimation;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private float _interactionTime;
    [SerializeField] private StackView _stackContainer;

    private readonly Timer _timer = new Timer();
    private Grave _graveTrigger;

    public event Action StartedDigging;
    public event Action StoppedDigging;

    public bool Interacting => _graveTrigger != null;
    
    private void OnEnable() => 
        _timerView.Init(_timer);

    private void OnDisable() => 
        _timer.Completed -= OnTimerCompleted;

    private void OnValidate() => 
        _interactionTime = Mathf.Clamp(_interactionTime, 0f, float.MaxValue);

    private void Update() => 
        _timer.Tick(Time.deltaTime);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Grave grave))
        {
            if (_graveTrigger != null)
                return;
            
            if (grave.CanInteract)
            {
                _graveTrigger = grave;
                _doctorAnimation.StartDigging();
                _graveTrigger.StartInteraction(_interactionTime);
                _timer.Start(_interactionTime);
                _timer.Completed += OnTimerCompleted;
                _stackContainer.gameObject.SetActive(false);
                _doctorAnimation.StopHolding();
                StartedDigging?.Invoke();
            }
        }
    }

    private void OnTimerCompleted()
    {
        _timer.Completed -= OnTimerCompleted;
        _doctorAnimation.StopDigging();
        _doctorAnimation.UpdateHolding();
        _graveTrigger.StopInteraction();
        _graveTrigger = null;
        _stackContainer.gameObject.SetActive(true);
        StoppedDigging?.Invoke();
    }
}
