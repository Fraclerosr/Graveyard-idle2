namespace BabyStack.Settings
{
    public interface ISetting
    {
        bool IsEnable { get; }

        void Enable();
        void Disable();
    }
}
