using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    private void Start()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif

        Application.targetFrameRate = 60;
    }
}
