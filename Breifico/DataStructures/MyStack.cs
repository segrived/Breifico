using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация стэка
    /// </summary>
    /// <typeparam name="T">Тип элементов в стэке</typeparam>
    [DebuggerDisplay("MyStack<T>: {Count} element(s)")]
    public class MyStack<T> : IEnumerable<T>, ICollection
    {
        private readonly MyLinkedList<T> _stackData
            = new MyLinkedList<T>();

        private object _syncRoot;

        private int LastIndex => this.Count - 1;

        /// <summary>
        /// Количество элементов в стэке
        /// </summary>
        public int Count => this._stackData.Count;

        /// <summary>
        /// Возвращает True если стэк непустой, иначе False
        /// </summary>
        public bool IsEmpty => this.Count == 0;

        /// <summary>
        /// Возвращает первый элемент из стэка, после чего удаляет его
        /// </summary>
        /// <returns>Первый элемент стэка</returns>
        /// <exception cref="InvalidOperationException">Бросается в случае, если стэк пуст</exception>
        public T Pop() {
            if (this.IsEmpty) {
                throw new InvalidOperationException("Stack is empty");
            }
            var item = this._stackData[this.LastIndex];
            this._stackData.RemoveAt(this.LastIndex);
            return item;
        }

        /// <summary>
        /// Добавляет новый элемент в начало стэка
        /// </summary>
        /// <param name="value">Добавляемый элемент</param>
        public void Push(T value) {
            this._stackData.Add(value);
        }

        /// <summary>
        /// Возвращает первый элемент из стэка, но не удаляет его
        /// </summary>
        /// <returns>Первый элемент стэка</returns>
        /// <exception cref="InvalidOperationException">Бросается в случае, если стэк пуст</exception>
        public T Peek() {
            if (this.IsEmpty) {
                throw new InvalidOperationException("Stack is empty");
            }
            return this._stackData[this.LastIndex];
        }

        /// <summary>
        /// Очищает стэк
        /// </summary>
        public void Clear() {
            this._stackData.Clear();
        }

        #region IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator() {
            return this._stackData.ReverseEnumerate().GetEnumerator();
        }

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