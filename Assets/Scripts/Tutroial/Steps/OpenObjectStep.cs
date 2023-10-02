using System;
using UnityEngine;
using DG.Tweening;

public class OpenObjectStep : ITutorialStep
{
    private readonly GameObject _gameObject;
    private readonly float _openDuration;
    private Tweener _tweener;

    public OpenObjectStep(GameObject gameObject, float openDuration)
    {
        _gameObject = gameObject;
        _openDuration = openDuration; 
    }

    public bool Completed { get; private set; }

    public void Execute()
    {
        if (_tweener.IsActive())
            throw new InvalidOperationException("Already execute");

        var baseScale = _gameObject.transform.localScale;

        _gameObject.SetActive(true);
        _gameObject.transform.localScale = Vector3.zero;

        _tweener = _gameObject.transform.DOScale(baseScale * 1.2f, _openDuration / 2)
                            .OnComplete(() => 
                            {
                                _gameObject.transform.DOScale(baseScale, _openDuration / 2);
                                Completed = true;
                            });
    }
}
