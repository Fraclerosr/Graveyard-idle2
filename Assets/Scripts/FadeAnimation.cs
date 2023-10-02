using UnityEngine;
using DG.Tweening;

public class FadeAnimation
{
    private readonly CanvasGroup _canvasGroup;
    private Tweener _fadeTweener;

    public FadeAnimation(CanvasGroup canvasGroup)
    {
        _canvasGroup = canvasGroup;
    }

    public void Enable(float duration)
    {
        _canvasGroup.interactable = true;
        Fade(1, duration);
    }

    public void Disable(float duration)
    {
        _canvasGroup.interactable = false;
        Fade(0, duration);
    }

    private void Fade(float endValue, float duration)
    {
        if (_fadeTweener.IsActive())
            _fadeTweener.Kill();

        _fadeTweener = _canvasGroup.DOFade(endValue, duration);
    }
}
