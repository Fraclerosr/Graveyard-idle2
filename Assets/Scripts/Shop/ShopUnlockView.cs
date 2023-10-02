using System;
using UnityEngine;

public class ShopUnlockView : MonoBehaviour
{
    private static readonly int Open = Animator.StringToHash("Open");

    [SerializeField] private ShopUnlocker _shopUnlocker;
    [SerializeField] private Animator _modelAnimator;

    private bool _active;

    private void OnEnable()
    {
        if(_shopUnlocker == null)
            return;
        
        if (_shopUnlocker.IsUnlocked)
        {
            OnUnlocked();
            return;
        }

        _shopUnlocker.Unlocked += OnUnlocked;
    }

    private void OnDisable()
    {
        if(_shopUnlocker == null)
            return;
        
        _shopUnlocker.Unlocked -= OnUnlocked;
    }

    public void OnUnlocked()
    {
        if(_active)
            return;
        
        _modelAnimator.SetTrigger(Open);
        _active = true;
    }
}