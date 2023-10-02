using BabyStack.Model;
using UnityEngine;
using UnityEngine.Events;

public class LockedBuyZonePresenter : BuyZonePresenter
{
    [SerializeField] private UnlockRule[] _unlockRules;

    private int _reduceValue = 1;

    public event UnityAction Aviabled;
    public event UnityAction FirstTimeAviabled;
    public override event UnityAction Unlocking;

    public UnlockRule[] UnlockRules => _unlockRules;

    public bool CanUnlock()
    {
        foreach (var rule in _unlockRules)
            if (rule.CanUnlock == false)
                return false;

        return true;
    }

    protected override void OnEnabled()
    {
        if (CanUnlock())
            Unlock();
        else
            SubscribeUpdateEvents();
    }

    protected override void OnDisabled()
    {
        if (CanUnlock() == false)
            UnsubscribeUpdateEvents();
    }

    protected override void BuyFrame(BuyZone buyZone, MoneyHolder moneyHolder)
    {
        if (CanUnlock() == false)
            return;

        if (moneyHolder.HasMoney == false)
            return;

        _reduceValue = Mathf.Clamp((int)(TotalCost * 1.5f * Time.deltaTime), 1, TotalCost);
        if (buyZone.CurrentCost < _reduceValue)
            _reduceValue = buyZone.CurrentCost;

        _reduceValue = Mathf.Clamp(_reduceValue, 1, moneyHolder.Value);

        buyZone.ReduceCost(_reduceValue);
        moneyHolder.SpendMoney(_reduceValue);

        Unlocking?.Invoke();
    }

    private void Unlock()
    {
        PlayNewText();
        Aviabled?.Invoke();
    }

    private void OnProgressUpdate()
    {
        if (CanUnlock())
        {
            Unlock();
            FirstTimeAviabled?.Invoke();
            UnsubscribeUpdateEvents();
        }
    }

    private void SubscribeUpdateEvents()
    {
        foreach (var rule in _unlockRules)
            rule.AddUpdateListener(OnProgressUpdate);
    }

    private void UnsubscribeUpdateEvents()
    {
        foreach (var rule in _unlockRules)
            rule.RemoveUpdateListener(OnProgressUpdate);
    }
}
