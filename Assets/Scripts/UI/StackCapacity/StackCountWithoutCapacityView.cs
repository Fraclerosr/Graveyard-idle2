public class StackCountWithoutCapacityView : StackCountView
{
    protected override string Format(int currentCount, int capacity) => $"{currentCount}";
}
