using UnityEngine;
using UnityEngine.UI;

public class CanvasShopPresenter : MonoBehaviour
{
    [SerializeField] private MenuView _shopView;
    [SerializeField] private Button _openButton;
    [SerializeField] private JoystickInput _joystickInput;
    [SerializeField] private Canvas _joystickCanvas;
    
    private void OnEnable()
    {
        _openButton.onClick.AddListener(OnOpenButtonClicked);
        _shopView.CloseButtonClicked += OnCloseButtonClicked;
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(OnOpenButtonClicked);
        _shopView.CloseButtonClicked -= OnCloseButtonClicked;
    }

    private void OnOpenButtonClicked()
    {
        SetShopOpened(false);
    }

    private void OnCloseButtonClicked()
    {
        SetShopOpened(true);
    }

    private void SetShopOpened(bool opened)
    {
        _openButton.gameObject.SetActive(opened);
        _shopView.gameObject.SetActive(!opened);
        _joystickInput.enabled = opened;
        _joystickCanvas.enabled = opened;
    }
}
