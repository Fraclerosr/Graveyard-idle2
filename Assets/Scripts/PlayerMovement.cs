using UnityEngine;
using BabyStack.Model;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IModificationListener<float>
{
    [SerializeField] private DoctorAnimation _animation;
    [SerializeField] private Transform _playerModel;
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private float _speedRate = 1f;
    private float _flySpeedRate = 1f;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        _playerModel.LookAt(_playerModel.position + direction);
        _rigidbody.velocity = direction * _speed * _speedRate * _flySpeedRate;

        _animation.SetSpeed(direction.magnitude);
        IsMoving = true;
    }

    public void Stop()
    {
        if (_rigidbody != null)
            _rigidbody.velocity = Vector3.zero;

        _animation.SetSpeed(0);
        IsMoving = false;
    }

    public void OnModificationUpdate(float value)
    {
        _speedRate = value;
    }

    public void SetFlySpeedRate(float rate)
    {
        if (rate <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(rate));

        _flySpeedRate = rate;
    }
}
