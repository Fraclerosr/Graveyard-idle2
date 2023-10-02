using UnityEngine;

public class BoxQueueZone : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Vector2 _distance;
    
    private Transform[,] _matrix;

    public Vector2Int Size => _size;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int y = 0; y < _size.y; y++)
        {
            for (int x = 0; x < _size.x; x++)
            {
                var point = transform.TransformPoint(new Vector3(x * _distance.x, 0, y * _distance.y));
                Gizmos.DrawSphere(point, 0.5f);
            }
        }
    }
#endif

    private void Awake()
    {
        _matrix = new Transform[_size.y, _size.x];
    }

    public Vector3 Enqueue(Transform entity)
    {
        for (int y = 0; y < _size.y; y++)
        {
            for (int x = 0; x < _size.x; x++)
            {
                if (_matrix[y, x] == null)
                {
                    _matrix[y, x] = entity;
                    return transform.TransformPoint(new Vector3(x * _distance.x, 0, y * _distance.y));
                }
            }
        }

        return transform.TransformPoint(new Vector3(_size.x, 0, _size.y));
    }

    public void Remove(Transform entity)
    {
        for (int y = 0; y < _size.y; y++)
            for (int x = 0; x < _size.x; x++)
                if (_matrix[y, x] == entity)
                    _matrix[y, x] = null;
    }
}
