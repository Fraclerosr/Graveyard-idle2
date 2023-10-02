#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(BuyZonePresenter))]
public class BuyZonePresenterEditor : Editor
{
    private BuyZonePresenter _buyZonePresenter;

    private void Awake()
    {
        _buyZonePresenter = target as BuyZonePresenter;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var allGUIDObjects = FindObjectsOfType<GUIDObject>();

        foreach (var otherGUID in allGUIDObjects)
        {
            if (otherGUID == _buyZonePresenter)
                continue;

            if (otherGUID.GUID == _buyZonePresenter.GUID)
            {
                EditorGUILayout.HelpBox("This GUID already exist in other object. Try regenerate guid", MessageType.Warning);
                break;
            }
        }
    }

    private void OnSceneGUI()
    {
        //if (_buyZonePresenter.transform.hasChanged == false)
        //    return;

        //var unlockable = _buyZonePresenter.UnlockableObject;
        //if (unlockable is UnlockableMapZone)
        //    return;

        //var yPos = unlockable.transform.position.y;
        //unlockable.transform.position = new Vector3(_buyZonePresenter.transform.position.x, yPos, _buyZonePresenter.transform.position.z);

        //if (GUI.changed)
        //    EditorUtility.SetDirty(_buyZonePresenter);
    }
}
#endif