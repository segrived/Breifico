using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Breifico.DataStructures.Interfaces;

namespace Breifico.DataStructures
{
    [DebuggerDisplay("MyLinkedList<T>: {Count} element(s)")]
    public class MyLinkedList<T> : IMyDoublyLinkedList<T>
    {
        [DebuggerDisplay("{Previous} <- {Value} -> {Next}")]
        internal class Node<TR>
        {
            public Node(TR value) {
                this.Value = value;
            }

            internal Node<TR> Previous { get; set; }
            internal Node<TR> Next { get; set; }

            public TR Value { get; set; }

            public override string ToString() {
                return this.Value.ToString();
            }
        }

        private object _syncRoot;

        private Node<T> _headNode;
        private Node<T> _lastNode;

        public int Count { get; private set; }

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

        public T this[int index]
        {
            get { return this.GetNodeByIndex(index).Value; }
            set { this.GetNodeByIndex(index).Value = value; }
        }

        public void Add(T item) {
            this.Insert(item, this.Count);
        }

        public void AddRange(IEnumerable<T> items) {
            foreach (var item in items) {
                this.Add(item);
            }
        }

        public void Insert(T item, int position) {
            if (position < 0 || position > this.Count) {
                throw new IndexOutOfRangeException();
            }
            var newNode = new Node<T>(item);
            if (this.Count == 0) {
                this._headNode = this._lastNode = newNode;
            } else if (position == 0) {
                var oldHeadNode = this._headNode;
                this._headNode = newNode;
                this._headNode.Next = oldHeadNode;
                oldHeadNode.Previous = this._headNode;
            } else if (position == this.Count) {
                this._lastNode.Next = newNode;
                newNode.Previous = this._lastNode;
                this._lastNode = newNode;
            } else {
                var node = this.GetNodeByIndex(position);
                newNode.Next = node;
                newNode.Previous = node.Previous;
                newNode.Previous.Next = newNode;
                newNode.Next.Previous = newNode;
            }
            this.Count += 1;
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
                if (this._lastNode != null) {
                    this._lastNode.Next = null;
                }
            } else {
                var node = this.GetNodeByIndex(index - 1);
                node.Next = node.Next.Next;
            }
            this.Count -= 1;
        }

        public bool RemoveFirst(T item) {
            var tempNode = this._headNode;
            for (int i = 0; i < this.Count; i++) {
                if (tempNode.Value.Equals(item)) {
                    this.Remove(i);
                    return true;
                }
                tempNode = tempNode.Next;
            }
            return false;
        }

        public void Clear() {
            this._headNode = this._lastNode = null;
            this.Count = 0;
        }

        public bool Contains(T item) {
            var tempNode = this._headNode;
            while (tempNode != null) {
                if (tempNode.Value.Equals(item)) {
                    return true;
                }
                tempNode = tempNode.Next;
            }
            return false;
        }

        public override string ToString() {
            string headNodeText = this._headNode?.ToString() ?? "null";
            string lastNodeText = this._lastNode?.ToString() ?? "null";
            return $"S: {headNodeText}, E: {lastNodeText}";
        }

        public IEnumerable<T> ReverseEnumerate() {
            var tempNode = this._lastNode;
            while (tempNode != null) {
                yield return tempNode.Value;
                tempNode = tempNode.Previous;
            }
        }

        #region ICollection implementation
        public void CopyTo(Array array, int index) {
            throw new NotImplementedException();
        }

        public object SyncRoot
        {
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

        #region IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator() {
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