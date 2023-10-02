using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransformListStackHolder : StackView
{
    [SerializeField] private List<Transform> _placePoints;
    
    private readonly Dictionary<Transform, Transform> _placedObjects = new Dictionary<Transform, Transform>();

    protected override Vector3 CalculateAddEndPosition(Transform container, Transform stackable)
    {
        Transform placeTransform = GetNextFreePlace();

        return placeTransform.localPosition;
    }

    protected override Vector3 CalculateEndRotation(Transform container, Transform stackable)
    {
        Transform placeTransform = GetNextFreePlace();
        _placedObjects.Add(placeTransform, stackable);

        return placeTransform.localRotation.eulerAngles;
    }

    public Transform GetNextFreePlace()
    {
        return _placePoints.FirstOrDefault(transform => _placedObjects.ContainsKey(transform) == false);
    }

    protected override void OnRemove(Transform stackable)
    {
        _placedObjects.Remove(_placedObjects.First(pair => pair.Value == stackable).Key);
    }

    protected override void Sort(List<Transform> unsortedTransforms)
    {
        return;
    }
}