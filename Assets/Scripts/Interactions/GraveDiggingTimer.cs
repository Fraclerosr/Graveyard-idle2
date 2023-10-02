using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BabyStack.Model;

public class GraveDiggingTimer : MonoBehaviour
{
    [SerializeField] private TimerView _timerView;
    [SerializeField] private float _interactionTime;

    private Timer _timer = new Timer();

    public ITimer Timer => _timer;
    protected virtual float InteracionTime => _interactionTime;

    private void OnEnable()
    {
        _timerView.Init(_timer);
    }

    private void OnValidate()
    {
        _interactionTime = Mathf.Clamp(_interactionTime, 0f, float.MaxValue);
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    public void Entered()
    {
        _timer.Start(InteracionTime);
    }

    public void Exited()
    {
        _timer.Stop();
    }
}
