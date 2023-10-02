using System;
using UnityEngine;
using BabyStack.Model;

public class ShopPresenter : MonoBehaviour
{
    [SerializeField] private ShopZone _shopZone;
    [SerializeField] private MenuView _shopView;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private JoystickInput _joystickInput;
    [SerializeField] private Canvas _joystickCanvas;
    [SerializeField] private float _waitTime = 1f;
    [SerializeField] private GameObject _modifications;

    private Timer _timer = new Timer();

    private void OnEnable()
    {
        _timerView.Init(_timer);
     
        _shopZone.Enter += OnEnter;
        _shopZone.Exit += OnExit;
        _timer.Completed += OnTimerCompleted;
        _shopView.CloseButtonClicked += OnCloseButtonClicked;
    }

    private void Start()
    {
        SetModificationsActive(false);
    }

    private void OnDisable()
    {
        _shopZone.Enter -= OnEnter;
        _shopZone.Exit -= OnExit;
        _timer.Completed -= OnTimerCompleted;
        _shopView.CloseButtonClicked -= OnCloseButtonClicked;
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    private void OnDestroy()
    {
        gameObject.SetActive(false);
    }

    private void OnEnter(PlayerStackPresenter player)
    {
        SetModificationsActive(true);
        _timer.Start(_waitTime);
    }

    private void OnExit(PlayerStackPresenter player)
    {
        _timer.Stop();
        SetModificationsActive(false);
    }

    private void SetModificationsActive(bool active)
    {
        if(_modifications)
            _modifications.SetActive(active);
    }

    private void OnTimerCompleted()
    {
        _shopView.gameObject.SetActive(true);
        _joystickInput.enabled = false;
        _joystickCanvas.enabled = false;
    }

    private void OnCloseButtonClicked()
    {
        _shopView.gameObject.SetActive(false);
        _joystickInput.enabled = true;
        _joystickCanvas.enabled = true;
    }
}
