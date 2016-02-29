using System.Collections;
using System.Collections.Generic;

namespace Breifico.DataStructures.Interfaces
{
    public interface IMyLinkedList<T> : IEnumerable<T>, ICollection
    {
        T this[int index] { get; set; }

        void Add(T item);
        void AddRange(IEnumerable<T> items);
        void Clear();
        void Insert(T item, int position);
        void Remove(int index);
    }
}
