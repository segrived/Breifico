using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация связанного списка
    /// </summary>
    /// <typeparam name="T">Тип элементов в связанном списке</typeparam>
    [DebuggerDisplay("MyLinkedList<T>: {Count} element(s)")]
    public class MyLinkedList<T> : IList<T>
    {
        /// <summary>
        /// Нода связанного списка
        /// </summary>
        /// <typeparam name="TR">Тип значения в ноде</typeparam>
        [DebuggerDisplay("{Previous} <- {Value} -> {Next}")]
        protected class Node<TR>
        {
            /// <summary>
            /// Создает новую ноду с указанным значением
            /// </summary>
            /// <param name="value"></param>
            public Node(TR value) {
                this.Value = value;
            }

            /// <summary>
            /// Ссылка на предыдущую ноду
            /// </summary>
            internal Node<TR> Previous { get; set; }

            /// <summary>
            /// Ссылка на следующую ноду
            /// </summary>
            internal Node<TR> Next { get; set; }

            /// <summary>
            /// Значение элемента а ноде
            /// </summary>
            public TR Value { get; set; }

            /// <summary>
            /// Строковое представление ноды
            /// </summary>
            /// <returns></returns>
            public override string ToString() {
                return this.Value.ToString();
            }
        }

        private object _syncRoot;

        protected Node<T> HeadNode;
        protected Node<T> LastNode;

        /// <summary>
        /// Количество элементов в связанном списке
        /// </summary>
        public int Count { get; private set; }

        public bool IsReadOnly { get; } = false;

        /// <summary>
        /// Возвращает или устанавливает значение элемента по указанному индексу
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Значение элемента по искомому индексу</returns>
        public T this[int index]
        {
            get { return this.GetNodeByIndex(index).Value; }
            set { this.GetNodeByIndex(index).Value = value; }
        }

        private Node<T> GetNodeByIndex(int index) {
            if (index < 0 || index >= this.Count) {
                throw new IndexOutOfRangeException();
            }
            int midPoint = this.Count / 2;

            Node<T> tempNode;
            if (index < midPoint) {
                tempNode = this.HeadNode;
                for (int i = 0; i < index; i++) {
                    tempNode = tempNode.Next;
                }
            } else {
                tempNode = this.LastNode;
                for (int i = this.Count - 1; i > index; i--) {
                    tempNode = tempNode.Previous;
                }
            }
            return tempNode;
        }

        /// <summary>
        /// Вставляет элемент в начало связанного списка
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public virtual void AddFirst(T item) {
            this.Insert(0, item);
        }

        /// <summary>
        /// Вставляет элемент в конец связанного списка
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public virtual void AddLast(T item) {
            this.Insert(this.Count, item);
        }

        /// <summary>
        /// Вставляет элемент после указанного элемента
        /// Если указанный элемент отсутствует в списке, он будет добавлен в конец
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public virtual void AddAfter(T item) {
            int nodeIndex = this.IndexOf(item);
            if (nodeIndex == -1) {
                this.AddLast(item);
            } else {
                this.Insert(nodeIndex + 1, item);
            }
        }

        /// <summary>
        /// Добавляет элемент в конец связанного списка
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public virtual void Add(T item) => this.AddLast(item);

        /// <summary>
        /// Добавляет несколько элементов в связанный список
        /// </summary>
        /// <param name="items">Добавляемые элементы</param>
        public void AddRange(IEnumerable<T> items) {
            foreach (var item in items) {
                this.Add(item);
            }
        }

        /// <summary>
        /// Возвращает индекс указанного элемента в связанном списке
        /// Если указанный элемент отсутствует функция возвратит -1
        /// </summary>
        /// <param name="item">Искомый элемент</param>
        /// <returns>Индекс искомого элемента</returns>
        public int IndexOf(T item) {
            var tempNode = this.HeadNode;
            for (int i = 0; i < this.Count; i++) {
                if (tempNode.Value.Equals(item)) {
                    return i;
                }
                tempNode = tempNode.Next;
            }
            return -1;
        }

        /// <summary>
        /// Вставляет элемент в связанный список по указанному индексу
        /// </summary>
        /// <param name="index">Индекс, в который нужно вставить элемент</param>
        /// <param name="item">Элемент, который необходимо вставить в связанный список</param>
        /// <exception cref="IndexOutOfRangeException">Бросается в случае, если указанный 
        /// индекс меньше нуля, либо больше размера коллекции</exception>
        public virtual void Insert(int index, T item) {
            if (index < 0 || index > this.Count) {
                throw new IndexOutOfRangeException();
            }
            var newNode = new Node<T>(item);
            // Если список пуст
            if (this.Count == 0) {
                this.HeadNode = this.LastNode = newNode;
            // Добавление в начало списка
            } else if (index == 0) {
                var oldHeadNode = this.HeadNode;
                this.HeadNode = newNode;
                this.HeadNode.Next = oldHeadNode;
                oldHeadNode.Previous = this.HeadNode;
            // Добавление в конец списка
            } else if (index == this.Count) {
                this.LastNode.Next = newNode;
                newNode.Previous = this.LastNode;
                this.LastNode = newNode;
            // Добавление в середину списка
            } else {
                var node = this.GetNodeByIndex(index);
                newNode.Next = node;
                newNode.Previous = node.Previous;
                newNode.Previous.Next = newNode;
                newNode.Next.Previous = newNode;
            }
            this.Count += 1;
        }

        /// <summary>
        /// Проверяет наличие элемента в связанном списке
        /// </summary>
        /// <param name="item">Искомый элемент</param>
        /// <returns>True - если элемент присутствует в связанном списке, иначе False</returns>
        public bool Contains(T item) {
            var tempNode = this.HeadNode;
            while (tempNode != null) {
                if (tempNode.Value.Equals(item)) {
                    return true;
                }
                tempNode = tempNode.Next;
            }
            return false;
        }

        /// <summary>
        /// Удаляет элемент по указанному индексу
        /// </summary>
        /// <param name="index">Индекс элемента, который нужно удалить</param>
        /// <exception cref="IndexOutOfRangeException">Бросается в случае, если указанный 
        /// индекс выходит за границы связанного списка</exception>
        public void RemoveAt(int index) {
            if (index < 0 || index >= this.Count) {
                throw new IndexOutOfRangeException();
            }
            if (index == 0) {
                this.HeadNode = this.HeadNode.Next;
                if (this.HeadNode != null) {
                    this.HeadNode.Previous = null;
                }
            } else if (index == this.Count - 1) {
                this.LastNode = this.LastNode.Previous;
                if (this.LastNode != null) {
                    this.LastNode.Next = null;
                }
            } else {
                var node = this.GetNodeByIndex(index - 1);
                node.Next = node.Next.Next;
            }
            this.Count -= 1;
        }

        /// <summary>
        /// Удаляет элемент из связанного списка
        /// </summary>
        /// <param name="item">Элемент, который необходимо удалить</param>
        /// <returns>True если элемент был удален, False если указанный элемент не найден</returns>
        public bool Remove(T item) {
            var tempNode = this.HeadNode;
            for (int i = 0; i < this.Count; i++) {
                if (tempNode.Value.Equals(item)) {
                    this.RemoveAt(i);
                    return true;
                }
                tempNode = tempNode.Next;
            }
            return false;
        }

        /// <summary>
        /// Очищает связанный список
        /// </summary>
        public void Clear() {
            this.HeadNode = this.LastNode = null;
            this.Count = 0;
        }

        /// <summary>
        /// Перечисляет элементы связанного списка в обратном порядке
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> ReverseEnumerate() {
            var tempNode = this.LastNode;
            while (tempNode != null) {
                yield return tempNode.Value;
                tempNode = tempNode.Previous;
            }
        }

        /// <summary>
        /// Возврщает строковое представление экземпляра связанного списка
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            string headNodeText = this.HeadNode?.ToString() ?? "null";
            string lastNodeText = this.LastNode?.ToString() ?? "null";
            return $"S: {headNodeText}, E: {lastNodeText}";
        }

        #region ICollection implementation
        public void CopyTo(Array array, int index) {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex) {
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

        #region IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator() {
            var tempNode = this.HeadNode;
            while (tempNode != null) {
                yield return tempNode.Value;
                tempNode = tempNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        #endregion
    }

    /// <summary>
    /// Тесты к имплементации связанного списка
    /// </summary>
    [TestClass]
    public class MyLinkedListTests
    {
        [TestMethod]
        public void NewInstance_ShouldBeEmpty() {
            new MyLinkedList<int>().Should().BeEmpty();
        }

        [TestMethod]
        public void Count_ShouldReflectCollectionChanges() {
            var list = new MyLinkedList<int>();
            list.Count.Should().Be(0);
            list.Add(10);
            list.Count.Should().Be(1);
            list.AddRange(new[] {20, 30});
            list.Count.Should().Be(3);
            list.RemoveAt(0);
            list.Count.Should().Be(2);
            list.Clear();
            list.Count.Should().Be(0);
        }

        [TestMethod]
        public void IndexerGet_WhenAnyElements_ShouldReturnElementByIndex() {
            var list = new MyLinkedList<int> {10, 20, 30};
            list[0].Should().Be(10);
            list[1].Should().Be(20);
            list[2].Should().Be(30);
        }

        [TestMethod]
        public void IndexerGet_WhenIndexOutOfRange_ShouldThrowException() {
            var list = new MyLinkedList<int> {10, 20, 30};
            list.Invoking(l => l[-1].DoNothing())
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[10].DoNothing())
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void IndexerSet_WhenAnyElements_ShouldAssignValue() {
            var list = new MyLinkedList<int> {10, 20, 30};
            list[0] = 100;
            list[0].Should().Be(100);
            list[1] = 200;
            list[1].Should().Be(200);
            list[2] = 300;
            list[2].Should().Be(300);
        }

        [TestMethod]
        public void IndexerSet_WhenIndexOutOfRange_ShouldThrowException() {
            var list = new MyLinkedList<int> {10, 20, 30};
            list.Invoking(l => l[-1] = 0)
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[10] = 0)
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Add_ShouldAddElements() {
            var list = new MyLinkedList<int> {10};
            list.Should().Equal(10);
            list.Add(20);
            list.Should().Equal(10, 20);
            list.Add(30);
            list.Should().Equal(10, 20, 30);
        }

        [TestMethod]
        public void AddRange_ShouldAddMultipleElements() {
            var list = new MyLinkedList<int>();
            list.AddRange(new[] {40, 50, 60});
            list.Should().Equal(40, 50, 60);
            list.AddRange(new[] {70, 80});
            list.Should().Equal(40, 50, 60, 70, 80);
        }

        [TestMethod]
        public void IndexOf_WhenItemNotExists_ShouldReturnMinusOne() {
            var list = new MyLinkedList<int>();
            list.AddRange(new[] { 40, 50, 60 });
            list.IndexOf(-1).Should().Be(-1);
            list.IndexOf(3).Should().Be(-1);
            list.IndexOf(12).Should().Be(-1);
        }

        [TestMethod]
        public void IndexOf_WhenItemExists_ShouldReturnIndex() {
            var list = new MyLinkedList<int>();
            list.AddRange(new[] { 40, 50, 60 });
            list.IndexOf(40).Should().Be(0);
            list.IndexOf(50).Should().Be(1);
            list.IndexOf(60).Should().Be(2);
        }

        [TestMethod]
        public void Insert_WhenEmptyList_ShouldThrowException() {
            var list = new MyLinkedList<int>();
            list.Invoking(l => l.Insert(-1, 0))
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l.Insert(1, 0))
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Insert_WhenValidIndex_ShouldInsertElements() {
            var list = new MyLinkedList<int>();
            list.Insert(0, 10);
            list.Should().Equal(10);
            list.Insert(0, 20);
            list.Should().Equal(20, 10);
            list.Insert(2, 30);
            list.Should().Equal(20, 10, 30);
            list.Insert(1, 40);
            list.Should().Equal(20, 40, 10, 30);
            list.Insert(1, 10);
            list.Should().Equal(20, 10, 40, 10, 30);
        }

        [TestMethod]
        public void Insert_WhenIndexOutOfRange_ShouldThrowException() {
            var list = new MyLinkedList<int>();
            list.AddRange(new[] {10, 20, 30, 40});
            list.Invoking(l => l.Insert(5, 0))
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l.Insert(-1, 0))
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Contains_WhenEmpty_ShouldAlwaysReturnFalse() {
            var list = new MyLinkedList<int>();
            list.Contains(0).Should().BeFalse();
        }

        [TestMethod]
        public void Contains_ShouldReturnTrueIfContains() {
            var list = new MyLinkedList<int> { 10, 20, 30 };
            list.Contains(10).Should().BeTrue();
            list.Contains(15).Should().BeFalse();
            list.Contains(30).Should().BeTrue();
            list.Contains(20).Should().BeTrue();
            list.Contains(25).Should().BeFalse();
        }

        [TestMethod]
        public void Remove_WhenEmpty_ShouldThrowExcepetion() {
            var list = new MyLinkedList<int>();
            list.Invoking(l => l.RemoveAt(0))
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Remove_WhenAnyElements_ShouldRemoveElement() {
            var list = new MyLinkedList<int> {1, 2, 3};
            list.RemoveAt(0);
            list.Should().Equal(2, 3);
            list.RemoveAt(1);
            list.Should().Equal(2);
            list.RemoveAt(0);
            list.Should().BeEmpty();
        }

        [TestMethod]
        public void Remove_WhenIndexOutOfRange_ShouldThrowException() {
            var list = new MyLinkedList<int> {1, 2, 3};
            list.Invoking(l => l.RemoveAt(-1))
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l.RemoveAt(10))
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Remove_WhenLastIndexMultipleTimes_ShouldNotThrowException() {
            var list = new MyLinkedList<int> {1, 2, 3};
            list.RemoveAt(2);
            list.Add(10);
            list.RemoveAt(2);
        }

        [TestMethod]
        public void RemoveFirst_WhenIncluded_ShouldRemoveItemAndReturnTrue() {
            var list = new MyLinkedList<int> {1, 2, 3};
            list.Remove(2).Should().BeTrue();
            list.Should().Equal(1, 3);

            var list2 = new MyLinkedList<int> {1, 2, 2, 2, 3};
            list2.Remove(2).Should().BeTrue();
            list2.Should().Equal(1, 2, 2, 3);
            list2.Remove(3).Should().BeTrue();
            list2.Should().Equal(1, 2, 2);
            list2.Remove(1).Should().BeTrue();
            list2.Should().Equal(2, 2);
        }

        [TestMethod]
        public void RemoveFirst_WhenNotIncluded_ShouldReturnFalse() {
            var list = new MyLinkedList<int> {1, 2, 3};
            list.Remove(4).Should().BeFalse();
            list.Should().Equal(1, 2, 3);
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements() {
            var list = new MyLinkedList<int> {10, 20};
            list.Should().NotBeEmpty();
            list.Clear();
            list.Should().BeEmpty();
        }

        [TestMethod]
        public void ReverseEnumerate_ShouldIterateBackward() {
            var list = new MyLinkedList<int> {10, 20, 30, 40};
            list.ReverseEnumerate().Should().Equal(40, 30, 20, 10);
            list.RemoveAt(0);
            list.ReverseEnumerate().Should().Equal(40, 30, 20);
            list.Clear();
            list.ReverseEnumerate().Should().BeEmpty();
        }

        [TestMethod]
        public void ToString_ShouldReturnString() {
            var list1 = new MyLinkedList<int> { 10, 20 };
            list1.ToString().Should().Be("S: 10, E: 20");

            var list2 = new MyLinkedList<int>();
            list2.ToString().Should().Be("S: null, E: null");
        }

        [TestMethod]
        public void SyncRoot_ShouldBeObject() {
            new MyLinkedList<int>().SyncRoot.Should().NotBeNull().And.BeOfType<object>();
        }

        [TestMethod]
        public void IsSynchronized_ShouldBeFalse() {
            new MyLinkedList<int>().IsSynchronized.Should().BeFalse();
        }
    }
}