using System.Collections.Generic;

namespace Breifico.DataStructures.Interfaces
{
    interface IMyDoublyLinkedList<T> : IMyLinkedList<T>
    {
        IEnumerable<T> ReverseEnumerate();
    }
}
