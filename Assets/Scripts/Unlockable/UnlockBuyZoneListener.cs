using UnityEngine;
using UnityEngine.Events;

public class UnlockBuyZoneListener : UnlockableObject
{
    public event UnityAction<bool, string> Unlocked;

    public bool IsUnlocked { get; private set; } = false;

    public override GameObject Unlock(Transform parent, bool onLoad, string guid)
    {
        IsUnlocked = true;
        Unlocked?.Invoke(onLoad, guid);
        return gameObject;
    }
}
