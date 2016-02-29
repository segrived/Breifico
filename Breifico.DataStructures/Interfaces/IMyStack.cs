using System.Collections;
using System.Collections.Generic;

namespace Breifico.DataStructures.Interfaces
{
    public interface IMyStack<T> : IEnumerable<T>, ICollection
    {
        void Clear();
        T Pop();
        T Peek();
        void Push(T value);
    }
}
