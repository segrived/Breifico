using System.Collections.Generic;

namespace Breifico.DataStructures.Interfaces
{
    public interface ILinkedList<T> : IEnumerable<T>
    {
        int Count { get; }

        void Add(T value);
        void Insert(T value, int position);
        T Get(int index);
        void Remove(int index);
        void Clear();
    }
}