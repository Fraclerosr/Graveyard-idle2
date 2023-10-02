using UnityEngine;
using UnityEngine.Events;

public abstract class Progress : ScriptableObject
{
    [SerializeField] private string _saveKey;

    public abstract event UnityAction Updated;

    protected string SaveKey => _saveKey;
    public abstract int CurrentProgress { get; }

    private void OnEnable()
    {
        Load();
    }

    public abstract void Load();
    public abstract void Save();
}
