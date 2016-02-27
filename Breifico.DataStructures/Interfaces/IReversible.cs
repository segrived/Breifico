namespace Breifico.DataStructures.Interfaces
{
    public interface IReversible<T> : ILinkedList<T>
    {
        void Reverse();
    }
}
