using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплеменация хеш-сета (методом цепочек, на основе двусвязного списка)
    /// </summary>
    /// <typeparam name="T">Тип элементов в хеш-сете</typeparam>
    public class MyHashSet<T> : IEnumerable<T>, ICollection
    {
        /// <summary>
        /// Количество блоков в хэш-таблице по умолчанию
        /// </summary>
        private const int DefaultBucketsCount = 1024;

        private readonly int _bucketsCount;
        private object _syncRoot;

        private readonly MyLinkedList<T>[] _backets;

        /// <summary>
        /// Количество элементов в хэш-таблице
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Создает новую хэш-таблицу с количеством блоков по умолчанию
        /// </summary>
        public MyHashSet() : this(DefaultBucketsCount) {}

        /// <summary>
        /// Создает новую хэш-таблицу с указанным количеством блоков
        /// </summary>
        /// <param name="bucketsCount">Количество блоков</param>
        public MyHashSet(int bucketsCount) {
            if (bucketsCount <= 0) {
                throw new ArgumentOutOfRangeException();
            }
            this._bucketsCount = bucketsCount;
            this._backets = new MyLinkedList<T>[bucketsCount];
        }

        private int GetBacketNumber(T item) {
            return Math.Abs(item.GetHashCode()) % this._bucketsCount;
        }

        /// <summary>
        /// Добавляет элемент в хэш-таблицу
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        /// <returns>
        /// True если элемент был успешно добавлен,
        /// False - если элемент уже содержится в хэш-таблице
        /// </returns>
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

        /// <summary>
        /// Добавляет коллекцию в хэш-таблицу
        /// </summary>
        /// <param name="items">Добавляемые в хэш-таблицу элементы</param>
        public void AddRange(IEnumerable<T> items) {
            foreach (var item in items) {
                this.Add(item);
            }
        }

        /// <summary>
        /// Проверяет наличие указанного элемента в хэш-таблице
        /// </summary>
        /// <param name="item">Искомый элемент</param>
        /// <returns>True - если элемент присутствует в коллекции, иначе False</returns>
        public bool Contains(T item) {
            int bt = this.GetBacketNumber(item);
            if (this._backets[bt] == null) {
                return false;
            }
            MyLinkedList<T> list = this._backets[bt];
            return list.Contains(item);
        }

        /// <summary>
        /// Удаляет элемент из хэш-таблицы
        /// </summary>
        /// <param name="item">Удаляемый элемент</param>
        /// <returns>
        /// True - если элемент был успешно удален, False -
        /// если элемент отсутствует в коллекции
        /// </returns>
        public bool Remove(T item) {
            int bt = this.GetBacketNumber(item);
            if (this._backets[bt] == null) {
                return false;
            }
            if (!this._backets[bt].Remove(item)) {
                return false;
            }
            this.Count -= 1;
            return true;
        }

        #region Set operations
        /// <summary>
        /// Проверяет, являются ли элементы текущего хэш-сета подмножеством
        /// элементов другого хэш-сета
        /// </summary>
        /// <param name="otherSet">Второй хэш-сет</param>
        /// <returns>
        /// True - если элементы текущего хэш-сета являются подмножеством
        /// элементов другого хэш-сета, иначе False
        /// </returns>
        public bool IsSubsetOf(MyHashSet<T> otherSet) {
            foreach (var element in this) {
                if (!otherSet.Contains(element)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Проверяет, являются ли элементы текущего хэш-сета надмножеством
        /// элементов другого хеш-сета
        /// </summary>
        /// <param name="otherSet">Второй хеш-сет</param>
        /// <returns>
        /// True - если элементы текущего хэш-сета являются надмножеством
        /// элементов другого хеш-сета, иначе False
        /// </returns>
        public bool IsSupersetOf(MyHashSet<T> otherSet) {
            foreach (var element in otherSet) {
                if (!this.Contains(element)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Пересечение множеств. Возвращает новый хеш-сет, в котором содержатся только
        /// те элементы, которые присутствуют в обоих хеш-сетах.
        /// <para />
        /// Например: A = {1, 2, 3}, B = {3, 4, 5}, A ∩ B = { 3 }
        /// </summary>
        /// <param name="otherSet">Второй хеш-сет</param>
        /// <returns>
        /// Новый хеш-сет, в котором содержатся только те элементы, которые
        /// присутствуют в обоих хеш-сетах
        /// </returns>
        public MyHashSet<T> Intersection(MyHashSet<T> otherSet) {
            var newSet = new MyHashSet<T>();
            foreach (var e in this) {
                if (otherSet.Contains(e)) {
                    newSet.Add(e);
                }
            }
            return newSet;
        }

        /// <summary>
        /// Разность множеств. Возвращает новый хеш-сет, в котором содержатся только те
        /// элементы, которые содержатся в исходном хеш-сете и не содержащиеся во втором
        /// <para />
        /// Например:
        /// A = {1, 2, 3}, B = {3, 4, 5}, A ∖ B = { 1, 2 }
        /// <para />
        /// A = {3, 4, 5}, B = {1, 2, 3}, A ∖ B = { 4, 5 }
        /// </summary>
        /// <param name="otherSet">Второй хеш-сет</param>
        /// <returns>
        /// Новый хеш-сет, в котором содержатся только те элементы, которые содержатся
        /// в исходном хеш-сете и не содержащиеся во втором
        /// </returns>
        public MyHashSet<T> Complement(MyHashSet<T> otherSet) {
            var newSet = new MyHashSet<T>();
            foreach (var e in this) {
                if (!otherSet.Contains(e)) {
                    newSet.Add(e);
                }
            }
            return newSet;
        }

        /// <summary>
        /// Объединение множеств. Возвращает новый хеш-сет, в котором содержатся элементы из обоих
        /// хеш-сетов. Количество блоков в новом хеш-сете будет таким же как и в хеш-сете с
        /// большим количеством блоков
        /// <para />
        /// Например: A = {1, 2, 3}, B = {3, 4, 5}, A ∪ B = { 1, 2, 3, 4, 5 }
        /// </summary>
        /// <param name="otherSet">Второй хеш-сет</param>
        /// <returns>Новый хэш-сет с элементами из обоих хеш-сетов</returns>
        public MyHashSet<T> Union(MyHashSet<T> otherSet) {
            int bucketsCount = Math.Max(this._bucketsCount, otherSet._bucketsCount);
            var newSet = new MyHashSet<T>(bucketsCount);
            foreach (var e in this) {
                newSet.Add(e);
            }
            foreach (var e in otherSet) {
                newSet.Add(e);
            }
            return newSet;
        }

        /// <summary>
        /// Симметрическая разность множеств. Возвращает новый хеш-сет, в котором содержатся только
        /// элементы не пренадлежащие обоим хеш-сетам сразу
        /// <para />
        /// Например: A = {1, 2, 3, 4, 5}, B = {3, 4, 5, 6, 7}, A ∆ B = { 1, 2, 6, 7 }
        /// </summary>
        /// <param name="otherSet"></param>
        /// <returns></returns>
        public MyHashSet<T> Difference(MyHashSet<T> otherSet) {
            var newSet = new MyHashSet<T>();
            foreach (var e in this) {
                if (!otherSet.Contains(e)) {
                    newSet.Add(e);
                }
            }
            foreach (var e in otherSet) {
                if (!this.Contains(e)) {
                    newSet.Add(e);
                }
            }
            return newSet;
        }
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
            for (int i = 0; i < this._bucketsCount; i++) {
                MyLinkedList<T> bt = this._backets[i];
                if (bt == null) {
                    continue;
                }
                foreach (var item in bt) {
                    yield return item;
                }
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
}