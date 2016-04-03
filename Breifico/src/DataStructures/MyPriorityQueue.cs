using System;
using System.Collections;
using System.Collections.Generic;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация приоритетной очереди
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class MyPriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
    {
        private class QueueItem<TR> : IComparable<QueueItem<TR>>
        {
            public TR Item { get; }
            public int Priority { get; }

            public QueueItem(TR item, int priority) {
                this.Item = item;
                this.Priority = priority;
            }

            public int CompareTo(QueueItem<TR> other) {
                return this.Priority.CompareTo(other.Priority);
            }
        }

        private readonly MyBinaryHeap<QueueItem<T>> _internalHeap = 
            MyBinaryHeap<QueueItem<T>>.CreateMaxHeap();

        private readonly int _minPriority;
        private readonly int _maxPriority;

        /// <summary>
        /// Количество элементов в приоритетной очереди
        /// </summary>
        public int Count => this._internalHeap.Count;

        /// <summary>
        /// Создает новый экземпляр <see cref="MyPriorityQueue{T}"/> с минимальным
        /// и максимальным приоритетом по умолчанию (от 0 до <see cref="int.MaxValue"/>)
        /// </summary>
        public MyPriorityQueue()
            : this(0, Int32.MaxValue) { }

        /// <summary>
        /// Создает новый экземпляр <see cref="MyPriorityQueue{T}"/> с указанным
        /// максимально возможным приоритетом. Минимально возможный приоритет - 0
        /// </summary>
        /// <param name="maxPriority">Максимально допустимый приоритет</param>
        public MyPriorityQueue(int maxPriority) 
            : this(0, maxPriority) { } 

        /// <summary>
        /// Создает новый экземпляр <see cref="MyPriorityQueue{T}"/> с указанным
        /// минимально и максимально возможным приоритетом
        /// </summary>
        /// <param name="minPriority">Минимально допустимый приоритет</param>
        /// <param name="maxPriority">Максимально допустимый приоритет</param>
        public MyPriorityQueue(int minPriority, int maxPriority) {
            this._minPriority = minPriority;
            this._maxPriority = maxPriority;
        } 

        /// <summary>
        /// Добавляет элемент в приоритетную очередь
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        /// <param name="priority">Приоритет добавляемого элемента</param>
        public void Enqueue(T item, int priority) {
            if (priority < this._minPriority || priority > this._maxPriority) {
                throw new ArgumentException("Priority out of range");
            }
            var qItem = new QueueItem<T>(item, priority);
            this._internalHeap.Add(qItem);
        }

        /// <summary>
        /// Извлекает самый приоритетный элемент из очереди
        /// </summary>
        /// <returns>Извлеченный элемент</returns>
        public T Dequeue() {
            return this._internalHeap.Extract().Item;
        }

        /// <summary>
        /// Очищает приоритеную очередь
        /// </summary>
        public void Clear() {
            this._internalHeap.Clear();
        }

        /// <summary>
        /// Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="IEnumerator{T}" />, который может использоваться
        /// для перебора коллекции
        /// </returns>
        public IEnumerator<T> GetEnumerator() {
            foreach (var item in this._internalHeap) {
                yield return item.Item;
            }
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
    }
}
