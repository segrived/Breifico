using System;
using System.Collections;

namespace Breifico.DataStructures
{
    public class MyStack<T>
    {
        public const int InitStackSize = 8;

        private T[] _stackInternalArray;


        public int Count { get; private set; } = 0;


        public MyStack() {
            this._stackInternalArray = new T[InitStackSize];
        }

        public bool HasElements() {
            return this.Count > 0;
        }

        public T Push() {
            if (this.Count == 0) {
                throw new InvalidOperationException("Empty stack");
            }
            var item = this._stackInternalArray[this.Count - 1];
            this.Count -= 1;
            return item;
        }

        public IEnumerator GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
