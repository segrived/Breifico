using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация очереди
    /// </summary>
    /// <typeparam name="T">Тип элементов в очереди</typeparam>
    [DebuggerDisplay("MyQueue<T>: {Count} element(s)")]
    public class MyQueue<T> : IEnumerable<T>, ICollection
    {
        private object _syncRoot;

        private readonly MyLinkedList<T> _queueData
            = new MyLinkedList<T>();

        /// <summary>
        /// Колиество элементов в очереди
        /// </summary>
        public int Count => this._queueData.Count;

        /// <summary>
        /// Возвращает True если очередь непустая, иначе False
        /// </summary>
        public bool IsEmpty => this.Count == 0;

        /// <summary>
        /// Добавляет элемент в список
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public void Enqueue(T item) {
            this._queueData.Add(item);
        }

        /// <summary>
        /// Возвращает первый элемент в коллекции и удаляет его
        /// </summary>
        /// <returns>Первый элемент коллекции</returns>
        /// <exception cref="InvalidOperationException">Бросается в случае, если очередь пуста</exception>
        public T Dequeue() {
            if (this.IsEmpty) {
                throw new InvalidOperationException();
            }
            var item = this._queueData[0];
            this._queueData.RemoveAt(0);
            return item;
        }

        /// <summary>
        /// Возвращает первый элемент коллекции и оставляет его
        /// </summary>
        /// <returns>Первый элемент коллекции</returns>
        /// <exception cref="InvalidOperationException">Бросается в случае, если очередь пуста</exception>
        public T Peek() {
            if (this.IsEmpty) {
                throw new InvalidOperationException();
            }
            return this._queueData[0];
        }
        
        /// <summary>
        /// Очищает список
        /// </summary>
        public void Clear() {
            this._queueData.Clear();
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
            return this._queueData.GetEnumerator();
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

        public object SyncRoot
        {
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