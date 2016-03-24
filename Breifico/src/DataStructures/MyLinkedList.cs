using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация связного списка
    /// </summary>
    /// <typeparam name="T">Тип элементов в связном списке</typeparam>
    [DebuggerDisplay("MyLinkedList<T>: {Count} element(s)")]
    public class MyLinkedList<T> : IList<T>, ICollection
    {
        /// <summary>
        /// Нода связного списка
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

        /// <summary>
        /// Начальная (первая) нода в связном списке
        /// </summary>
        protected Node<T> HeadNode;

        /// <summary>
        /// Конечная (последняя) нода в связном списке
        /// </summary>
        protected Node<T> LastNode;

        /// <summary>
        /// Количество элементов в связном списке
        /// </summary>
        public int Count { get; protected set; }

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

        /// <summary>
        /// Возвращает ноду по указанному индексу
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Нода по указанному индексу</returns>
        protected Node<T> GetNodeByIndex(int index) {
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
        /// Вставляет элемент в начало связного списка
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public virtual void AddFirst(T item) {
            this.Insert(0, item);
        }

        /// <summary>
        /// Вставляет элемент в конец связного списка
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
        /// Добавляет элемент в конец связного списка
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public virtual void Add(T item) => this.AddLast(item);

        /// <summary>
        /// Добавляет несколько элементов в связный список
        /// </summary>
        /// <param name="items">Добавляемые элементы</param>
        public void AddRange(IEnumerable<T> items) {
            foreach (var item in items) {
                this.Add(item);
            }
        }

        /// <summary>
        /// Объеденяет текщуий связный список с другим, добавляя его элементы в конец
        /// </summary>
        /// <param name="list">Другой двусвязный список</param>
        public void Append(MyLinkedList<T> list) {
            if (list.Count == 0) {
                return;
            }
            if (this.Count == 0) {
                this.HeadNode = list.HeadNode;
                this.LastNode = list.LastNode;
            } else {
                this.LastNode.Next = list.HeadNode;
                this.LastNode.Next.Previous = this.LastNode;
                this.LastNode = list.LastNode;
            }
            this.Count += list.Count;
        }

        /// <summary>
        /// Возвращает индекс указанного элемента в связном списке
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
        /// Вставляет элемент в связный список по указанному индексу
        /// </summary>
        /// <param name="index">Индекс, в который нужно вставить элемент</param>
        /// <param name="item">Элемент, который необходимо вставить в связный список</param>
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
        /// Проверяет наличие элемента в связном списке
        /// </summary>
        /// <param name="item">Искомый элемент</param>
        /// <returns>True - если элемент присутствует в связном списке, иначе False</returns>
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
        /// индекс выходит за границы связного списка</exception>
        public virtual void RemoveAt(int index) {
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
        /// Удаляет элемент из связного списка
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
        /// Очищает связный список
        /// </summary>
        public void Clear() {
            this.HeadNode = this.LastNode = null;
            this.Count = 0;
        }

        /// <summary>
        /// Перечисляет элементы связного списка в обратном порядке
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
        /// Возвращает строковое представление экземпляра связного списка
        /// </summary>
        /// <returns>Строковое представление экземпляра связного списка</returns>
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
        /// <summary>
        /// Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="IEnumerator{T}" />, который может использоваться
        /// для перебора коллекции
        /// </returns>
        public IEnumerator<T> GetEnumerator() {
            var tempNode = this.HeadNode;
            while (tempNode != null) {
                yield return tempNode.Value;
                tempNode = tempNode.Next;
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