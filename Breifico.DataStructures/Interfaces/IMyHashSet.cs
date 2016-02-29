using System.Collections;
using System.Collections.Generic;

namespace Breifico.DataStructures.Interfaces
{
    interface IMyHashSet<T> : IEnumerable<T>, ICollection
    {
        bool Add(T item);
        void AddRange(IEnumerable<T> items);
        bool Contains(T item);
    }
}
