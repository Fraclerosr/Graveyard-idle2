#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ObjectsLayout : MonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private float _space;

    private int _childCount;

    private void OnValidate()
    {
        UpdatePositions();
    }

    private void OnEnable()
    {
        _childCount = transform.childCount;
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    private void OnDisable()
    {
        EditorApplication.hierarchyChanged -= OnHierarchyChanged;
    }

    private void OnHierarchyChanged()
    {
        if (transform.childCount == _childCount)
            return;

        _childCount = transform.childCount;
        UpdatePositions();
    }

    private void UpdatePositions()
    {
        var position = Vector3.zero;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = position;

            if (_type == Type.Horizontal)
                position += Vector3.right * _space;
            if (_type == Type.Vertical)
                position += Vector3.forward * _space;
        }

        EditorUtility.SetDirty(this);
    }

    public enum Type
    {
        Horizontal,
        Vertical
    }
}

#endif