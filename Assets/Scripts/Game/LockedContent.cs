using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LockedContent : ScriptableObject, ILocationContent
{
    public bool Unlocked => PlayerPrefs.HasKey(UnlockSaveKey);
    public abstract IEnumerable<ICollectionContent> Content { get; }
    protected abstract string UnlockSaveKey { get; }

    public void Unlock()
    {
        PlayerPrefs.SetInt(UnlockSaveKey, 1);
    }
}
