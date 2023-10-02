using UnityEngine;

public class LocationBuyZoneUI : MonoBehaviour
{
    [SerializeField] private Canvas _locationCanvas;
    [SerializeField] private LockedBuyZonePresenter _buyZone;

    private void OnEnable()
    {
        _buyZone.Aviabled += OnZoneAviabled;
    }

    private void OnDisable()
    {
        _buyZone.Aviabled -= OnZoneAviabled;
    }

    private void Start()
    {
        if (_buyZone.CanUnlock())
            OnZoneAviabled();
    }

    private void OnZoneAviabled()
    {
        _locationCanvas.enabled = true;
    }
}
