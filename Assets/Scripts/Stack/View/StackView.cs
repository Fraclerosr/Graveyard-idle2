using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class StackView : MonoBehaviour, IStackableContainer
{
    [SerializeField] private Transform _stackContainer;
    [SerializeField] private float _animationDuration;
    [SerializeField] private FloatSetting _scalePunch = new FloatSetting(true, 1.1f);
    [SerializeField] private FloatSetting _jumpPower = new FloatSetting(false, 0f);
    [SerializeField] private Vector3 _scaleMultiply = Vector3.one;

    private List<Transform> _transforms = new List<Transform>();

    public event Action<Stackable> Added;
    public event Action<Stackable> MoveEnded;
    public event Action Removed;

    public void Add(Stackable stackable)
    {
        Vector3 defaultScale = stackable.transform.localScale;

        stackable.transform.localScale = new Vector3(_scaleMultiply.x * defaultScale.x,
            _scaleMultiply.y * defaultScale.y, _scaleMultiply.z * defaultScale.z);

        Vector3 endPosition = CalculateAddEndPosition(_stackContainer, stackable.transform);
        Vector3 endRotation = CalculateEndRotation(_stackContainer, stackable.transform);


        stackable.transform.DOComplete(true);
        stackable.transform.parent = _stackContainer;

        stackable.transform.DOLocalMove(endPosition, _animationDuration).OnComplete(() => MoveEnded?.Invoke(stackable));
        stackable.transform.DOLocalRotate(endRotation, _animationDuration);

        if (_scalePunch.Enabled)
            stackable.transform.DOPunchScale(defaultScale * _scalePunch.Value, _animationDuration);
        if (_jumpPower.Enabled)
            stackable.transform.DOLocalJump(endPosition, _jumpPower.Value, 1, _animationDuration);

        _transforms.Add(stackable.transform);
        Added?.Invoke(stackable);
    }

    public void Remove(Stackable stackable)
    {
        stackable.transform.DOComplete(true);
        stackable.transform.parent = null;

        int removedIndex = _transforms.IndexOf(stackable.transform);
        _transforms.RemoveAt(removedIndex);
        OnRemove(stackable.transform);

        Sort(_transforms);

        Removed?.Invoke();
    }

    public float FindTopPositionY()
    {
        float topPositionY = 0f;

        foreach (var item in _transforms)
            if (item.position.y + item.localScale.y > topPositionY)
                topPositionY = item.position.y + item.localScale.y;

        return topPositionY;
    }

    protected virtual void OnRemove(Transform stackable)
    {
    }

    protected virtual Vector3 CalculateEndRotation(Transform container, Transform stackable)
    {
        return Vector3.zero;
    }

    protected abstract Vector3 CalculateAddEndPosition(Transform container, Transform stackable);
    protected abstract void Sort(List<Transform> unsortedTransforms);

    [Serializable]
    public class Setting<T>
    {
        [SerializeField] private bool _enabled;
        [SerializeField] private T _value;

        public Setting(bool enabled, T value)
        {
            _enabled = enabled;
            _value = value;
        }

        public bool Enabled => _enabled;
        public T Value => _value;
    }

    [Serializable]
    public class FloatSetting : Setting<float>
    {
        public FloatSetting(bool enabled, float value)
            : base(enabled, value)
        {
        }
    }

    [Serializable]
    public class VectorSetting : Setting<Vector3>
    {
        public VectorSetting(bool enabled, Vector3 value)
            : base(enabled, value)
        {
        }
    }
}

public interface IStackableContainer
{
    event Action<Stackable> Added;
    event Action Removed;

    float FindTopPositionY();
}