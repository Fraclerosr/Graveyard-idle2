using System.Threading.Tasks;

public class WaitConditionStep : ITutorialStep
{
    private readonly ITutorialStepCondition _condition;

    public WaitConditionStep(ITutorialStepCondition condition)
    {
        _condition = condition;
    }

    public bool Completed { get; private set; }

    public async void Execute()
    {
        await Wait(_condition);
        Completed = true;
    }

    private async Task Wait(ITutorialStepCondition condition)
    {
        while (condition.Completed == false)
            await Task.Yield();
    }
}
