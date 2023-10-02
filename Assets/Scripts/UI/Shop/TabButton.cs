using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TabButton : MonoBehaviour
{
    [SerializeField] private GameObject _targetPanel;
    [SerializeField] private Image _image;

    private Button _selfButton;
    private Color _activeColor;
    private Color _hideColor;

    public event UnityAction<TabButton> Clicked;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveListener(OnButtonClicked);
    }

    public void Init(Color activeColor, Color hideColor)
    {
        _activeColor = activeColor;
        _hideColor = hideColor;
    }

    public void HidePanel()
    {
        _targetPanel.SetActive(false);
        _image.color = _hideColor;
    }

    public void ShowPanel()
    {
        _targetPanel.SetActive(true);
        _image.color = _activeColor;
    }

    private void OnButtonClicked()
    {
        Clicked?.Invoke(this);
        ShowPanel();
    }
}
