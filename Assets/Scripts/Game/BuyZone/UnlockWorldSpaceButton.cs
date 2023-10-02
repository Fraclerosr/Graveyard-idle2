using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnlockWorldSpaceButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration = 0.5f;

    private FadeAnimation _fadeAnimation;

    public event UnityAction Clicked;

    public bool CanClick { get; private set; }

    private void Awake()
    {
        _fadeAnimation = new FadeAnimation(_canvasGroup);
        _fadeAnimation.Disable(0);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Enable()
    {
        CanClick = true;
        _fadeAnimation.Enable(_fadeDuration);
    }

    public void Disable()
    {
        CanClick = false;
        _fadeAnimation.Disable(_fadeDuration);
    }

    public void Click()
    {
        if (CanClick)
            OnButtonClick();
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke();
       
    }
}
