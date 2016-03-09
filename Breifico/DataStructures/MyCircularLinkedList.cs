using System;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация циклического связного списка
    /// </summary>
    /// <typeparam name="T">Тип элементов в циклическом связном списке</typeparam>
    public class MyCircularLinkedList<T> : MyLinkedList<T>
    {
        /// <summary>
        /// Вставляет элемент в циклический связный список
        /// </summary>
        /// <param name="index">Позиция, куда необходимо вставить элемент</param>
        /// <param name="item">Добавляемый элемент</param>
        public override void Insert(int index, T item) {
            if (index < 0 || index > this.Count) {
                throw new IndexOutOfRangeException();
            }
            var newNode = new Node<T>(item);
            if (this.Count == 0) {
                // Если список пуст
                newNode.Next = newNode;
                newNode.Previous = newNode;
                this.HeadNode = this.LastNode = newNode;
            } else if (index == 0) {
                // Добавление в начало списка
                var oldHeadNode = this.HeadNode;
                this.HeadNode = newNode;
                this.HeadNode.Next = oldHeadNode;
                oldHeadNode.Previous = this.HeadNode;
                this.HeadNode.Previous = this.LastNode;
                this.LastNode.Next = this.HeadNode;
            } else if (index == this.Count) {
                // Добавление в конец списка
                this.LastNode.Next = newNode;
                newNode.Previous = this.LastNode;
                this.LastNode = newNode;
                this.LastNode.Next = this.HeadNode;
                this.HeadNode.Previous = this.LastNode;
            } else {
                // Добавление в середину списка
                var node = this.GetNodeByIndex(index);
                newNode.Next = node;
                newNode.Previous = node.Previous;
                newNode.Previous.Next = newNode;
                newNode.Next.Previous = newNode;
            }
            this.Count += 1;
        }

        /// <summary>
        /// Удаляет элемент с указанным индексом
        /// </summary>
        /// <param name="index">Индекс удаляемого элемента</param>
        /// <exception cref="IndexOutOfRangeException">Бросается в случае, если указанный 
        /// индекс выходит за границы циклического связного списка</exception>
        public override void RemoveAt(int index) {
            if (index < 0 || index >= this.Count) {
                throw new IndexOutOfRangeException();
            }
            // Удаление первого элемента
            if (index == 0) {
                if (this.Count == 1) {
                    this.HeadNode = this.LastNode = null;
                } else {
                    this.HeadNode = this.HeadNode.Next;
                    this.HeadNode.Previous = this.LastNode;
                    this.LastNode.Next = this.HeadNode;
                }
            } else if (index == this.Count - 1) {
                if (this.Count == 1) {
                    this.HeadNode = this.LastNode = null;
                } else {
                    this.LastNode = this.LastNode.Previous;
                    this.LastNode.Next = this.HeadNode;
                    this.HeadNode.Previous = this.LastNode;
                }
            } else {
                var node = this.GetNodeByIndex(index - 1);
                node.Next = node.Next.Next;
            }
            this.Count -= 1;
        }
    }
}
