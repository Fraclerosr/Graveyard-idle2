using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabFocus : MonoBehaviour
{
    [SerializeField] private float _duration;

    private RectTransform _rectTransform;
    private Vector2 _targetPosition;

    private void Start()
    {
        _rectTransform = transform as RectTransform;
    }

    private void Update()
    {
        _targetPosition = new Vector2(0, _rectTransform.anchoredPosition.y);
        _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _targetPosition, _duration * Time.deltaTime);
    }
}
