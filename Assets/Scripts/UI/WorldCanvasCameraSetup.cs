using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class WorldCanvasCameraSetup : MonoBehaviour
{
    private static Camera _camera;
    private Canvas _canvas;

    private void Start()
    {
        _camera ??= Camera.main;

        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = _camera;
    }
}
