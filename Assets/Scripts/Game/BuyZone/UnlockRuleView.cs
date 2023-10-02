using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockRuleView : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    [SerializeField] private Image _icon;

    private UnlockRule _rule;

    public void Render(UnlockRule rule)
    {
        _value.text = $"{rule.CurrentProgress}/{rule.TargetProgress}";
        _icon.sprite = rule.Icon;

        _rule = rule;
        _rule.AddUpdateListener(OnRuleUpdate);
        OnRuleUpdate();
    }

    private void OnRuleUpdate()
    {
        if (_rule.CanUnlock)
            _value.text = "Yep";
        else
            _value.text = $"{_rule.CurrentProgress}/{_rule.TargetProgress}";
    }

    private void OnDisable()
    {
        if (_rule)
            _rule.RemoveUpdateListener(OnRuleUpdate);
    }
}
