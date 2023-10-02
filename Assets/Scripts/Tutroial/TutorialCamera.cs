using UnityEngine;
using Cinemachine;

public class TutorialCamera : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CinemachineVirtualCamera _customerCamera;
    [SerializeField] private CinemachineVirtualCamera _secondCustomerCamera;

    [Space(10)]
    [SerializeField] private InputSwither _inputSwither;

    public void InitializeCustomerCamera(Transform bookCourier) => _customerCamera.Follow = bookCourier;
    public void InitializeSecondCustomerCamera(Transform deskCamera) => _secondCustomerCamera.Follow = deskCamera;

    public void Show(string trigger)
    {
        if (trigger == CameraAnimatorParameters.ShowPlayer)
            _inputSwither.Enable();
        else
            _inputSwither.Disable();

        ResetAllTrigger();
        _animator.SetTrigger(trigger);
    }

    private void ResetAllTrigger()
    {
        _animator.ResetTrigger(CameraAnimatorParameters.ShowCustomer);
        _animator.ResetTrigger(CameraAnimatorParameters.ShowPlayer);
        _animator.ResetTrigger(CameraAnimatorParameters.ShowConveyor);
        _animator.ResetTrigger(CameraAnimatorParameters.ShowCustomer);
        _animator.ResetTrigger(CameraAnimatorParameters.ShowSecondCustomer);
        _animator.ResetTrigger(CameraAnimatorParameters.ShowGrave);
        _animator.ResetTrigger(CameraAnimatorParameters.ShowItemProducer);
    }
}
