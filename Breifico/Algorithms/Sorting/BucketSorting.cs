using System;
using Breifico.DataStructures;

namespace Breifico.Algorithms.Sorting
{
    public class BucketSorting<T> : ISorter<T> where T : IComparable<T>
    {
        private const int DefaultBucketsCount = 1000;

        private readonly MyLinkedList<T>[] _buckets;
        private readonly Func<T, T, int, int> _bucketSelectorFunction;

        public BucketSorting(Func<T, T, int, int> f) 
            : this(DefaultBucketsCount, f) {}

        public BucketSorting(int bucketsCount, Func<T, T, int, int> f) {
            this._buckets = new MyLinkedList<T>[bucketsCount];
            this._bucketSelectorFunction = f;
        }

        private T FindMaxElement(T[] input) {
            if (input.Length == 0) {
                throw new ArgumentException("Array is empty, can't find maximum element");
            }
            var maxElement = input[0];
            foreach (var iteritem in input) {
                if (iteritem.CompareTo(maxElement) == 1) {
                    maxElement = iteritem;
                }
            }
            return maxElement;
        }

        public T[] Sort(T[] input) {
            var maxElement = this.FindMaxElement(input);
            for (int i = 0; i < input.Length; i++) {
                int bucketNumber = this._bucketSelectorFunction(input[i], maxElement, this._buckets.Length);
                if (bucketNumber < 0 || bucketNumber >= this._buckets.Length) {
                    throw new Exception("Invalid bucket number, please check bucket selector function");
                }
                if (this._buckets[bucketNumber] == null) {
                    this._buckets[bucketNumber] = new MyLinkedList<T>();
                }
                this._buckets[bucketNumber].Add(input[i]);
            }
            return new T[0];
        }
    }
}
