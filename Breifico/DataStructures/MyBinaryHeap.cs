using System;
using System.Collections.Generic;

namespace Breifico.DataStructures
{
    public class MyBinaryHeap<T> where T : IComparable<T>
    {
        private readonly MyList<T> _data = new MyList<T>();

        private readonly IComparer<T> _comparer;

        public MyBinaryHeap(IComparer<T> comparer) {
            this._comparer = comparer;
            this._data.Add(default(T)); // нулевой элемент (неиспользуемый)
        }

        public MyBinaryHeap(Comparison<T> comparision) 
            : this(Comparer<T>.Create(comparision)) { } 

        public MyBinaryHeap()
            : this(Comparer<T>.Default) {}

        public static MyBinaryHeap<T> DefaultHeap()
            => new MyBinaryHeap<T>((a, b) => a.CompareTo(b));

        public static MyBinaryHeap<T> ReverseHeap()
            => new MyBinaryHeap<T>((a, b) => b.CompareTo(a));  

        public void Add(T item) {
            this._data.Add(item);
            if (this._data.Count < 3) {
                return;
            }
            int addedItemIndex = this._data.Count - 1;
            while (addedItemIndex > 1) {
                int parentIndex = addedItemIndex / 2;

                if (this._comparer.Compare(this._data[parentIndex], this._data[addedItemIndex]) > 0) {
                    return;
                }
                var tmp = this._data[parentIndex];
                this._data[parentIndex] = this._data[addedItemIndex];
                this._data[addedItemIndex] = tmp;
                addedItemIndex = parentIndex;
            }
        }

        public void AddRange(IEnumerable<T> items) {
            foreach (var item in items) {
                this.Add(item);
            }
        }

        public T Peek() {
            if (this._data.Count < 2) {
                throw new IndexOutOfRangeException();
            }
            return this._data[1];
        }

        public T Extract() {
            if (this._data.Count < 2) {
                throw new IndexOutOfRangeException();
            }
            var element = this._data[1];
            this._data[1] = this._data[this._data.Count - 1];
            this._data.RemoveAt(this._data.Count - 1);

            int index = 1;

            while (true) {
                int leftChild = index * 2;
                int rightChild = index * 2 + 1;

                int compareIndex;
                if (leftChild >= this._data.Count) {
                    break;
                }
                if (rightChild >= this._data.Count) {
                    compareIndex = leftChild;
                } else {
                    var l = this._data[leftChild];
                    var r = this._data[rightChild];
                    compareIndex = this._comparer.Compare(l, r) > 0 ? leftChild : rightChild;
                }
                if ( this._comparer.Compare(this._data[index], this._data[compareIndex]) < 0) {
                    var tmp = this._data[compareIndex];
                    this._data[compareIndex] = this._data[index];
                    this._data[index] = tmp;
                }
                index = compareIndex;
            }
            return element;
        }
    }
}
