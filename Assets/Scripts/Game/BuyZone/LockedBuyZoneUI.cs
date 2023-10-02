using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockedBuyZoneUI : MonoBehaviour
{
    [SerializeField] private LockedBuyZonePresenter _buyZone;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private UnlockRuleView _viewTemplate;
    [SerializeField] private Transform _viewContainer;
    [SerializeField] private MoneyHolderTrigger _trigger;

    private void OnEnable()
    {
        _trigger.Enter += OnEntered;
        _trigger.Exit += OnExited;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnEntered;
        _trigger.Exit -= OnExited;
    }

    private void Start()
    {
        _canvasGroup.alpha = 0f;

        foreach (var rule in _buyZone.UnlockRules)
        {
            var view = Instantiate(_viewTemplate, _viewContainer);
            view.Render(rule);
        }
    }

    private void OnEntered(MoneyHolder moneyHolder)
    {
        if (_buyZone.CanUnlock())
            return;

        _canvasGroup.DOFade(1f, 0.5f);
    }

    private void OnExited(MoneyHolder moneyHolder)
    {
        _canvasGroup.DOFade(0f, 0.5f);
    }
}
