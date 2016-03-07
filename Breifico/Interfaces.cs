using System;

namespace Breifico
{
    public interface ISorter<T> where T : IComparable<T>
    {
        T[] Sort(T[] input);
    }
}
