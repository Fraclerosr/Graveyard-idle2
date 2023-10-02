using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReferenceObjectList<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private UnlockableReference _unlockReference;

    private List<T> _data = new List<T>();

    public event UnityAction<T, bool, string> Unlocked;

    public IEnumerable<T> Data => _data;
    public int Count => _data.Count;

    private void OnEnable()
    {
        _unlockReference.Unlocked += OnUnlocked;
        Enabled();
    }

    private void OnDisable()
    {
        _unlockReference.Unlocked -= OnUnlocked;
        Disabled();
    }

    private void OnUnlocked(MonoBehaviour reference, bool onLoad, string guid)
    {
        _data.Add(reference as T);
        Unlocked?.Invoke(reference as T, onLoad, guid);

        AfterUnlocked(reference as T, onLoad, guid);
    }

    protected virtual void AfterUnlocked(T reference, bool onLoad, string guid) { }
    protected virtual void Enabled() { }
    protected virtual void Disabled() { }
}
