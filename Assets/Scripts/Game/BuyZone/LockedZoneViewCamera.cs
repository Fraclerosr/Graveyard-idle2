using System.Collections;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class LockedZoneViewCamera : MonoBehaviour
{
    [SerializeField] private LockedBuyZonePresenter _buyZone;
    [SerializeField] private float _beforeDelay;

    private CinemachineVirtualCamera _camera;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        _buyZone.FirstTimeAviabled += OnAviabled;
    }

    private void OnDisable()
    {
        _buyZone.FirstTimeAviabled -= OnAviabled;
    }

    private void OnAviabled()
    {
        StartCoroutine(ShowZone());
    }

    private IEnumerator ShowZone()
    {
        yield return new WaitForSeconds(_beforeDelay);
        _camera.Priority = 100;
        yield return new WaitForSeconds(4f);
        _camera.Priority = 0;
    }
}
