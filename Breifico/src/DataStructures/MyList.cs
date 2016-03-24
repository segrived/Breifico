using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация списка
    /// </summary>
    /// <typeparam name="T">Тип элементов в списке</typeparam>
    [DebuggerDisplay("MyList<T>: Count: {Count} / Capacity: {Capacity}")]
    public class MyList<T> : IList<T>, ICollection
    {
        private const int StartSize = 8;

        private T[] _internalArray;

        private object _syncRoot;

        /// <summary>
        /// Создает новый список с вместимостью по умолчанию
        /// </summary>
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
        /// Возвращает True если коллекция пустая, иначе False
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

        /// <summary>
        /// Возвращает или устанавливает значение в списке по указанному индексу
        /// </summary>
        /// <param name="index">Индекс искомого элемента</param>
        /// <returns>Значение по указанному индексу</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Бросается, если указаннный индекс
        /// выходит за границы доступных значений
        /// </exception>
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
        /// <param name="forItems">
        /// Общее количество элементов,
        /// которое должна содержать коллекция
        /// </param>
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
        /// Удаляет первый указанный элемент из списка
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
        /// <exception cref="IndexOutOfRangeException">
        /// Бросается, если указаннный индекс
        /// выходит за границы доступных значений
        /// </exception>
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
        /// <exception cref="IndexOutOfRangeException">
        /// Бросается, если указаннный индекс
        /// выходит за границы доступных значений
        /// </exception>
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

        public void CopyTo(T[] array, int index, int count) {
            Array.Copy(this._internalArray, 0, array, index, count);
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
        /// <summary>
        /// Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="IEnumerator{T}" />, который может использоваться
        /// для перебора коллекции
        /// </returns>
        public IEnumerator<T> GetEnumerator() {
            for (int i = 0; i < this.Count; i++) {
                yield return this._internalArray[i];
            }
        }

        /// <summary>
        /// Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="IEnumerator" />, который может использоваться для 
        /// перебора коллекции
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        #endregion
    }
}