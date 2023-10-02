using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ListProgress))]
public class ListProgressEditor : Editor
{
    private const string LockKey = "LockEditProgressKey";

    private ListProgress _progress;
    private bool _lock;

    private void Awake()
    {
        _progress = target as ListProgress;
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

        serializedObject.Update();

        try
        {
            GUI.enabled = false;
            EditorGUILayout.TextField("List Progress: " + _progress.CurrentProgress);

            GUILayout.Space(15);

            EditorGUILayout.TextField("Save Data:");
            EditorGUI.indentLevel++;

            foreach (var data in _progress.Data)
                EditorGUILayout.TextField(data);

            EditorGUI.indentLevel--;
            GUI.enabled = true;
        }
        catch (Exception _) { }
    }
}
