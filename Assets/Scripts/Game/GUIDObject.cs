using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GUIDObject : MonoBehaviour
{
#if UNITY_EDITOR
    //[ReadOnly]
#endif
    [SerializeField] private string _guid;

    public string GUID => _guid;

#if UNITY_EDITOR
    [ContextMenu("Regenerate GUID")]
    public void RegenerateGUID()
    {
        _guid = Guid.NewGuid().ToString();
        EditorUtility.SetDirty(gameObject);
    }
#endif
}
