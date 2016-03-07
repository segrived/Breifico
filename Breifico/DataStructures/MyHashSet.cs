using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.DataStructures
{
    public interface IMyHashSet<T> : IEnumerable<T>, ICollection
    {
        bool Add(T item);
        void AddRange(IEnumerable<T> items);
        bool Contains(T item);
        void Remove(T item);
    }

    public class MyHashSet<T> : IMyHashSet<T>
    {
        private const int DefaultTableSize = 1024;

        private readonly int _tableSize;
        private object _syncRoot;

        private readonly MyLinkedList<T>[] _backets;

        public int Count { get; private set; }

        public MyHashSet() : this(DefaultTableSize) {}

        public MyHashSet(int tableSize) {
            if (tableSize <= 0) {
                throw new ArgumentOutOfRangeException();
            }
            this._tableSize = tableSize;
            this._backets = new MyLinkedList<T>[tableSize];
        } 

        private int GetBacketNumber(T item) {
            return item.GetHashCode() % this._tableSize;
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

        public void Remove(T item) {
            int bt = this.GetBacketNumber(item);
            if (this._backets[bt] == null) {
                return;
            }
            // RemoveFirst returns true if selement was removed
            if (this._backets[bt].Remove(item)) {
                this.Count -= 1;
            }
        }

        #region IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator() {
            for (int i = 0; i < this._tableSize; i++) {
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

    [TestClass]
    public class MyHashSetTests
    {
        [TestMethod]
        public void NewInstance_WhenPositionBacketsNumber_ShouldNotThrowException() {
            var c = new Action(() => new MyHashSet<int>(20));
            c.ShouldNotThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void NewInstance_WhenInvalidBacketsNumber_ShouldThrowException() {
            var a = new Action(() => new MyHashSet<int>(0));
            var b = new Action(() => new MyHashSet<int>(-1));
            a.ShouldThrow<ArgumentOutOfRangeException>();
            b.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void Add_WhenDuplicate_ShouldNotBeAdded() {
            var hashSet = new MyHashSet<int>();
            hashSet.Add(2).Should().BeTrue();
            hashSet.Count.Should().Be(1);
            hashSet.Add(2).Should().BeFalse();
            hashSet.Count.Should().Be(1);
            hashSet.Add(3).Should().BeTrue();
            hashSet.Count.Should().Be(2);
            hashSet.Add(2).Should().BeFalse();
            hashSet.Add(3).Should().BeFalse();
            hashSet.Count.Should().Be(2);
        }

        [TestMethod]
        public void AddRange_ShouldAddElements() {
            var hashSet = new MyHashSet<int>();
            hashSet.AddRange(new[] { 1, 2, 2, 3, 4, 4, 1, 5, 6, 7, 4, 5, 8, 4, 8, 4, 8, 9 });
            hashSet.Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        [TestMethod]
        public void Contains_WhenElementExists_ShouldReturnTrue() {
            var hashSet = new MyHashSet<int> { 10, 20, 30 };
            hashSet.Contains(10).Should().BeTrue();
            hashSet.Contains(20).Should().BeTrue();
            hashSet.Contains(30).Should().BeTrue();
        }

        [TestMethod]
        public void Contains_WhenElementNotExists_ShouldReturnFalse() {
            var hashSet = new MyHashSet<int> { 10, 20, 30 };
            hashSet.Contains(11).Should().BeFalse();
            hashSet.Contains(22).Should().BeFalse();
            hashSet.Contains(33).Should().BeFalse();
            hashSet.Add(33);
            hashSet.Contains(33).Should().BeTrue();
        }

        [TestMethod]
        public void Remove_WhenExists_ShouldRemoveElement() {
            var hashSet = new MyHashSet<int> { 10, 20, 30, 40 };
            hashSet.Remove(20);
            hashSet.Should().Equal(10, 30, 40);
            hashSet.Remove(20);
            hashSet.Should().Equal(10, 30, 40);
            hashSet.Remove(10);
            hashSet.Should().Equal(30, 40);
        }

        [TestMethod]
        public void Remove_WhenEmpty_ShouldDoNothing() {
            var hashSet = new MyHashSet<int>();
            hashSet.Remove(0);
            hashSet.Should().BeEmpty();
        }

        [TestMethod]
        public void SyncRoot_ShouldBeObject() {
            new MyHashSet<int>().SyncRoot.Should().NotBeNull()
                .And.BeOfType<object>();
        }

        [TestMethod]
        public void IsSynchronized_ShouldBeFalse() {
            new MyHashSet<int>().IsSynchronized.Should().BeFalse();
        }
    }
}
