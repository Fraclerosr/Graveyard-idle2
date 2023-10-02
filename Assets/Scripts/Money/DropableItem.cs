using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class DropableItem : MonoBehaviour
{
    [SerializeField] private float _horizontalForce = 50;
    [SerializeField] private Collider _collider;

    private Rigidbody _rigidbody;

    protected virtual void Awake() => 
        _rigidbody = GetComponent<Rigidbody>();

    public void Push()
    {
        var random = Random.insideUnitCircle;
        var shift = new Vector3(random.x, 0, random.y);
        Push(Vector3.up + shift * _horizontalForce);
    }

    public void Push(Vector3 direction)
    {
        _rigidbody.AddForce(direction, ForceMode.VelocityChange);
        StartCoroutine(DisableBodyWhenReady());
    }

    public void DisableGravity()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        _collider.enabled = false;
    }

    private IEnumerator DisableBodyWhenReady()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => _rigidbody.velocity == Vector3.zero);

        _rigidbody.isKinematic = true;
    }
}