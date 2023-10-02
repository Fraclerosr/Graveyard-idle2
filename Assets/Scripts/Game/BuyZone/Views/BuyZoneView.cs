using UnityEngine;
using TMPro;

public class BuyZoneView : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentCost;
    [SerializeField] private Animator _newTextAnimator;

    public void RenderCost(int value)
    {
        _currentCost.text = value.ToString();
    }

    public void PlayNewText()
    {
        _newTextAnimator.SetTrigger("Show");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
