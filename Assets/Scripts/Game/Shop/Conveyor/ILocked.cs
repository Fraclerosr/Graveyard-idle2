using UnityEngine;

public class LockedGameObject : MonoBehaviour
{
    public object Locked { get; private set; }

    public void Lock(object locked)
    {
        Locked = locked;
    }

    public void Unlock()
    {
        Locked = null;
    }
}
