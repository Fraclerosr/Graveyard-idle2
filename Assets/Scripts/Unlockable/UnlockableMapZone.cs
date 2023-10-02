using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MapZoneCamera))]
public class UnlockableMapZone : UnlockableObject
{
    [SerializeField] private GameObject _mapRoot;
    [SerializeField] private float _unlockDuration = 1f;

    private MapZoneCamera _mapZoneCamera;

    private void Awake()
    {
        _mapZoneCamera = GetComponent<MapZoneCamera>();
    }

    public override GameObject Unlock(Transform parent, bool onLoad, string guid)
    {
        _mapRoot.transform.localScale = Vector3.zero;
        _mapRoot.SetActive(true);

        _mapRoot.transform.DOScale(1f, _unlockDuration);

        if (onLoad == false)
            _mapZoneCamera.Show();
        
        return _mapRoot;
    }
}
