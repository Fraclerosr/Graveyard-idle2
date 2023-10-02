using UnityEngine;

public class AICharacter : MonoBehaviour
{
    [SerializeField] private State _firstState;
    [SerializeField] private AIMovement _movement;
    [SerializeField] private DoctorAnimation _animation;
    [SerializeField] private StackPresenter _stack;

    private State _currentState;
    
    public CharacterReferences References { get; private set; }
    public StackPresenter Stack => _stack;

    public void Init(CharacterReferences characterReferences)
    {
        References = characterReferences;
    }

    private void Update()
    {
        _animation.SetSpeed(_movement.NormalizedSpeed);

        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    public void Run()
    {
        Transit(_firstState);
    }

    public void Warp(Vector3 position)
    {
        _movement.Warp(position);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }
}