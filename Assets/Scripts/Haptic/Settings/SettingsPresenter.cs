using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPresenter : MonoBehaviour
{
    [SerializeField] private MenuView _settings;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private JoystickInput _joystickInput;
    [SerializeField] private Canvas _joystickCanvas;
    [Space(10)]
    [SerializeField] private List<GameObject> _disabledUIObjects;

    private void OnEnable()
    {
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        _settings.CloseButtonClicked += OnCloseButtonClicked;
    }

    private void OnDisable()
    {
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        _settings.CloseButtonClicked -= OnCloseButtonClicked;
    }
    private void OnSettingsButtonClick()
    {
        _settings.gameObject.SetActive(true);
        _disabledUIObjects.ForEach(item => item.SetActive(false));
        _joystickInput.enabled = false;
        _joystickCanvas.enabled = false;
    }

    private void OnCloseButtonClicked()
    {
        _settings.gameObject.SetActive(false);
        _disabledUIObjects.ForEach(item => item.SetActive(true));
        _joystickInput.enabled = true;
        _joystickCanvas.enabled = true;
    }
}
