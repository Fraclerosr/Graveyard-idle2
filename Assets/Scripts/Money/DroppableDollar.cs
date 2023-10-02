using System;
using System.Collections;
using UnityEngine;

public class DroppableDollar : DropableItem
{
    [SerializeField] private float _canTakeDelay;
    [SerializeField] private Dollar _dollar;

    private bool _canTake;
    private bool _taken;
    
    public bool CanTake => !_taken && _canTake;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(UnlockTake());
    }

    public IEnumerator UnlockTake()
    {
        yield return new WaitForSeconds(_canTakeDelay);
        _canTake = true;
    }

    public Dollar Take()
    {
        if (!CanTake)
            throw new InvalidOperationException();
        
        _taken = true;
        DisableGravity();
        
        return _dollar;
    }
}