using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class StackHolder : StackView
{
    [SerializeField] private float _offsetY;
    [SerializeField] private float _sortMoveDuration = 2f;

    private Vector3 _lastTopPosition;
    private int _lastChildCount;

    protected override Vector3 CalculateAddEndPosition(Transform container, Transform stackable)
    {
        var stackableLocalScale = container.InverseTransformVector(stackable.lossyScale);
        var endPosition = new Vector3(0, stackableLocalScale.y / 2, 0);

        if (container.childCount > _lastChildCount)
        {
            endPosition += _lastTopPosition;
        }
        else if (container.childCount != 0)
        {
            Transform topStackable = FindTopStackable(container);
            Vector3 topPosition = new Vector3(0, topStackable.localPosition.y + topStackable.localScale.y / 2, 0);

            endPosition += topPosition;
        }

        endPosition.y += _offsetY;

        _lastChildCount = container.childCount;
        _lastTopPosition = endPosition + new Vector3(0, stackableLocalScale.y / 2, 0);

        return endPosition;
    }

    protected override void Sort(List<Transform> unsortedTransforms)
    {
        var sortedList = unsortedTransforms.OrderBy(transform => transform.localPosition.y);
        var position = Vector3.zero;

        foreach (var item in sortedList)
        {
            position.y += item.localScale.y / 2;

            item.transform.DOComplete(true);
            item.transform.DOLocalMove(position, _sortMoveDuration);

            position.y += item.localScale.y / 2 + _offsetY;
        }
    }

    private Transform FindTopStackable(Transform container)
    {
        Transform topStackable = container.GetChild(0);

        foreach (Transform stackable in container)
            if (topStackable.position.y < stackable.position.y)
                topStackable = stackable;

        return topStackable;
    }
}
