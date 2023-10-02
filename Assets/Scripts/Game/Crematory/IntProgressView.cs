using TMPro;
using UnityEngine;

public class IntProgressView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _pattern = "{0}/{1}";

    public void Display(int currentProgress, int targetProgress) => 
        _text.text = string.Format(_pattern, currentProgress, targetProgress);
}