using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuyZoneSettingWindow : EditorWindow
{
    private UnlockableObject _unlockableObject;
    private Transform _container;
    private List<BuyZonePresenter> _presenters;

    [MenuItem("Window/BuyZoneSettings")]
    public static void OpenWindow()
    {
        var window = GetWindow<BuyZoneSettingWindow>("BuyZoneSettings");
    }

    private void OnGUI()
    {
        //_unlockableObject = EditorGUILayout.ObjectField("Template", _unlockableObject, typeof(UnlockableObject)) as UnlockableObject;
        //_container = EditorGUILayout.ObjectField("Container", _container, typeof(Transform)) as Transform;
        //_presenters = EditorGUILayout.ObjectField("Container", _presenters, typeof(List<BuyZonePresenter>)) as List<BuyZonePresenter>;

        GUILayout.FlexibleSpace();

       
    }

}
