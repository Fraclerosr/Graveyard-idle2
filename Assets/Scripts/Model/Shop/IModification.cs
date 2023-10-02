
namespace BabyStack.Model
{
    public interface IModification<T>
    {
        public T CurrentModificationValue { get; }
    }
}