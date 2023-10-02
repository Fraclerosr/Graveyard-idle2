public class ShowObjectStep : ITutorialStep
{
    private readonly TutorialCamera _camera;
    private readonly string _trigger;

    public ShowObjectStep(TutorialCamera camera, string trigger)
    {
        _camera = camera;
        _trigger = trigger;
    }

    public bool Completed { get; private set; }

    public void Execute()
    {
        _camera.Show(_trigger);

        Completed = true;
    }
}
