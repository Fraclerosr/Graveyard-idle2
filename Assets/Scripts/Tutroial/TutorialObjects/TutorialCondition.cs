public class TutorialCondition : ITutorialStepCondition
{
    public bool Completed { get; private set; }

    public void Complete()
    {
        Completed = true;
    }
}
