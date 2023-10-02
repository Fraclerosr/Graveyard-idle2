using System.Collections.Generic;
using UnityEngine;

public class SingleLayerStackHolder : StackView
{
    protected override Vector3 CalculateAddEndPosition(Transform container, Transform stackable)
    {
        return container.localPosition;
    }

    protected override void Sort(List<Transform> unsortedTransforms)
    {
        return;
    }
}
