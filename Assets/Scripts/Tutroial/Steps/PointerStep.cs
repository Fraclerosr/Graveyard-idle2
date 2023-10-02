using UnityEngine;

public class PointerStep : ITutorialStep
{
    private readonly ITutorialArrow[] _pointers;
    private readonly Transform _target;

    public PointerStep(Transform target = null, params ITutorialArrow[] pointers)
    {
        _pointers = pointers;
        _target = target;
    }

    public bool Completed { get; private set; }

    public void Execute()
    {
        
        foreach (var pointer in _pointers)
            pointer.SetTarget(_target);

        Completed = true;
    }
}
