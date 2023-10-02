using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnlockRuleProgress : MonoBehaviour
{
    [SerializeField] private List<UnlockRule> _unlockRules;
    [SerializeField] private IntProgress _intProgress;
    [SerializeField] private IntProgressView _progressView;

    private void OnEnable() => 
        _intProgress.Updated += OnProgressUpdated;

    private void Start() => 
        OnProgressUpdated();

    private void OnDisable() => 
        _intProgress.Updated -= OnProgressUpdated;

    private void OnProgressUpdated()
    {
        if (TryGetFirstLockedRule(out UnlockRule rule))
            DisplayProgress(rule);
        else
            DisableView();
    }

    private bool TryGetFirstLockedRule(out UnlockRule unlockRule)
    {
        unlockRule = _unlockRules.FirstOrDefault(rule => rule.CanUnlock == false);

        return unlockRule != null;
    }

    private void DisplayProgress(UnlockRule rule) => 
        _progressView.Display(rule.CurrentProgress, rule.TargetProgress);

    private void DisableView() => 
        _progressView.gameObject.SetActive(false);
}