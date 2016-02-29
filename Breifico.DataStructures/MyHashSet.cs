using System;
using System.Collections;
using System.Collections.Generic;
using Breifico.DataStructures.Interfaces;

namespace Breifico.DataStructures
{
    public class MyHashSet<T> : IMyHashSet<T>
    {
        private const int DefaultTableSize = 64;

        private object _syncRoot;

        private MyLinkedList<T>[] _backets =
            new MyLinkedList<T>[DefaultTableSize];

        public int Count { get; private set; }

        private int GetBacketNumber(T item) {
            return item.GetHashCode() % DefaultTableSize;
        }

        public bool Add(T item) {
            // O(N/B)
            if (this.Contains(item)) {
                return false;
            }
            int bt = this.GetBacketNumber(item);
            if (this._backets[bt] == null) {
                this._backets[bt] = new MyLinkedList<T>();
            }
            this._backets[bt].Add(item);
            this.Count += 1;
            return true;
        }

        public void AddRange(IEnumerable<T> items) {
            foreach (var item in items) {
                this.Add(item);
            }
        }

        public bool Contains(T item) {
            int bt = this.GetBacketNumber(item);
            if (this._backets[bt] == null) {
                return false;
            }
            var list = this._backets[bt];
            return list.Contains(item);
        }

        #region IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator() {
            for (int i = 0; i < DefaultTableSize; i++) {
                var bt = this._backets[i];
                if (bt == null) continue;
                foreach (var item in bt) {
                    yield return item;
                }
            }
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
                    System.Threading.Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
                }
                return this._syncRoot;
            }
        }

        public bool IsSynchronized { get; } = false;
        #endregion
    }
}
