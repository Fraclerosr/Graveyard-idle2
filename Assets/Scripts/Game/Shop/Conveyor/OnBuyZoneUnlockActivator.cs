using UnityEngine;

public class OnBuyZoneUnlockActivator : MonoBehaviour
{
    [SerializeField] private BuyZonePresenter _buyZone;

    private void Awake()
    {
        gameObject.SetActive(false);
        _buyZone.Unlocked += OnUnlocked;
    }

    private void OnDestroy()
    {
        _buyZone.Unlocked -= OnUnlocked;
    }

    private void OnUnlocked(BuyZonePresenter _)
    {
        gameObject.SetActive(true);
    }
}
