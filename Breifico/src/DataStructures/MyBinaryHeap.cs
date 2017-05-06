using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация бинарной кучи
    /// </summary>
    /// <typeparam name="T">Тип элементов в бинарной куче</typeparam>
    [DebuggerDisplay("MyBinaryHeap<T>: Count: {Count}")]
    public sealed class MyBinaryHeap<T> : ICollection, IEnumerable<T> where T : IComparable<T>
    {
        private object _syncRoot;

        private readonly MyList<T> _data = new MyList<T>();
        private readonly IComparer<T> _comparer;

        /// <summary>
        /// Количество элементов в бинарной куче
        /// </summary>
        public int Count => this._data.Count;

        /// <summary>
        /// Возвращает True если коллекция пустая, иначе False
        /// </summary>
        public bool IsEmpty => this._data.Count == 0;

        public MyBinaryHeap(IComparer<T> comparer)
        {
            this._comparer = comparer;
        }

        public MyBinaryHeap(Comparison<T> comparision)
            : this(Comparer<T>.Create(comparision))
        {
        }

        /// <summary>
        /// Создает новый экземпляр бинарной кучи (Max-Heap) и возвращает ее
        /// </summary>
        /// <returns>Экземпляр бинарной кучи (Max-Heap)</returns>
        public static MyBinaryHeap<T> CreateMaxHeap()
            => new MyBinaryHeap<T>((a, b) => a.CompareTo(b));

        /// <summary>
        /// Создает новый экземпляр бинарной кучи (Min-Heap) и возвращает ее
        /// </summary>
        /// <returns>Экземпляр бинарной кучи (Min-Heap)</returns>
        public static MyBinaryHeap<T> CreateMinHeap()
            => new MyBinaryHeap<T>((a, b) => b.CompareTo(a));

        /// <summary>
        /// Добавляет элемент в бинарную кучу
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public void Add(T item)
        {
            this._data.Add(item);

            if (this.Count <= 1)
                return;

            int addedItemIndex = this.Count - 1;
            while (addedItemIndex > 0)
            {
                int parentIndex = (addedItemIndex - 1) / 2;

                if (this._comparer.Compare(this._data[parentIndex], this._data[addedItemIndex]) > 0)
                    return;

                var tmp = this._data[parentIndex];
                this._data[parentIndex] = this._data[addedItemIndex];
                this._data[addedItemIndex] = tmp;
                addedItemIndex = parentIndex;
            }
        }

        /// <summary>
        /// Добавляет коллекцию эллементов в бинарную кучу
        /// </summary>
        /// <param name="items">Коллекция с добавляемыми элементами</param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                this.Add(item);
        }

        /// <summary>
        /// Возващает элемент из вершины бинарной кучи, но не удаляего его
        /// </summary>
        /// <returns>Элемент из вершины</returns>
        public T Peek()
        {
            if (this.Count < 1)
                throw new IndexOutOfRangeException();
            return this._data[0];
        }

        /// <summary>
        /// Возващает элемент в вершины бинарной кучи, и удаляет его. После удаления
        /// куча перестраивается
        /// </summary>
        /// <returns>Элемент из вершины</returns>
        public T Extract()
        {
            if (this.Count <= 0)
                throw new ArgumentOutOfRangeException();

            var element = this._data[0];
            this._data[0] = this._data[this.Count - 1];
            this._data.RemoveAt(this.Count - 1);

            int index = 0;

            while (true)
            {
                int lIndex = index * 2 + 1;
                int rIndex = lIndex + 1;

                if (lIndex >= this._data.Count)
                    break;

                int cmpIndex = rIndex >= this._data.Count
                    ? lIndex
                    : (this._comparer.Compare(this._data[lIndex], this._data[rIndex]) > 0 ? lIndex : rIndex);

                if (this._comparer.Compare(this._data[index], this._data[cmpIndex]) < 0)
                {
                    var tmp = this._data[cmpIndex];
                    this._data[cmpIndex] = this._data[index];
                    this._data[index] = tmp;
                }
                index = cmpIndex;
            }
            return element;
        }

        /// <summary>
        /// Очищает бинарную кучу
        /// </summary>
        public void Clear() => this._data.Clear();

        #region ICollection implementation

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public object SyncRoot
        {
            get
            {
                if (this._syncRoot == null)
                    Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
                return this._syncRoot;
            }
        }

        public bool IsSynchronized { get; } = false;

        #endregion

        #region IEnumerable<T> implementation

        /// <summary>
        /// Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="IEnumerator{T}" />, который может использоваться
        /// для перебора коллекции
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
                yield return this._data[i];
        }

        /// <summary>
        /// Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="IEnumerator" />, который может использоваться для 
        /// перебора коллекции
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        #endregion
    }
}