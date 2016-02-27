using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Breifico.DataStructures.Interfaces;

namespace Breifico.DataStructures
{
    [DebuggerDisplay("{Count} elements")]
    public class SinglyLinkedList<T> : ILinkedList<T>, IReversible<T>
    {
        [DebuggerDisplay("{Value} -> {Next}")]
        private class Node<TR>
        {
            public Node(TR value) {
                this.Value = value;
            }

            internal Node<TR> Next { get; set; }

            public TR Value { get; }

            public override string ToString() {
                return this.Value.ToString();
            }
        }

        private Node<T> _headNode;
        private Node<T> _lastNode;

        public int Count { get; private set; }

        public SinglyLinkedList() {
            this._headNode = null;
            this._lastNode = null;
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

        public void Add(T value) => this.Insert(value, this.Count);

        public void Insert(T value, int position) {
            if (position < 0 || position > this.Count) {
                throw new IndexOutOfRangeException();
            }
            var newNode = new Node<T>(value);
            if (this._headNode == null || this._lastNode == null) {
                this._headNode = newNode;
                this._lastNode = this._headNode;
            } else if (position == this.Count) {
                this._lastNode.Next = newNode;
                this._lastNode = this._lastNode.Next;
            } else if (position == 0) {
                var oldHeadNode = this._headNode;
                this._headNode = new Node<T>(value);
                this._headNode.Next = oldHeadNode;
            } else {
                var node = this.GetNodeByIndex(position - 1);
                newNode.Next = node.Next;
                node.Next = newNode;
            }
            this.Count += 1;
        }

        public T Get(int index) {
            return this.GetNodeByIndex(index).Value;
        }

        public void Reverse() {
            if (this.Count <= 1) {
                return;
            }
            Node<T> p = this._headNode, n = null;
            while (p != null) {
                var tmp = p.Next;
                p.Next = n;
                n = p;
                p = tmp;
            }
            this._headNode = n;
        }

        public void Remove(int index) {
            if (index < 0 || index >= this.Count) {
                throw new IndexOutOfRangeException();
            }
            if (index == 0) {
                this._headNode = this._headNode.Next;
            } else {
                var node = this.GetNodeByIndex(index - 1);
                node.Next = node.Next.Next;
            }
            this.Count -= 1;
        }

        public void Clear() {
            this._headNode = this._lastNode = null;
            this.Count = 0;
        }

        public override string ToString() {
            string headNodeText = this._headNode?.ToString() ?? "null";
            string lastNodeText = this._lastNode?.ToString() ?? "null";
            return $"S: {headNodeText}, E: {lastNodeText}";
        }

        #region IEnumerable implementation
        public IEnumerator<T> GetEnumerator() {
            var tempNode = this._headNode;
            for (int i = 0; i < this.Count; i++) {
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