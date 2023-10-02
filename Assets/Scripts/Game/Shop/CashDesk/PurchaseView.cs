using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseView : MonoBehaviour
{
    [SerializeField] private IconByStackable _iconByStackable;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;

    public void Display(StackableType type, int amount)
    {
        _text.text = amount.ToString();
        _icon.sprite = _iconByStackable.GetIconByType(type);
    }
}