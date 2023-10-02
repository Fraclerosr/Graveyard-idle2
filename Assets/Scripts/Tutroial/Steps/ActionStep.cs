using System;

public class ActionStep : ITutorialStep
{
    private readonly Action _action;

    public ActionStep(Action action)
    {
        _action = action;
    }

    public bool Completed { get; private set; }

    public void Execute()
    {
        _action?.Invoke();
        Completed = true;
    }
}
