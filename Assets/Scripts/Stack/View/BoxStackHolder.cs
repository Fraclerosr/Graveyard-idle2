using System.Collections.Generic;
using UnityEngine;

public class BoxStackHolder : StackView
{
    [Space(15f)]
    [SerializeField] private Vector3Int _size = Vector3Int.one;
    [SerializeField] private Vector3 _distanceBetweenObjects = Vector3.one;
#if UNITY_EDITOR
    [SerializeField] private bool _canDrawGizmos = true;
#endif

    private Transform[,,] _matrix;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_canDrawGizmos == false)
            return;

        Gizmos.color = Color.red;

        for (int y = 0; y < _size.y; y++)
        {
            for (int x = 0; x < _size.x; x++)
            {
                for (int z = 0; z < _size.z; z++)
                {
                    var position = transform.TransformPoint(
                        new Vector3(x * _distanceBetweenObjects.x, y * _distanceBetweenObjects.y, z * _distanceBetweenObjects.z));
                    Gizmos.DrawSphere(position, 0.2f);
                }
            }
        }
    }
#endif

    private void Awake()
    {
        _matrix??= new Transform[_size.x, _size.y, _size.z];
    }

    protected override Vector3 CalculateAddEndPosition(Transform container, Transform stackable)
    {
        var index = FindFreeIndex();
        var position = new Vector3(index.x * _distanceBetweenObjects.x, index.y * _distanceBetweenObjects.y, index.z * _distanceBetweenObjects.z);

        _matrix[index.x, index.y, index.z] = stackable;

        return position;
    }

    protected override void OnRemove(Transform stackable)
    {
        for (int y = _size.y - 1; y >= 0; y--)
        {
            for (int x = _size.x - 1; x >= 0; x--)
            {
                for (int z = _size.z - 1; z >= 0; z--)
                {
                    if (_matrix[x, y, z] != null && _matrix[x, y, z] == stackable)
                    {
                        _matrix[x, y, z] = null;
                    }
                }
            }
        }
    }

    protected override void Sort(List<Transform> unsortedTransforms)
    {
        return;
    }

    private Vector3Int FindFreeIndex()
    {
        _matrix ??= new Transform[_size.x, _size.y, _size.z];

        for (int y = 0; y < _size.y; y++)
        {
            for (int x = 0; x < _size.x; x++)
            {
                for (int z = 0; z < _size.z; z++)
                {
                    if (_matrix[x, y, z] == null)
                    {
                        return new Vector3Int(x, y, z);
                    }
                }
            }
        }

        return _size;
    }
}
