using System.Linq;
using UnityEditor;
using UnityEngine;

public class WorldCanvasCameraSetupEditor : EditorWindow
{
    [MenuItem("Window/Setup World Canvas Camera")]
    static void Init()
    {
        WorldCanvasCameraSetupEditor window = (WorldCanvasCameraSetupEditor)EditorWindow.GetWindow(typeof(WorldCanvasCameraSetupEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Setup world canvases", EditorStyles.boldLabel);
        if (GUILayout.Button("Setup"))
        {
            var worldCanvases = FindObjectsOfType<Canvas>(true).Where(canvas => canvas.renderMode == RenderMode.WorldSpace);
            foreach (var canvas in worldCanvases)
            {
                canvas.worldCamera = Camera.main;
                EditorUtility.SetDirty(canvas.gameObject);
            }
        }
    }
}
