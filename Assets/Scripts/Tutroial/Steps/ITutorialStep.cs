public interface ITutorialStep
{
    bool Completed { get; }

    void Execute();
}
