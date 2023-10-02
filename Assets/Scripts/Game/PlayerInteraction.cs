using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Rigidbody _body;

    public void EnableInteraction()
    {
        _collider.enabled = true;
        _body.isKinematic = false;
    }

    public void DisableInteraction()
    {
        _collider.enabled = false;
        _body.isKinematic = true;
    }
}
