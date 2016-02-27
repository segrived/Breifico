using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Breifico.DataStructures.Interfaces;

namespace Breifico.DataStructures
{
    [DebuggerDisplay("{Count} elements")]
    public class DoubleLinkedList<T> : ILinkedList<T>, IReversible<T>
    {
        [DebuggerDisplay("{Previous} <- {Value} -> {Next}")]
        internal class Node<TR>
        {
            public Node(T value) {
                this.Value = value;
            }

            internal Node<TR> Previous { get; set; }
            internal Node<TR> Next { get; set; }

            public T Value { get; }

            public override string ToString() {
                return this.Value.ToString();
            }
        }

        private Node<T> _headNode;
        private Node<T> _lastNode;

        public int Count { get; private set; }

        public void Add(T value) => this.Insert(value, this.Count);

        public void Insert(T value, int position) {
            if (position < 0 || position > this.Count) {
                throw new IndexOutOfRangeException();
            }
            var newNode = new Node<T>(value);
            if (this._headNode == null || this._lastNode == null) {
                this._headNode = this._lastNode = newNode;
            } else if (position == this.Count) {
                newNode.Previous = this._lastNode;
                this._lastNode.Next = newNode;
                this._lastNode = this._lastNode.Next;
            } else if (position == 0) {
                newNode.Next = this._headNode;
                this._headNode.Previous = newNode;
                this._headNode = newNode;
            } else {
                var node = this.GetNodeByIndex(position);
                newNode.Next = node;
                newNode.Previous = node.Previous;
                newNode.Previous.Next = newNode;
                newNode.Next.Previous = newNode;
            }
            this.Count += 1;
        }

        private Node<T> GetNodeByIndex(int index) {
            if (index < 0 || index >= this.Count) {
                throw new IndexOutOfRangeException();
            }
            var tempNode = this._headNode;
            for (int i = 0; i < index; i++) {
                tempNode = tempNode.Next;
            }
            return tempNode;
        }

        public T Get(int index) {
            return this.GetNodeByIndex(index).Value;
        }

        public void Remove(int index) {
            if (index < 0 || index >= this.Count) {
                throw new IndexOutOfRangeException();
            }
            if (index == 0) {
                this._headNode = this._headNode.Next;
                if (this._headNode != null) {
                    this._headNode.Previous = null;
                }
            } else if (index == this.Count - 1) {
                this._lastNode = this._lastNode.Previous;
                this._lastNode.Next = null;
            } else {
                var node = this.GetNodeByIndex(index);
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
            }
            this.Count -= 1;
        }

        public void Clear() {
            this._headNode = this._lastNode = null;
            this.Count = 0;
        }

        public void Reverse() {
            if (this.Count <= 1) {
                return;
            }
            var initStartNode = this._headNode;
            var tempNode = this._lastNode;
            while(tempNode != null) {
                var nextNode = tempNode.Next;
                tempNode.Next = tempNode.Previous;
                tempNode.Previous = nextNode;
                tempNode = tempNode.Next;
            }
            this._headNode = this._lastNode;
            this._lastNode = initStartNode;
        }

        public IEnumerable<T> ReverseEnumerable() {
            if (this._lastNode == null) {
                yield break;
            }
            var tempNode = this._lastNode;
            while (tempNode != null) {
                yield return tempNode.Value;
                tempNode = tempNode.Previous;
            }
        }

        public override string ToString() {
            string startNodeText = this._headNode?.ToString() ?? "null";
            string endNodeText = this._lastNode?.ToString() ?? "null";
            return $"S: {startNodeText}, E: {endNodeText}";
        }

        #region IEnumerable implementation members
        public IEnumerator<T> GetEnumerator() {
            if (this._headNode == null) {
                yield break;
            }
            var tempNode = this._headNode;
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
}