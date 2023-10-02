using System;
using System.Threading.Tasks;

public class SequenceSteps : ITutorialStep
{
    private readonly ITutorialStep[] _tutorialSteps;
    private readonly ITutorialStepCondition _skipCondition;
    private bool _executing;

    public SequenceSteps(ITutorialStep[] tutorialSteps, ITutorialStepCondition skipCondition = null)
    {
        _tutorialSteps = tutorialSteps;
        _skipCondition = skipCondition;
    }

    public bool Completed { get; private set; }

    public static SequenceSteps Create(ITutorialStepCondition skipCondition, params ITutorialStep[] steps)
    {
        return new SequenceSteps(steps, skipCondition);
    }

    public static SequenceSteps Create(params ITutorialStep[] steps)
    {
        return new SequenceSteps(steps);
    }

    public async void Execute()
    {
        if (_executing)
            throw new InvalidOperationException("Already execute");

        _executing = true;

        if (_skipCondition != null && _skipCondition.Completed)
        {
            Completed = true;
            return;
        }

        foreach (var step in _tutorialSteps)
        {
            step.Execute();
            await Wait(step);
        }

        Completed = true;
    }

    private async Task Wait(ITutorialStep step)
    {
        while (step.Completed == false)
            await Task.Yield();
    }
}
