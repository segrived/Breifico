using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация стека
    /// </summary>
    /// <typeparam name="T">Тип элементов в стеке</typeparam>
    [DebuggerDisplay("MyStack<T>: {Count} element(s)")]
    public sealed class MyStack<T> : IEnumerable<T>, ICollection
    {
        private readonly MyLinkedList<T> _stackData
            = new MyLinkedList<T>();

        private object _syncRoot;

        private int LastIndex => this.Count - 1;

        /// <summary>
        /// Количество элементов в стеке
        /// </summary>
        public int Count => this._stackData.Count;

        /// <summary>
        /// Возвращает True если стек непустой, иначе False
        /// </summary>
        public bool IsEmpty => this.Count == 0;

        /// <summary>
        /// Возвращает первый элемент из стека, после чего удаляет его
        /// </summary>
        /// <returns>Первый элемент стека</returns>
        /// <exception cref="InvalidOperationException">Бросается в случае, если стек пуст</exception>
        public T Pop() {
            if (this.IsEmpty) {
                throw new InvalidOperationException("Stack is empty");
            }
            var item = this._stackData[this.LastIndex];
            this._stackData.RemoveAt(this.LastIndex);
            return item;
        }

        /// <summary>
        /// Добавляет новый элемент в начало стека
        /// </summary>
        /// <param name="value">Добавляемый элемент</param>
        public void Push(T value) {
            this._stackData.Add(value);
        }

        /// <summary>
        /// Возвращает первый элемент из стека, но не удаляет его
        /// </summary>
        /// <returns>Первый элемент стека</returns>
        /// <exception cref="InvalidOperationException">Бросается в случае, если стек пуст</exception>
        public T Peek() {
            if (this.IsEmpty) {
                throw new InvalidOperationException("Stack is empty");
            }
            return this._stackData[this.LastIndex];
        }

        /// <summary>
        /// Очищает стек
        /// </summary>
        public void Clear() {
            this._stackData.Clear();
        }

        #region IEnumerable<T> implementation
        /// <summary>
        /// Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="IEnumerator{T}" />, который может использоваться
        /// для перебора коллекции
        /// </returns>
        public IEnumerator<T> GetEnumerator() {
            return this._stackData.ReverseEnumerate().GetEnumerator();
        }

        /// <summary>
        /// Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="IEnumerator" />, который может использоваться для 
        /// перебора коллекции
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        #endregion

        #region ICollection implementation
        public void CopyTo(Array array, int index) {
            throw new NotImplementedException();
        }

        public object SyncRoot {
            get
            {
                if (this._syncRoot == null) {
                    Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
                }
                return this._syncRoot;
            }
        }

        public bool IsSynchronized { get; } = false;
        #endregion
    }
}