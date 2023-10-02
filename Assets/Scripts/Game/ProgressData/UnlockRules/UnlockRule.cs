using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UnlockRule", menuName = "Unlock Rules/New Unlock Rule", order = 51)]
public class UnlockRule : ScriptableObject
{
    [SerializeField] private int _targetProgress;
    [SerializeField] private Progress _progress;
    [SerializeField] private Sprite _icon;

    public virtual bool ForRewarded => false;
    public virtual int CurrentProgress => _progress.CurrentProgress;
    public virtual int TargetProgress => _targetProgress;
    public bool CanUnlock => CurrentProgress >= TargetProgress;
    public Sprite Icon => _icon;

   

    public void AddUpdateListener(UnityAction action)
    {
        if (_progress)
        {
            _progress.Updated += action;
           
        }
           
    }

    public void RemoveUpdateListener(UnityAction action)
    {
        if (_progress)
        {
            _progress.Updated -= action;
        }
            
    }
}
