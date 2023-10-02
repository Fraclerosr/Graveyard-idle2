using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;
    [SerializeField] private bool _randomTransition = false;

    public void Enter()
    {
        if (enabled == false)
        { 
            enabled = true;
            foreach (var transition in _transitions)
                transition.enabled = true;
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public State GetNextState()
    {
        if (_randomTransition)
            return GetRandomTransition();

        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }

    private State GetRandomTransition()
    {
        var transitions = new List<Transition>();

        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                transitions.Add(transition);
        }

        if (transitions.Count == 0)
            return null;

        return transitions[Random.Range(0, transitions.Count)].TargetState;
    }
}
