using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация бинарного дерева поиска
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerDisplay("MyBinarySearchTree<T>: {Count} element(s)")]
    public class MyBinarySearchTree<T> : IEnumerable<T>, ICollection where T : IComparable<T>
    {
        private enum NodePosition { Left, Right }

        /// <summary>
        /// Нода бинарного дерева
        /// </summary>
        /// <typeparam name="TR">Тип элементов в бинарном дереве. Тип должен
        /// имплементировать интерфейс <see cref="IComparable{T}"/></typeparam>
        [DebuggerDisplay("{Value}")]
        public class Node<TR> where TR : IComparable<TR>
        {
            /// <summary>
            /// Левая нода
            /// </summary>
            public Node<TR> Left { get; set; }

            /// <summary>
            /// Правая нода
            /// </summary>
            public Node<TR> Right { get; set; }

            /// <summary>
            /// Значение текущей ноды
            /// </summary>
            public TR Value { get; set; }

            /// <summary>
            /// Создает новую ноду с указанным значением
            /// </summary>
            /// <param name="value"></param>
            public Node(TR value) {
                this.Value = value;
            }

            /// <summary>
            /// Возвращает True, если нода является листом (не имеет детей)
            /// </summary>
            public bool IsLeaf => this.Left == null && this.Right == null;

            /// <summary>
            /// Возвращает True, если нода имеет хоть одного ребенка
            /// </summary>
            public bool HasOnlyOneChild =>
                (this.Left != null && this.Right == null) 
             || (this.Right != null && this.Left == null);
            
            /// <summary>
            /// Строковое представление экземпляра
            /// </summary>
            /// <returns></returns>
            public override string ToString() {
                return $"{this.Value}";
            }
        }

        private object _syncRoot;

        private Node<T> _rootNode;

        /// <summary>
        /// Количество элементов в дереве
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Добавляет новый элемент в дерево
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public void Add(T item) {
            if (this._rootNode == null) {
                this._rootNode = new Node<T>(item);
                this.Count += 1;
                return;
            }
            var currentNode = this._rootNode;
            while (true) {
                int compareResult = item.CompareTo(currentNode.Value);
                // игноритровать дубликаты
                if (compareResult == 0) {
                    return;
                }
                if (compareResult < 0) {
                    if (currentNode.Left == null) {
                        currentNode.Left = new Node<T>(item);
                        break;
                    }
                    currentNode = currentNode.Left;
                } else {
                    if (currentNode.Right == null) {
                        currentNode.Right = new Node<T>(item);
                        break;
                    }
                    currentNode = currentNode.Right;
                }
            }
            this.Count += 1;
        }

        /// <summary>
        /// Проверяет наличие элемента в коллекции
        /// </summary>
        /// <param name="item">Искомый элемент</param>
        /// <returns>True если элемент в коллекции найден, иначе False</returns>
        public bool Contains(T item) {
            var node = this.FindNode(item);
            return node != null;
        }

        /// <summary>
        /// Обертка над нодой, содержит ссылку на родительскую ноду и путь
        /// к указанной ноде (влево или вправо)
        /// </summary>
        /// <typeparam name="TR">Тип элемента внутри ноды</typeparam>
        class SmartNode<TR> where TR: IComparable<TR>
        {
            /// <summary>
            /// Исходная нода
            /// </summary>
            public Node<TR> RefNode { get; }

            /// <summary>
            /// Родитель исходной ноды
            /// </summary>
            public Node<TR> Parent { get; }

            /// <summary>
            /// Положение исходной ноды по отношению к родителю
            /// </summary>
            private NodePosition Position { get; }

            /// <summary>
            /// Создает новую обертку над нодой
            /// </summary>
            /// <param name="refNode">Исходная нода</param>
            /// <param name="parent">Родитель исходной ноды</param>
            /// <param name="position">Положение исходной ноды по отношению к родителю</param>
            public SmartNode(Node<TR> refNode, Node<TR> parent, NodePosition position) {
                this.RefNode = refNode;
                this.Parent = parent;
                this.Position = position;
            }

            /// <summary>
            /// Заменяет исходную ноду новой нодой
            /// </summary>
            /// <param name="newNode">Новая нода, которая заменит исходную</param>
            public void ReplaceNode(Node<TR> newNode) {
                if (this.Position == NodePosition.Left) {
                    this.Parent.Left = newNode;
                } else {
                    this.Parent.Right = newNode;
                }
            }

            /// <summary>
            /// Изменяет значение текущей ноды
            /// </summary>
            /// <param name="newValue"></param>
            public void ReplaceNodeValue(TR newValue) {
                this.RefNode.Value = newValue;
            }

            /// <summary>
            /// Удаляет текущую ноду
            /// </summary>
            public void RemoveNode() => this.ReplaceNode(null);
        }

        /// <summary>
        /// Ищет в дереве указанную ноду
        /// </summary>
        /// <param name="item">Искомая нода</param>
        /// <returns>Экземпляр класса SmartNode если элемент был найден.
        /// Если искомая нода отсуствует в коллекции, функция вернет null</returns>
        private SmartNode<T> FindNode(Node<T> item) {
            return this.FindNode(item.Value);
        }

        /// <summary>
        /// Ищет в дереве указанную ноду по значению
        /// </summary>
        /// <param name="item">Значение искомой ноды</param>
        /// <returns>Экземпляр класса SmartNode если элемент был найден.
        /// Если искомый элемент отсуствует в коллекции, функция вернет null</returns>
        private SmartNode<T> FindNode(T item) {
            var tempNode = this._rootNode;
            Node<T> parent = null;
            var nodePosition = NodePosition.Left;

            while (tempNode != null) {
                int compResult = item.CompareTo(tempNode.Value);
                if (compResult == 0) {
                    return new SmartNode<T>(tempNode, parent, nodePosition);
                }
                if (compResult < 0) {
                    parent = tempNode;
                    nodePosition = NodePosition.Left;
                    tempNode = tempNode.Left;
                } else {
                    parent = tempNode;
                    nodePosition = NodePosition.Right;
                    tempNode = tempNode.Right;
                }
            }
            return null;
        } 

        /// <summary>
        /// Удаляет элемент из коллекции
        /// </summary>
        /// <param name="item">Удаляемый элемент</param>
        public void Remove(T item) {
            var node = this.FindNode(item);
            if (node == null) {
                return;
            }
            // Если нода - лист (у нее нет детей)
            if (node.RefNode.IsLeaf) {
                if (node.Parent == null) {
                    this._rootNode = null;
                } else {
                    node.RemoveNode();
                }
            // Если у ноды один ребенок
            } else if (node.RefNode.HasOnlyOneChild) {
                var newNode = node.RefNode.Left ?? node.RefNode.Right;
                // if root with only one child
                if (node.Parent == null) {
                    this._rootNode = newNode;
                } else {
                    node.ReplaceNode(newNode);
                }
            // Если у ноды два ребенка
            } else {
                // TODO: оптимизировать удаление ноды в данном ситуации
                var x = node.RefNode.Right;
                var parent = node.RefNode;
                bool isLeftNode = false;
                while (x != null) {
                    if (x.Left == null) {
                        var nodeValue = x.Value;
                        this.Remove(isLeftNode ? parent.Left.Value : parent.Right.Value);
                        node.ReplaceNodeValue(nodeValue);
                        return;
                    }
                    parent = x;
                    x = x.Left;
                    isLeftNode = true;
                }
            }
        }

        /// <summary>
        /// Ищет ноду с минимальным элементов
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        private Node<T> FindMinimumNode(Node<T> start) {
            var tempNode = start;
            while (tempNode != null) {
                if (tempNode.Left == null) {
                    return tempNode;
                }
                tempNode = tempNode.Left;
            }
            return null;
        }

        /// <summary>
        /// Ищет ноду с максимальным элементом
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        private Node<T> FindMaximumNode(Node<T> start) {
            var tempNode = start;
            while (tempNode != null) {
                if (tempNode.Right == null) {
                    return tempNode;
                }
                tempNode = tempNode.Right;
            }
            return null;
        }

        /// <summary>
        /// Ищет минимальный элемент в коллекции. 
        /// Если элемент отсутствует в коллекции - функция вернет указанное
        /// значение, либо значение по умолчанию
        /// </summary>
        /// <param name="def">Значение, которое вернет функция, если элемент в коллекции отсутствует</param>
        /// <returns>Минимальный элемент или значение по умолчанию</returns>
        public T FindMinimum(T def = default(T)) {
            var node = this.FindMinimumNode(this._rootNode);
            return node == null ? def : node.Value;
        }

        /// <summary>
        /// Ищет максимальный элемент в коллекции. 
        /// Если элемент отсутствует в коллекции - функция вернет указанное
        /// значение, либо значение по умолчанию 
        /// </summary>
        /// <param name="def">Значение, которое вернет функция, если элемент в коллекции отсутствует</param>
        /// <returns>Максимальный элемент или значение по умолчанию</returns>
        public T FindMaximum(T def = default(T)) {
            var node = this.FindMaximumNode(this._rootNode);
            return node == null ? def : node.Value;
        }

        /// <summary>
        /// Добавляет элементы указанной коллекции в бинарное дерево
        /// </summary>
        /// <param name="items">Коллекция с добавляемыми элементами</param>
        public void AddRange(IEnumerable<T> items) {
            foreach (var item in items) {
                this.Add(item);
            }
        }

        #region IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator() {
            var stack = new MyStack<Node<T>>();
            var node = this._rootNode;
            while (stack.Count != 0 || node != null) {
                if (node != null) {
                    stack.Push(node);
                    node = node.Left;
                } else {
                    node = stack.Pop();
                    yield return node.Value;
                    node = node.Right;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        #endregion

        #region ICollection implementation
        public void CopyTo(Array array, int index) {
            Array.Copy(this.ToArray(), 0, array, index, this.Count);
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
}