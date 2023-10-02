using UnityEngine;

public class Dollar : Stackable
{
    [SerializeField] private int _value = 1;
    [SerializeField] private Collider _collider;

    public override StackableType Type => StackableType.Dollar;
    public int Value => _value;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void DisableCollision()
    {
        _collider.enabled = false;
    }
}
