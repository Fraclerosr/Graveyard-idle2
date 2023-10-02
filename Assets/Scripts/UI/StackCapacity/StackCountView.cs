using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class StackCountView : StackUIView
{
    [SerializeField] private Trigger<MoneyHolder> _trigger;
    [SerializeField] private TMP_Text _capacityText;
    [SerializeField] private CanvasGroup _canvasGroup;

    private Coroutine _waitBeforeCoroutine;

    protected override void Render(int currentCount, int capacity, float topPositionY)
    {
        _capacityText.text = Format(currentCount, capacity);
    }

    protected override void Enable()
    {
        _canvasGroup.alpha = 0;

        _trigger.Enter += OnEnter;
        _trigger.Exit += OnExit;
    }

    protected override void Disable()
    {
        _trigger.Enter -= OnEnter;
        _trigger.Exit -= OnExit;
    }

    protected virtual string Format(int currentCount, int capacity) => $"{currentCount}/{capacity}";

    private void OnEnter(MoneyHolder moneyHolder)
    {
        if (_waitBeforeCoroutine != null)
            StopCoroutine(_waitBeforeCoroutine);

        _canvasGroup.DOComplete(true);

        if (_canvasGroup.alpha != 1)
            _canvasGroup.DOFade(1, 0.5f);
    }

    private void OnExit(MoneyHolder moneyHolder)
    {
        _waitBeforeCoroutine = StartCoroutine(WaitBeforeDo(1.5f, () => 
        {
            _canvasGroup.DOComplete(true);

            if (_canvasGroup.alpha != 0)
                _canvasGroup.DOFade(0, 0.5f);
        }));
    }

    private IEnumerator WaitBeforeDo(float duration, Action action)
    {
        yield return new WaitForSeconds(duration);

        action?.Invoke();
    }
}
