using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyStackHolder : StackView
{
    [Space(15f)]
    [SerializeField] private Vector2Int _size;
    [SerializeField] private int _height;
    [SerializeField] private float _xDistance;
    [SerializeField] private float _yDistance;

    private MoneyStack[,] _matrix;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int y = 0; y < _size.y; y++)
        {
            for (int x = 0; x < _size.x; x++)
            {
                var position = transform.TransformPoint(new Vector3(x * _xDistance, 0, y * _yDistance));
                Gizmos.DrawSphere(position, 0.2f);
            }
        }
    }
#endif

    private void Awake()
    {
        _matrix = new MoneyStack[_size.x, _size.y];

        for (int y = 0; y < _size.y; y++)
            for (int x = 0; x < _size.x; x++)
                _matrix[x, y] = new MoneyStack();
    }

    protected override Vector3 CalculateAddEndPosition(Transform container, Transform stackable)
    {
        var x = GetRandomIndex(_size.x);
        var y = GetRandomIndex(_size.y);

        _matrix[x, y].Add(stackable);

        var count = _matrix[x, y].Count < _height ? _matrix[x, y].Count : _height;

        var horizontalOffset = new Vector3(x * _xDistance, 0, y * _yDistance) + new Vector3(Random.Range(0, 0.1f), 0, Random.Range(0, 0.1f));
        return Vector3.up * count * 0.15f + horizontalOffset;
    }

    protected override Vector3 CalculateEndRotation(Transform container, Transform stackable)
    {
        return Vector3.up * Random.Range(-10, 10);
    }

    protected override void OnRemove(Transform stackable)
    {
        foreach (var stack in _matrix)
            if (stack.Remove(stackable))
                break;
    }

    protected override void Sort(List<Transform> unsortedTransforms)
    {
        return;
    }

    private int GetRandomIndex(int maxIndex)
    {
        var half = Mathf.Ceil((float)maxIndex / 2f);

        int sum = 0;
        for (int i = 1; i <= half; i++)
            sum += i;

        var rate = 1f / sum;
        var random = Random.Range(0f, 1f);

        var sumRate = rate;
        int index;
        for (index = 0; index < half; index++)
        {
            if (sumRate >= random)
                break;

            sumRate += rate * (index + 2);
        }

        if (Random.Range(0f, 1f) > 0.5f)
            return maxIndex - index - 1;
        else
            return index;
    }

    private class MoneyStack
    {
        private List<Transform> _stack;

        public MoneyStack()
        {
            _stack = new List<Transform>();
        }

        public int Count => _stack.Count;

        public void Add(Transform stackable)
        {
            _stack.Add(stackable);
        }

        public bool Remove(Transform stackable)
        {
            return _stack.Remove(stackable);
        }
    }
}
