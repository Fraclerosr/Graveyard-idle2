using UnityEngine;

[RequireComponent(typeof(LockedBuyZonePresenter))]
public class LockedBuyZoneView : MonoBehaviour
{
    [SerializeField] private Canvas _viewCanvas;
    [SerializeField] private SpriteRenderer _lockImage;

    private LockedBuyZonePresenter _buyZone;

    private void Awake()
    {
        _buyZone = GetComponent<LockedBuyZonePresenter>();
    }

    private void OnEnable()
    {
        _buyZone.Aviabled += OnAviabled;
    }

    private void OnDisable()
    {
        _buyZone.Aviabled -= OnAviabled;
    }

    private void Start()
    {
        if (_buyZone.CanUnlock())
            OnAviabled();
    }

    private void OnAviabled()
    {
        _viewCanvas.gameObject.SetActive(true);
        _lockImage.enabled = false;
    }
}
