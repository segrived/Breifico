using System;
using System.Collections;
using System.Collections.Generic;
using Breifico.DataStructures.Interfaces;

namespace Breifico.DataStructures
{
    public class MyList<T> : IMyList<T>
    {
        private const int DefaultInternalSize = 8;

        private object _syncRoot;
        private T[] _internalArray;

        private int LastIndex => this.Count - 1;
        public int Count { get; private set; } = 0;

        public int Capacity
        {
            get { return this._internalArray.Length; }
            set {
                if (value < this.Count) {
                    throw new ArgumentException();
                }
                Array.Resize(ref this._internalArray, value);
            }
        }

        private void IncreaseCapacity(int forItems) {
            int newSize = this.Capacity;
            do {
                newSize *= 2;
            } while (forItems > newSize);
            this.Capacity = newSize;
        }

        public MyList() : this(DefaultInternalSize) { }

        public MyList(int capacity) {
            if (capacity <= 0) {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }
            this._internalArray = new T[capacity];
        }

        public void Add(T item) {
            if (this.Count == this.Capacity) {
                this.IncreaseCapacity(this.Count + 1);
            }
            this._internalArray[this.Count] = item;
            this.Count += 1;
        }

        public void AddRange(IEnumerable<T> items) {
            var arrayItems = items as T[];
            if (arrayItems != null) {
                int totalCount = this.Count + arrayItems.Length;
                if (totalCount > this.Capacity) {
                    this.IncreaseCapacity(totalCount);
                    Array.ConstrainedCopy(arrayItems, 0, this._internalArray, this.Count, arrayItems.Length);
                }
                this.Count += arrayItems.Length;
            } else {
                foreach (var item in items) {
                    this.Add(item);
                }
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > this.LastIndex) {
                    throw new IndexOutOfRangeException();
                }
                return this._internalArray[index];
            }
            set
            {
                if (index < 0 || index > this.LastIndex) {
                    throw new IndexOutOfRangeException();
                }
                this._internalArray[index] = value;
            }
        }

        public void RemoveAt(int index) {
            if (index < 0 || index > this.LastIndex) {
                throw new IndexOutOfRangeException();
            }
            if (index == this.Count - 1) {
                this._internalArray[index] = default(T);
            } else {
                var p = new T[this.LastIndex - index];
                Array.Copy(this._internalArray, index + 1, p, 0, p.Length);
                Array.Copy(p, 0, this._internalArray, index, p.Length);
            }
            this.Count -= 1;
        }

        public bool Contains(T item) {
            for (int i = 0; i < this.Count; i++) {
                if (this._internalArray[i].Equals(item)) {
                    return true;
                }
            }
            return false;
        }

        public void ShrinkCapacity() {
            this.Capacity = this.Count;
        }

        public void Clear() {
            Array.Clear(this._internalArray, 0, this.Count);
            this.Count = 0;
        }

        #region ICollection implementation
        public void CopyTo(Array array, int index) {
            throw new NotImplementedException();
        }

        public object SyncRoot
        {
            get
            {
                if (this._syncRoot == null) {
                    System.Threading.Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
                }
                return this._syncRoot;
            }
        }

        public bool IsSynchronized { get; } = false;
        #endregion

        #region IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator() {
            for (int i = 0; i < this.Count; i++) {
                yield return this._internalArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        #endregion
    }
}