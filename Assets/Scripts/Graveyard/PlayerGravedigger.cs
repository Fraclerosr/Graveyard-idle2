using UnityEngine;

public class PlayerGravedigger : MonoBehaviour
{
    [SerializeField] private Gravedigger _gravedigger;
    [SerializeField] private InputSwither _inputSwitcher;

    private void OnEnable()
    {
        _gravedigger.StartedDigging += OnStartedDigging;
        _gravedigger.StoppedDigging += OnStoppedDigging;
    }

    private void OnDisable()
    {
        _gravedigger.StartedDigging -= OnStartedDigging;
        _gravedigger.StoppedDigging -= OnStoppedDigging;
    }

    private void OnStoppedDigging() => 
        _inputSwitcher.Enable();

    private void OnStartedDigging() => 
        _inputSwitcher.Disable();
}
