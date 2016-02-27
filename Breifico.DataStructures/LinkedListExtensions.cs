using System.Collections;
using System.Collections.Generic;
using Breifico.DataStructures.Interfaces;

namespace Breifico.DataStructures
{
    public static class LinkedListExtensions
    {
        public static void AddRange<T>(this ILinkedList<T> src, IEnumerable<T> coll) {
            foreach (var item in coll) {
                src.Add(item);
            }
        }

        public static void AddRange<T>(this ILinkedList<T> src, params T[] coll) {
            src.AddRange((IEnumerable<T>)coll);
        }
    }
}
