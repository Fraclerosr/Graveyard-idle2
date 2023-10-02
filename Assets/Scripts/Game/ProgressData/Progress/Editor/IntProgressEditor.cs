using UnityEditor;
using System.Reflection;
using UnityEngine;

[CustomEditor(typeof(IntProgress))]
public class IntProgressEditor : Editor
{
    private const string LockKey = "LockEditProgressKey";

    private IntProgress _progress;
    private string _property;
    private bool _lock;

    private void Awake()
    {
        _progress = target as IntProgress;
        _progress.Load();
        _lock = EditorPrefs.GetBool(LockKey, false);
    }

    public override void OnInspectorGUI()
    {
        _lock = EditorGUILayout.Toggle("Lock edit", _lock);

        EditorPrefs.SetBool(LockKey, _lock);
        if (_lock)
            GUI.enabled = false;

        base.OnInspectorGUI();
        GUI.enabled = true;

        GUILayout.Space(10);
        var value = _progress.CurrentProgress;

        GUI.enabled = false;
        EditorGUILayout.TextField("Progress: " + value.ToString());
        GUI.enabled = true;
    }
}
