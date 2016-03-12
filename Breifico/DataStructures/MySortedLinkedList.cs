using System;
using System.Diagnostics;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Сортированный связный список
    /// </summary>
    /// <typeparam name="T">Тип элементов в сортированном связном списке
    /// Указанный тип должен имлементировать интерфейс IComparable</typeparam>
    [DebuggerDisplay("MySortedLinkedList<T>: {Count} element(s)")]
    public class MySortedLinkedList<T> : MyLinkedList<T> where T : IComparable<T>
    {
        /// <summary>
        /// Добавляет элемент в коллекцию по нужному индексу, чтобы держать список
        /// в сортированном порядке
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public override void Add(T item) {
            var tempNode = this.HeadNode;
            int index = 0;
            while (tempNode != null && tempNode.Value.CompareTo(item) == -1) {
                tempNode = tempNode.Next;
                index++;
            }
            base.Insert(index, item);
        }

        #region Unsupported methods
        public override void AddAfter(T item) {
            throw new NotSupportedException("Please use only Add method");
        }

        public override void AddFirst(T item) {
            throw new NotSupportedException("Please use only Add method");
        }

        public override void AddLast(T item) {
            throw new NotSupportedException("Please use only Add method");
        }

        public override void Insert(int index, T item) {
            throw new NotSupportedException("Please use only Add method");
        }
        #endregion
    }
}