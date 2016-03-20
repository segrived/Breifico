using System;

namespace Breifico.DataStructures
{
    public class QueueItem<T> : IComparable<QueueItem<T>>
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

    public class MyPriorityQueue<T> where T : IComparable<T>
    {
        private readonly MyBinaryHeap<QueueItem<T>> _internalHeap = 
            new MyBinaryHeap<QueueItem<T>>();

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
