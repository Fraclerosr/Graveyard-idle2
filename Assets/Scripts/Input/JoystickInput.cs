using System;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private Joystick _joystick;

    public bool LastFrameMoving { get; private set; }

    public bool Moving => _joystick.Direction != Vector2.zero;
    public bool StoppedMoving => !Moving && LastFrameMoving;
    
    public event Action Moved;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }

    private void OnDisable()
    {
        _movement?.Stop();
    }

    public void Update()
    {
        if (!Moving)
        {
            _movement.Stop();
            return;
        }

        Vector3 rawDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
        _movement.Move(rawDirection);

        Moved?.Invoke();
    }

    private void LateUpdate()
    {
        LastFrameMoving = Moving;
    }
}
