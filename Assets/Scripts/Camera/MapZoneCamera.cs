using System.Collections;
using UnityEngine;
using Cinemachine;

public class MapZoneCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _cameras;

    public void Show()
    {
        StartCoroutine(ShowCoroutine(_cameras, 3f));
    }

    private IEnumerator ShowCoroutine(CinemachineVirtualCamera[] cameras, float delay)
    {
        foreach (var camera in cameras)
        {
            if (camera == null)
                continue;

            camera.Priority = 100;

            yield return new WaitForSeconds(delay);

            camera.Priority = 0;
        }
    }
}
