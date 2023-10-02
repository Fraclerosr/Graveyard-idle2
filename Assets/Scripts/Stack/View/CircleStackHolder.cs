using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleStackHolder : StackView
{
    [Space(15)]
    [SerializeField] private float _radius = 1f;
    [SerializeField] private float _deltaAngle = 15f;
    [SerializeField] private float _sortDuration = 1f;

    protected override Vector3 CalculateAddEndPosition(Transform container, Transform stackable)
    {
        var angle = container.childCount * _deltaAngle;
        return GetPositionByAngle(angle) * _radius;
    }

    protected override Vector3 CalculateEndRotation(Transform container, Transform stackable)
    {
        var angle = container.childCount * _deltaAngle;

        return -1 * Quaternion.Euler(0, angle, 0).eulerAngles;
    }

    protected override void Sort(List<Transform> unsortedTransforms)
    {
        for (int i = 0; i < unsortedTransforms.Count; i++)
        {
            var angle = i * _deltaAngle;
            var position = GetPositionByAngle(angle) * _radius;

            unsortedTransforms[i].DOComplete(true);
            unsortedTransforms[i].DOLocalMove(position, _sortDuration);
        }
    }

    private Vector3 GetPositionByAngle(float degreeAngle)
    {
        var xPosition = Mathf.Cos(degreeAngle * Mathf.Deg2Rad);
        var yPosition = Mathf.Sin(degreeAngle * Mathf.Deg2Rad);

        return new Vector3(xPosition, 0, yPosition);
    }
}
