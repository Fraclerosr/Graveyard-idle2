using UnityEngine;

public class DoctorAnimation : MonoBehaviour
{
    [SerializeField] private StackPresenter _playerStack;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        OnAwake();
    }

    private void OnEnable()
    {
        _playerStack.Added += OnAdded;
        _playerStack.BecameEmpty += OnBecameEmpty;
    }

    private void OnDisable()
    {
        _playerStack.Added -= OnAdded;
        _playerStack.BecameEmpty -= OnBecameEmpty;
    }

    public void SetSpeed(float normalizedSpeed)
    {
        if (_animator)
            _animator.SetFloat(AnimationParams.Speed, normalizedSpeed);
    }

    protected virtual void OnAwake() { }

    protected void SetFlying(bool value)
    {
        _animator.SetBool(AnimationParams.Flying, value);
    }

    public void UpdateHolding()
    {
        if(_playerStack.Count == 0)
            StopHolding();
        else
            Hold();
    }

    public void StopHolding()
    {
        _animator.SetLayerWeight(1, 0f);
    }

    public void StartDigging()
    {
        _animator.SetBool("isDig", true);
    }

    public void StopDigging()
    {
        _animator.SetBool("isDig", false);
    }

    private void OnAdded(Stackable _)
    {
        Hold();
    }

    private void OnBecameEmpty()
    {
        StopHolding();
    }

    private void Hold()
    {
        _animator.SetLayerWeight(1, 1f);
    }


    private static class AnimationParams
    {
        public static readonly string Speed = nameof(Speed);
        public static readonly string Idle = nameof(Idle);
        public static readonly string Flying = nameof(Flying);
    }
}
