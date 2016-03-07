using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.DataStructures
{
    public interface IMyQueue<T> : IEnumerable<T>, ICollection
    {
        bool IsEmpty { get; }

        void Enqueue(T item);
        T Dequeue();
        T Peek();
        void Clear();
    }

    /// <summary>
    /// Имплементация очереди
    /// </summary>
    /// <typeparam name="T">Тип элементов в очереди</typeparam>
    [DebuggerDisplay("MyQueue<T>: {Count} element(s)")]
    public class MyQueue<T> : IMyQueue<T>
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
        public IEnumerator<T> GetEnumerator() {
            return this._queueData.GetEnumerator();
        }

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

    /// <summary>
    /// Тесты к имплементации списка
    /// </summary>
    [TestClass]
    public class MyQueueTests
    {
        [TestMethod]
        public void NewInstance_ShouldBeEmpty() {
            new MyQueue<int>().Should().BeEmpty();
        }

        [TestMethod]
        public void Count_ShouldReflectCollectionChanges() {
            var queue = new MyQueue<int>();
            queue.Count.Should().Be(0);
            queue.Enqueue(1);
            queue.Count.Should().Be(1);
            queue.Enqueue(10);
            queue.Count.Should().Be(2);
            queue.Dequeue();
            queue.Count.Should().Be(1);
            queue.Peek();
            queue.Count.Should().Be(1);
            queue.Dequeue();
            queue.Count.Should().Be(0);
            queue.Enqueue(20);
            queue.Clear();
            queue.Count.Should().Be(0);
        }

        [TestMethod]
        public void IsEmpty_WhenCollectionEmpty_ShouldReturnTrue() {
            var queue = new MyQueue<int>();
            queue.IsEmpty.Should().BeTrue();
            queue.Enqueue(10);
            queue.Dequeue();
            queue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_WhenCollectionNotEmpty_ShouldReturnFalse() {
            var queue = new MyQueue<int>();
            queue.Enqueue(12);
            queue.IsEmpty.Should().BeFalse();
            queue.Enqueue(10);
            queue.IsEmpty.Should().BeFalse();
            queue.Dequeue();
            queue.IsEmpty.Should().BeFalse();
        }

        [TestMethod]
        public void Enqueue_ShouldAddNewElements() {
            var queue = new MyQueue<int>();
            queue.Should().BeEmpty();
            queue.Enqueue(12);
            queue.Should().Equal(12);
            queue.Enqueue(22);
            queue.Should().Equal(12, 22);
        }

        [TestMethod]
        public void Dequeue_WhenEmpty_ShouldThrowException() {
            var queue = new MyQueue<int>();
            queue.Invoking(q => q.Dequeue())
                 .ShouldThrow<InvalidOperationException>();

            queue.Enqueue(10);
            queue.Dequeue();
            queue.Invoking(q => q.Dequeue())
                 .ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Dequeue_WhenAnyElements_ShouldReturnItems() {
            var queue = new MyQueue<int>();
            queue.Enqueue(12);
            queue.Enqueue(15);
            queue.Should().Equal(12, 15);
            queue.Dequeue().Should().Be(12);
            queue.Should().Equal(15);
            queue.Dequeue().Should().Be(15);
            queue.Should().BeEmpty();
        }

        [TestMethod]
        public void Peek_WhenEmpty_ShouldThrowException() {
            var queue = new MyQueue<int>();
            queue.Invoking(q => q.Peek())
                 .ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Peek_Should_ReturnTopElement() {
            var queue = new MyQueue<int>();
            queue.Enqueue(10);
            queue.Enqueue(12);
            queue.Peek().Should().Be(10);
            queue.Peek().Should().Be(10);
            queue.Dequeue();
            queue.Peek().Should().Be(12);
            queue.Peek().Should().Be(12);
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements() {
            var queue = new MyQueue<int>();
            queue.Enqueue(10);
            queue.Enqueue(12);
            queue.Should().NotBeEmpty();
            queue.Clear();
            queue.Should().BeEmpty();
        }

        [TestMethod]
        public void SyncRoot_ShouldBeObject() {
            new MyQueue<int>().SyncRoot.Should().NotBeNull().And.BeOfType<object>();
        }

        [TestMethod]
        public void IsSynchronized_ShouldBeFalse() {
            new MyQueue<int>().IsSynchronized.Should().BeFalse();
        }
    }
}