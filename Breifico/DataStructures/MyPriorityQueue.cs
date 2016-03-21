using System;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация приоритетной очереди
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyPriorityQueue<T> where T : IComparable<T>
    {
        private class QueueItem<T> : IComparable<QueueItem<T>>
        {
            public T Item { get; }
            public int Priority { get; }

            public QueueItem(T item, int priority) {
                this.Item = item;
                this.Priority = priority;
            }

            public int CompareTo(QueueItem<T> other) {
                return this.Priority.CompareTo(other.Priority);
            }
        }

        private readonly MyBinaryHeap<QueueItem<T>> _internalHeap = 
            MyBinaryHeap<QueueItem<T>>.CreateMaxHeap();

        public int Count => this._internalHeap.Count;

        public MyPriorityQueue() {}

        public void Enqueue(T item, int priority) {
            var qItem = new QueueItem<T>(item, priority);
            this._internalHeap.Add(qItem);
        }

        public T Dequeue() {
            return this._internalHeap.Extract().Item;
        }
    }
}
