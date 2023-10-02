using UnityEngine;
using Cinemachine;

public class LevelCameraAnimation : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private InputSwither _input;

    public void LookTo(Transform lookObject)
    {
        _camera.Follow = lookObject;
        _input.Disable();
        _cameraAnimator.SetTrigger(CameraLevel2AnimatorParameters.ShowItem);
    }

    public void ResetLook()
    {
        _cameraAnimator.SetTrigger(CameraLevel2AnimatorParameters.ShowPlayer);
        _input.Enable();
    }
}
