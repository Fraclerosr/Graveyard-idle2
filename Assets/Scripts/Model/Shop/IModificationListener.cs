
namespace BabyStack.Model
{
    public interface IModificationListener<T>
    {
        void OnModificationUpdate(T value);
    }
}