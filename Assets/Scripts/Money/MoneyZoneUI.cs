using TMPro;
using UnityEngine;
using DG.Tweening;

public class MoneyZoneUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private MoneyHolderTrigger _trigger;
    [SerializeField] private MoneyZone _moneyZone;
    [SerializeField] private TMP_Text _dollarsCount;

    private void OnEnable()
    {
        _moneyZone.Changed += OnChanged;
        _trigger.Enter += TriggerEnter;
        _trigger.Exit += TriggerExit;
    }

    private void OnDisable()
    {
        _moneyZone.Changed -= OnChanged;
        _trigger.Enter -= TriggerEnter;
        _trigger.Exit -= TriggerExit;
    }

    private void Start()
    {
        _dollarsCount.text = _moneyZone.DollarsValue.ToString();
        _canvasGroup.alpha = 0;
    }

    private void TriggerEnter(MoneyHolder arg0)
    {
        _canvasGroup.DOFade(1f, 0.5f);
    }

    private void TriggerExit(MoneyHolder arg0)
    {
        _canvasGroup.DOFade(0f, 0.5f);
    }

    private void OnChanged()
    {
        _dollarsCount.text = _moneyZone.DollarsValue.ToString();
    }
}
