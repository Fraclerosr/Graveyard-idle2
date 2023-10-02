using UnityEngine;
using MoreMountains.NiceVibrations;
using BabyStack.Settings;

public abstract class BaseHaptics : MonoBehaviour
{
    private Vibration _vibration;
    private int _vibrationCount = 0;

    private void Awake()
    {
        _vibration = new Vibration();
    }

    private void Update()
    {
        _vibrationCount = 0;
    }

    protected void Vibrate()
    {
        if (_vibration.IsEnable && _vibrationCount == 0)
        {
            MMVibrationManager.Haptic(HapticTypes.Selection);
            _vibrationCount++;
        }
    }
}