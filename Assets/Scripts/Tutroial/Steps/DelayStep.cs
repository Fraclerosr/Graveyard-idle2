using System;
using System.Threading.Tasks;

public class DelayStep : ITutorialStep
{
    private readonly float _duration;
    private bool _executing;

    public DelayStep(float duration)
    {
        _duration = duration;
    }

    public bool Completed { get; private set; }

    public async void Execute()
    {
        if (_executing)
            throw new InvalidOperationException("Already execute");

        await Delay(_duration);
        Completed = true;
    }

    private async Task Delay(float duration)
    {
        _executing = true;

        await Task.Delay(TimeSpan.FromSeconds(duration));

        _executing = false;
    }
}
