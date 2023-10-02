using UnityEngine;
using BabyStack.Model;
using UnityEngine.UI;
using DG.Tweening;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TimerStackInteractableZone _interactableZone;
    [SerializeField] private CanvasGroup _timerCanvasGroup;
    [SerializeField] private Image _timerImage;
    [SerializeField] private float _fadeDuration = 0.5f;

    private ITimer _timer;
    private ITimer _tempTimer;
    private float _fullTime;
    private Tweener _fadeTweener;

    private void OnEnable()
    {
        _timerCanvasGroup.alpha = 0f;

        if (_tempTimer != null)
        {
            Init(_tempTimer);
            return;
        }

        if (_interactableZone != null)
            Init(_interactableZone.Timer);
    }

    private void OnDisable()
    {
        if (_timer != null)
        {
            _timer.Started -= OnTimerStart;
            _timer.Updated -= OnTimerUpdate;
            _timer.Stopped -= OnTimerStopped;
            _timer.Completed -= OnTimerCompleted;

            _tempTimer = _timer;
            _timer = null;
        }
    }

    public void Init(ITimer timer)
    {
        if (_timer != null)
            return;

        _timer = timer;

        _timer.Started += OnTimerStart;
        _timer.Updated += OnTimerUpdate;
        _timer.Stopped += OnTimerStopped;
        _timer.Completed += OnTimerCompleted;
    }

    private void OnTimerStart(float fullTime)
    {
        if (_fadeTweener.IsActive())
            _fadeTweener.Kill();

        _fadeTweener = _timerCanvasGroup.DOFade(1, _fadeDuration);

        _fullTime = fullTime;
    }

    private void OnTimerUpdate(float ellapsedTime)
    {
        _timerImage.fillAmount = ellapsedTime / _fullTime;
    }

    private void OnTimerStopped()
    {
        if (_fadeTweener.IsActive())
            _fadeTweener.Kill();

        _timerImage.fillAmount = 0f;
        _fadeTweener = _timerCanvasGroup.DOFade(0, _fadeDuration);
    }

    private void OnTimerCompleted()
    {
        if (_fadeTweener.IsActive() == false)
            _fadeTweener = _timerCanvasGroup.DOFade(0, _fadeDuration);

        _timerImage.fillAmount = 0f;
    }
}
