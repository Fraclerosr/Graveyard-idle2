using UnityEngine;

public class BuyZoneHaptic : BaseHaptics
{
    [SerializeField] private BuyZonePresenter _buyZonePresenter;
    [SerializeField] private MoneyAudio _buyZoneAudio;

    private void OnEnable()
    {
        _buyZonePresenter.Unlocking += OnBuyZoneUnlocking;
        _buyZonePresenter.FirstTimeUnlocked += OnBuyZoneUnlocked;
    }

    private void OnDisable()
    {
        _buyZonePresenter.Unlocking -= OnBuyZoneUnlocking;
        _buyZonePresenter.FirstTimeUnlocked -= OnBuyZoneUnlocked;
    }

    private void OnBuyZoneUnlocking()
    {
        Vibrate();
        _buyZoneAudio.PlayMoneySound();
    }

    private void OnBuyZoneUnlocked(BuyZonePresenter buyZonePresenter)
    {
        _buyZoneAudio.PlayBuySound();
    }
}
