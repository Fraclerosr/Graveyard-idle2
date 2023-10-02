using UnityEngine;
using BabyStack.Settings;

public class SettingPresenter : MonoBehaviour
{
    [SerializeField] private SettingView _view;

    private ISetting _setting;

    private void OnEnable()
    {
        if (_setting != null)
            _view.Clicked += OnViewClicked;
    }

    private void OnDisable()
    {
        if (_setting != null)
            _view.Clicked -= OnViewClicked;
    }

    public void Init(ISetting setting)
    {
        _setting = setting;

        _view.Clicked += OnViewClicked;

        UpdateView();
    }

    private void OnViewClicked()
    {
        if (_setting.IsEnable)
            _setting.Disable();
        else
            _setting.Enable();

        UpdateView();
    }

    private void UpdateView()
    {
        _view.Render(_setting.IsEnable);
    }
}
