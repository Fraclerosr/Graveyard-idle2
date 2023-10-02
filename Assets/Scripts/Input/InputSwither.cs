using System.Collections;
using UnityEngine;

public class InputSwither : MonoBehaviour
{
    [SerializeField] private JoystickInput _joystickInput;
    [SerializeField] private Canvas _joystickCanvas;
    [SerializeField] private PlayerInteraction _playerInteraction;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Disable();
        if (Input.GetKeyDown(KeyCode.H))
            Enable();
    }

    public void Disable()
    {
        DisableOnlyJoystick();
        _playerInteraction.DisableInteraction();
    }

    public void DisableOnlyJoystick()
    {
        _joystickInput.enabled = false;
        _joystickCanvas.enabled = false;
    }

    public void Enable()
    {
        _joystickInput.enabled = true;
        _joystickCanvas.enabled = true;
        _playerInteraction.EnableInteraction();
    }
}
