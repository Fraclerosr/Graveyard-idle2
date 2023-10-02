using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackInteractableZoneView : MonoBehaviour
{
    [SerializeField] private Transform _underlay;
    [SerializeField] private float _enterSize = 1.1f;
    [SerializeField] private float _animationDuration = 0.25f;

    private Vector3 _scale;

    private void Start()
    {
        _scale = _underlay.localScale;
    }

    public void Enter()
    {
        _underlay.DOComplete(true);
        _underlay.DOScale(_scale * _enterSize, _animationDuration);
    }

    public void Exit()
    {
        _underlay.DOComplete(true);
        _underlay.DOScale(_scale, _animationDuration);
    }
}
