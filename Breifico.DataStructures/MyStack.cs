using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Breifico.DataStructures.Interfaces;

namespace Breifico.DataStructures
{
    [DebuggerDisplay("MyStack<T>: {Count} element(s)")]
    public class MyStack<T> : IMyStack<T>
    {
        private readonly MyLinkedList<T> _stackData
            = new MyLinkedList<T>();

        private object _syncRoot;

        private int LastIndex => this.Count - 1;

        public int Count => this._stackData.Count;
        public bool IsEmpty => this.Count == 0;

        public MyStack() {}

        public MyStack(IEnumerable<T> input) {
            foreach (var item in input) {
                this.Push(item);
            }
        } 

        public void Clear() {
            this._stackData.Clear();
        }

        public T Pop() {
            if (this.IsEmpty) {
                throw new InvalidOperationException("Stack is empty");
            }
            var item = this._stackData[this.LastIndex];
            this._stackData.Remove(this.LastIndex);
            return item;
        }

        public T Peek() {
            if (this.IsEmpty) {
                throw new InvalidOperationException("Stack is empty");
            }
            return this._stackData[this.LastIndex];
        }

        public void Push(T value) {
            this._stackData.Add(value);
        }

        #region IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator() {
            return this._stackData.ReverseEnumerate().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        #endregion

        #region ICollection implementation
        public void CopyTo(Array array, int index) {
            throw new NotImplementedException();
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