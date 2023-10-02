using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class HorizontalStackHolder : StackView
{
    [SerializeField] private float _space = 0.05f;
    [SerializeField] private int _maxInRow = 4;
    [SerializeField] private Vector3 _axis;

    private void OnValidate()
    {
        _space = Mathf.Clamp(_space, 0f, float.MaxValue);
    }

    protected override Vector3 CalculateAddEndPosition(Transform container, Transform stackable)
    {
        return _axis * ((container.childCount % _maxInRow) * (stackable.lossyScale.x / 2f + _space))
               + Vector3.up * (container.childCount / _maxInRow) * stackable.lossyScale.y / 2f;
    }

    protected override void Sort(List<Transform> unsortedTransforms)
    {
        var sortedList = unsortedTransforms.OrderBy(transftorm => transform.localPosition.x);

        var iteration = 0;
        foreach (var item in sortedList)
        {
            var position = _axis * ((iteration % _maxInRow) * (item.lossyScale.x / 2f + _space))
                           + Vector3.up * (iteration / _maxInRow) * item.lossyScale.y / 2f;

            item.DOComplete(true);
            item.DOLocalMove(position, 0.2f);

            iteration++;
        }
    }
}
