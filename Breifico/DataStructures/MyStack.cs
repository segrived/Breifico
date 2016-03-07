using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.DataStructures
{
    public interface IMyStack<T> : IEnumerable<T>, ICollection
    {
        bool IsEmpty { get; }

        void Clear();
        T Pop();
        T Peek();
        void Push(T value);
    }

    /// <summary>
    /// Имплементация стэка
    /// </summary>
    /// <typeparam name="T">Тип элементов в стэке</typeparam>
    [DebuggerDisplay("MyStack<T>: {Count} element(s)")]
    public class MyStack<T> : IMyStack<T>
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

    /// <summary>
    /// Тесты к имплементации стэка
    /// </summary>
    [TestClass]
    public class MyStackTests
    {
        [TestMethod]
        public void NewInstance_ShouldBeEmpty() {
            new MyStack<int>().Should().BeEmpty();
        }

        [TestMethod]
        public void Count_ShouldReflectCollectionChanges() {
            var stack = new MyStack<int>();
            stack.Count.Should().Be(0);
            stack.Push(10);
            stack.Count.Should().Be(1);
            stack.Push(12);
            stack.Count.Should().Be(2);
            stack.Peek();
            stack.Count.Should().Be(2);
            stack.Pop();
            stack.Count.Should().Be(1);
            stack.Pop();
            stack.Count.Should().Be(0);
            stack.Push(10);
            stack.Clear();
            stack.Should().BeEmpty();
        }

        [TestMethod]
        public void IsEmpty_WhenCollectionEmpty_ShouldReturnTrue() {
            var queue = new MyStack<int>();
            queue.IsEmpty.Should().BeTrue();
            queue.Push(10);
            queue.Pop();
            queue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_WhenCollectionNotEmpty_ShouldReturnFalse() {
            var stack = new MyStack<int>();
            stack.Push(12);
            stack.IsEmpty.Should().BeFalse();
            stack.Push(10);
            stack.IsEmpty.Should().BeFalse();
            stack.Pop();
            stack.IsEmpty.Should().BeFalse();
        }

        [TestMethod]
        public void Push_ShouldAddNewElements() {
            var stack = new MyStack<int>();
            stack.Push(12);
            stack.Should().Equal(12);
            stack.Push(10);
            stack.Should().Equal(10, 12);
            stack.Push(8);
            stack.Should().Equal(8, 10, 12);
        }

        [TestMethod]
        public void Pop_WhenEmpty_ShouldThrowException() {
            var stack = new MyStack<int>();
            stack.Invoking(s => s.Pop())
                 .ShouldThrow<InvalidOperationException>();

            var stack2 = new MyStack<int>();
            stack2.Push(10);
            stack2.Pop();
            stack2.Invoking(s => s.Pop())
                  .ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Pop_WhenAnyElements_ShouldReturnItems() {
            var stack = new MyStack<int>();
            stack.Push(10);
            stack.Push(12);
            stack.Pop().Should().Be(12);
            stack.Pop().Should().Be(10);
        }

        [TestMethod]
        public void Peek_WhenEmpty_ShouldThrowException() {
            var stack = new MyStack<int>();
            stack.Invoking(s => s.Peek())
                 .ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Peek_Should_ReturnTopElement() {
            var stack = new MyStack<int>();
            stack.Push(10);
            stack.Push(12);
            stack.Peek().Should().Be(12);
            stack.Peek().Should().Be(12);
            stack.Pop();
            stack.Peek().Should().Be(10);
            stack.Peek().Should().Be(10);
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements() {
            var stack = new MyStack<int>();
            stack.Push(10);
            stack.Push(12);
            stack.Should().NotBeEmpty();
            stack.Clear();
            stack.Should().BeEmpty();
        }

        [TestMethod]
        public void SyncRoot_ShouldBeObject() {
            new MyStack<int>().SyncRoot.Should().NotBeNull().And.BeOfType<object>();
        }

        [TestMethod]
        public void IsSynchronized_ShouldBeFalse() {
            new MyStack<int>().IsSynchronized.Should().BeFalse();
        }
    }
}