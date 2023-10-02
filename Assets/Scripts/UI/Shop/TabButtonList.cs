using System.Collections.Generic;
using UnityEngine;

public class TabButtonList : MonoBehaviour
{
    [SerializeField] private RectTransform _line;
    [SerializeField] private Color _activeButtonColor;
    [SerializeField] private Color _hideButtonColor;
    [SerializeField] private List<TabButton> _tabButtons;

    private TabButton _activeButton;

    private void OnEnable()
    {
        foreach (var button in _tabButtons)
        {
            button.Clicked += SetActiveButton;
            button.Init(_activeButtonColor, _hideButtonColor);
        }

        _activeButton = _tabButtons[0];
        _activeButton.ShowPanel();
        _line.SetParent(_activeButton.transform);
    }

    private void OnDisable()
    {
        foreach (var button in _tabButtons)
        {
            button.Clicked -= SetActiveButton;
            button.HidePanel();
        }
    }

    private void Start()
    {
        _activeButton = _tabButtons[0];
    }

    private void SetActiveButton(TabButton tabButton)
    {
        _line.SetParent(tabButton.transform);

        _activeButton.HidePanel();
        _activeButton = tabButton;
    }
}
