using System.Collections;
using UnityEngine;

public class UnlockableMapZoneList : UnlockableObject
{
    [SerializeField] private float _showDelay = 3f;
    [SerializeField] private UnlockableMapZone[] _mapZones;

    public override GameObject Unlock(Transform parent, bool onLoad, string guid)
    {
        StartCoroutine(UnlockZones(parent, onLoad, guid));

        return gameObject;
    }

    private IEnumerator UnlockZones(Transform parent, bool onLoad, string guid)
    {
        var delay = onLoad ? null : new WaitForSeconds(_showDelay);
        foreach (var mapZone in _mapZones)
        {
            mapZone.Unlock(parent, onLoad, guid);
            yield return delay;
        }
    }
}
