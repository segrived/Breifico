using System.Collections;
using System.Collections.Generic;

namespace Breifico.DataStructures.Interfaces
{
    public interface IMyList<T> : IEnumerable<T>, ICollection
    {
        T this[int index] { get; set; }

        void Add(T item);
        void AddRange(IEnumerable<T> items);
        void RemoveAt(int index);
    }
}
