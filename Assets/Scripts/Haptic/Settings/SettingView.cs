using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SettingView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _enableIcon;
    [SerializeField] private Sprite _disableIcon;

    private Button _button;

    public event Action Clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(bool enabled)
    {
        _image.sprite = enabled ? _enableIcon : _disableIcon;
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke();
    }
}
