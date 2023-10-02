using UnityEngine;

public class MoveTutorial : MonoBehaviour
{
    [SerializeField] private JoystickInput _input;

    private void OnEnable()
    {
        _input.Moved += OnMoved;
    }

    private void OnDisable()
    {
        _input.Moved -= OnMoved;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    private void OnMoved()
    {
        gameObject.SetActive(false);
    }
}
