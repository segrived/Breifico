using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Реализация списка
    /// </summary>
    /// <typeparam name="T">Тип элементов в списке</typeparam>
    public class MyList<T> : IList<T>
    {
        private const int StartSize = 8;
        private T[] _internalArray;

        private object _syncRoot;

        public MyList() : this(StartSize) {}

        /// <summary>
        /// Создает новый список с указанной вместимостью
        /// </summary>
        /// <param name="capacity">Начальная вместимость списка</param>
        public MyList(int capacity) {
            if (capacity <= 0) {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }
            this._internalArray = new T[capacity];
        }

        /// <summary>
        /// Номер последнего индекса в массиве
        /// </summary>
        private int LastIndex => this.Count - 1;

        /// <summary>
        /// True если коллекция доступна только для чтения, иначе False
        /// </summary>
        public bool IsReadOnly { get; } = false;
        
        /// <summary>
        /// Количество элементов в коллекции
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// True если коллекция пустая, иначе False
        /// </summary>
        public bool IsEmpty => this.Count == 0;

        /// <summary>
        /// Вместимость списка
        /// </summary>
        public int Capacity
        {
            get { return this._internalArray.Length; }
            set
            {
                if (value < this.Count) {
                    throw new ArgumentException();
                }
                Array.Resize(ref this._internalArray, value);
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

        /// <summary>
        /// Увеличивает вместимость списка для указанного количества элементов
        /// </summary>
        /// <param name="forItems">Общее количество элементов, 
        /// которое должна содержать коллекция</param>
        private void IncreaseCapacity(int forItems) {
            int newSize = this.Capacity;
            do {
                newSize *= 2;
            } while (forItems > newSize);
            this.Capacity = newSize;
        }

        /// <summary>
        /// Уменьшает вместимость списка по предельного значения
        /// </summary>
        public void ShrinkCapacity() {
            this.Capacity = this.Count;
        }

        /// <summary>
        /// Добавляет элемент в список
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public void Add(T item) {
            this.Insert(this.Count, item);
        }

        /// <summary>
        /// Добавляет несколько элементов в коллекцию
        /// </summary>
        /// <param name="items">Добавляемые элементы</param>
        public void AddRange(IEnumerable<T> items) {
            var arrayItems = items as T[];
            if (arrayItems != null) {
                int totalCount = this.Count + arrayItems.Length;
                if (totalCount > this.Capacity) {
                    this.IncreaseCapacity(totalCount);
                }
                Array.Copy(arrayItems, 0, this._internalArray, this.Count, arrayItems.Length);
                this.Count += arrayItems.Length;
            } else {
                foreach (var item in items) {
                    this.Add(item);
                }
            }
        }

        /// <summary>
        /// Удаляет элемент из списка
        /// </summary>
        /// <param name="item">Элемент, который необходимо удалить</param>
        /// <returns>True если элемент был удален, False если указанный элемент не найден</returns>
        public bool Remove(T item) {
            for (int i = 0; i <= this.LastIndex; i++) {
                if (!this[i].Equals(item)) {
                    continue;
                }
                this.RemoveAt(i);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Возвращает индекс указанного элемента в списке
        /// Если указанный элемент отсутствует функция возвратит -1
        /// </summary>
        /// <param name="item">Искомый элемент</param>
        /// <returns>Индекс искомого элемента</returns>
        public int IndexOf(T item) {
            for (int i = 0; i <= this.LastIndex; i++) {
                if (this[i].Equals(item)) {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Вставляет элемент в список по указанному индексу
        /// </summary>
        /// <param name="index">Индекс, в который нужно вставить элемент</param>
        /// <param name="item">Элемент, который необходимо вставить в список</param>
        /// <exception cref="IndexOutOfRangeException">Бросается в случае, если указанный 
        /// индекс меньше нуля, либо больше размера коллекции</exception>
        public void Insert(int index, T item) {
            if (index < 0 || index > this.Count) {
                throw new IndexOutOfRangeException();
            }
            if (this.Count == this.Capacity) {
                this.IncreaseCapacity(this.Count + 1);
            }
            if (index == this.Count) {
                this._internalArray[this.Count] = item;
            } else {
                var secondPart = new T[this.Count - index];
                Array.Copy(this._internalArray, index, secondPart, 0, this.Count - index);
                this[index] = item;
                Array.Copy(secondPart, 0, this._internalArray, index + 1, secondPart.Length);
            }
            this.Count += 1;

        }

        /// <summary>
        /// Удаляет элемент по указанному индексу
        /// </summary>
        /// <param name="index">Индекс элемента, который нужно удалить</param>
        /// <exception cref="IndexOutOfRangeException">Бросается в случае, если указанный 
        /// индекс выходит за границы списка</exception>
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

        /// <summary>
        /// Проверяет наличие элемента в списке
        /// </summary>
        /// <param name="item">Искомый элемент</param>
        /// <returns>True - если элемент присутствует в списке, иначе False</returns>
        public bool Contains(T item) {
            for (int i = 0; i < this.Count; i++) {
                if (this._internalArray[i].Equals(item)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Очищает список
        /// </summary>
        public void Clear() {
            Array.Clear(this._internalArray, 0, this.Count);
            this.Count = 0;
        }

        #region ICollection implementation
        public void CopyTo(Array array, int index) {
            Array.Copy(this._internalArray, 0, array, index, this.Count);
        }

        public void CopyTo(T[] array, int index) {
            Array.Copy(this._internalArray, 0, array, index, this.Count);
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
            for (int i = 0; i < this.Count; i++) {
                yield return this._internalArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        #endregion
    }

    [TestClass]
    public class MyListTests
    {
        [TestMethod]
        public void NewInstance_ShouldBeEmpty() {
            new MyList<int>().Should().BeEmpty();
        }

        [TestMethod]
        public void CapacityTest() {
            var list = new MyList<int>(2);
            list.Capacity.Should().Be(2);
            var list2 = new MyList<int>(12);
            list2.Capacity.Should().Be(12);

            var list3 = new MyList<int>(2);
            list3.Add(0); // 1 element
            list3.Capacity.Should().Be(2);
            list3.Add(0); // 2 elements
            list3.Capacity.Should().Be(2);
            list3.Add(0); // 3 elements
            list3.Capacity.Should().Be(4);
            list3.Add(0); // 4 elements
            list3.Capacity.Should().Be(4);
            list3.Add(0); // 5 elements
            list3.Capacity.Should().Be(8);
            list3.AddRange(Enumerable.Range(0, 3).ToArray()); // 8 elements
            list3.Capacity.Should().Be(8);
            list3.Add(0); // 9 elements
            list3.Capacity.Should().Be(16);

            list3.Invoking(l => l.Capacity = 9).ShouldNotThrow();
            list3.Invoking(l => l.Capacity = 8).ShouldThrow<ArgumentException>();
            list3.Capacity.Should().Be(9);
        }

        [TestMethod]
        public void NewInstance_WithNegativeOrZeroCapacity_ShouldThrowException() {
            Action a = () => new MyList<int>(0).DoNothing();
            a.ShouldThrow<ArgumentOutOfRangeException>();
            Action b = () => new MyList<int>(-2).DoNothing();
            b.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void NewInstance_ShouldHasSpecifiedCapacity() {
            new MyList<int>(20).Capacity.Should().Be(20);
            new MyList<int>(1).Capacity.Should().Be(1);
        }


        [TestMethod]
        public void Add_ShouldAddNewElements() {
            var list = new MyList<int>(2) {1, 2, 3, 4, 5, 6};
            list.Count.Should().Be(6);
        }

        [TestMethod]
        public void AddRange_ShouldAddRange() {
            var list = new MyList<int>(2);
            list.AddRange(Enumerable.Range(0, 5).ToArray());
            list.Count.Should().Be(5);
            list.Should().Equal(0, 1, 2, 3, 4);
            list.AddRange(Enumerable.Range(20, 2));
            list.Should().Equal(0, 1, 2, 3, 4, 20, 21);
        }

        [TestMethod]
        public void Remove_WhenItemExists_ShouldReturnTrueAndRemove() {
            var list = new MyList<int> {0, 1, 2, 3, 4};
            list.Remove(4).Should().BeTrue();
            list.Should().Equal(0, 1, 2, 3);
            list.Remove(0).Should().BeTrue();
            list.Should().Equal(1, 2, 3);
            list.Remove(2).Should().BeTrue();
            list.Should().Equal(1, 3);
        }

        [TestMethod]
        public void Remove_WhenItemNotExists_ShouldReturnFalse() {
            var list = new MyList<int> {0, 1, 2, 3, 4};
            list.Remove(-1).Should().BeFalse();
            list.Remove(5).Should().BeFalse();
            list.Remove(99).Should().BeFalse();
            list.Should().Equal(0, 1, 2, 3, 4);
        }

        [TestMethod]
        public void RemoveAt_ShouldRemoveElements() {
            var list = new MyList<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            list.RemoveAt(4); // remove element in middle
            list.Should().Equal(0, 1, 2, 3, 5, 6, 7, 8, 9, 10);
            list.RemoveAt(0); // remove first element
            list.Should().Equal(1, 2, 3, 5, 6, 7, 8, 9, 10);
            list.RemoveAt(list.Count - 1); // remove last element
            list.Should().Equal(1, 2, 3, 5, 6, 7, 8, 9);

            var list2 = new MyList<int> {0};
            list2.RemoveAt(0);
            list2.Should().BeEmpty();
        }

        [TestMethod]
        public void RemoveAt_WhenIndexOutOfRange_ShouldThrowException() {
            var list = new MyList<int> {0, 1, 2, 3, 4, 5};
            list.Invoking(l => l.RemoveAt(-1)).ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l.RemoveAt(6)).ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void IndexerGet_Test() {
            var list = new MyList<int> {0, 1, 2, 3, 4, 5};
            list[0].Should().Be(0);
            list[3].Should().Be(3);
            list[5].Should().Be(5);
            list.Add(22);
            list[6].Should().Be(22);
        }

        [TestMethod]
        public void IndexerGet_WhenOutOfRange_ShouldThrowException() {
            var list = new MyList<int> {0, 1, 2, 3, 4, 5};
            list.Invoking(l => l[-1].DoNothing())
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[6].DoNothing())
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[20].DoNothing())
                .ShouldThrow<IndexOutOfRangeException>();

            var list2 = new MyList<int>();
            list2.Invoking(l => l[0]++)
                 .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void IndexerSet_Test() {
            var list = new MyList<int> {0, 1, 2, 3, 4, 5};
            list[0].Should().Be(0);
            list[0] = 12;
            list[0].Should().Be(12);
            list[5] = 10;
            list[5].Should().Be(10);
        }

        [TestMethod]
        public void IndexerSet_WhenOutOfRange_ShouldThrowException() {
            var list = new MyList<int> {0, 1, 2, 3, 4, 5};
            list.Invoking(l => l[-1] = 1)
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[6] = 1)
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[20] = 1)
                .ShouldThrow<IndexOutOfRangeException>();

            var list2 = new MyList<int>();
            list2.Invoking(l => l[0] = 1)
                 .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Contains_WhenEmpty_ShouldAlwaysReturnFalse() {
            var list = new MyList<int>();
            list.Contains(0).Should().BeFalse();
        }

        [TestMethod]
        public void Contains_WhenContainElement_ShouldReturnTrue() {
            var list = new MyList<int> {10, 20, 30};
            list.Contains(10).Should().BeTrue();
            list.Contains(15).Should().BeFalse();
            list.Contains(30).Should().BeTrue();
            list.Contains(20).Should().BeTrue();
            list.Contains(25).Should().BeFalse();
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements() {
            var list = new MyList<int> {0, 1, 2, 3, 4, 5};
            list.Clear();
            list.Count.Should().Be(0);
            list.Should().BeEmpty();
        }

        [TestMethod]
        public void SyncRoot_ShouldBeObject() {
            new MyList<int>().SyncRoot.Should().NotBeNull()
                             .And.BeOfType<object>();
        }

        [TestMethod]
        public void IsSynchronized_ShouldBeFalse() {
            new MyList<int>().IsSynchronized.Should().BeFalse();
        }
    }
}