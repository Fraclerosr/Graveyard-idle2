using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ReverceStackHolder : StackView
{
    [SerializeField] private float _space;
    [SerializeField] private float _sortAnimationDuration;

    private void OnValidate()
    {
        _space = Mathf.Clamp(_space, 0f, float.MaxValue);
        _sortAnimationDuration = Mathf.Clamp(_sortAnimationDuration, 0f, float.MaxValue);
    }

    private void OnEnable()
    {
        Added += OnReverceStackAdded;
    }

    private void OnDisable()
    {
        Added -= OnReverceStackAdded;
    }

    protected override Vector3 CalculateAddEndPosition(Transform container, Transform stackable)
    {
        return Vector3.zero;
    }

    private void OnReverceStackAdded(Stackable stackable)
    {
        stackable.transform.SetSiblingIndex(0);

        var container = stackable.transform.parent;
        var childCount = container.childCount;
        var childs = new List<Transform>();
        for (int i = 0; i < childCount; i++)
            childs.Add(container.GetChild(i));

        Sort(childs);
    }

    protected override void Sort(List<Transform> unsortedTransforms)
    {
        var position = Vector3.zero;
        foreach (var item in unsortedTransforms)
        {
            item.transform.DOComplete(true);
            item.transform.DOLocalMove(position, _sortAnimationDuration);
            position += Vector3.up * (item.transform.localScale.y + _space);
        }
    }
}
