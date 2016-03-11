using System;
using Breifico.DataStructures;

namespace Breifico.Algorithms.Sorting
{
    /// <summary>
    /// Блочная сортировка. Работает за O(N^2) в худшем случае и O(N+K) в среднем
    /// </summary>
    /// <typeparam name="T">Тип элементов, которые необходимо отсортировать</typeparam>
    public class BucketSorting<T> : ISorter<T> where T : IComparable<T>
    {
        /// <summary>
        /// Количество блоков по умолчанию
        /// </summary>
        private const int DefaultBucketsCount = 1000;

        private readonly MyLinkedList<T>[] _buckets;
        private readonly Func<T, T, T, int, int> _bucketSelectorFunction;

        /// <summary>
        /// Создает новый экземпляр <see cref="BucketSorting{T}"/> с функцией распределения 
        /// элементов по блокам и количеством блоков по умолчанию
        /// </summary>
        /// <param name="f"></param>
        public BucketSorting(Func<T, T, T, int, int> f) 
            : this(DefaultBucketsCount, f) {}

        /// <summary>
        /// Создает новый экземпляр <see cref="BucketSorting{T}"/> с указанным количеством блоков 
        /// и функцией распределения элементов по блокам
        /// </summary>
        /// <param name="bucketsCount">Колиество блоков</param>
        /// <param name="f">Функция распределения элементов по блокам</param>
        public BucketSorting(int bucketsCount, Func<T, T, T, int, int> f) {
            this._buckets = new MyLinkedList<T>[bucketsCount];
            this._bucketSelectorFunction = f;
        }

        /// <summary>
        /// Ищет минимальный и максимальный элемент в массиве
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>Кортеж, где первое значение - минимальный элемент, а
        /// второе - максимальное</returns>
        private Tuple<T, T> FindMaxElement(T[] input) {
            if (input.Length == 0) {
                throw new ArgumentException("Array is empty, can't find maximum element");
            }
            var minElement = input[0];
            var maxElement = input[0];
            foreach (var iterItem in input) {
                if (iterItem.CompareTo(maxElement) == 1) {
                    maxElement = iterItem;
                } else if (iterItem.CompareTo(minElement) == -1) {
                    minElement = iterItem;
                }
            }
            return Tuple.Create(minElement, maxElement);
        }

        /// <summary>
        /// Сортирует in-place исходный массив методом блочной сортировки и возвращает его
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>Отсортированный массив</returns>
        public T[] Sort(T[] input) {
            var minmax = this.FindMaxElement(input);
            for (int i = 0; i < input.Length; i++) {
                int bucketNumber = this._bucketSelectorFunction(input[i], 
                    minmax.Item1, minmax.Item2, this._buckets.Length);
                // функция не должна возвращать значение меньше нуля или больше, чем
                // общее число блоков
                if (bucketNumber < 0 || bucketNumber >= this._buckets.Length) {
                    throw new Exception("Invalid bucket number, please check bucket selector function");
                }
                if (this._buckets[bucketNumber] == null) {
                    this._buckets[bucketNumber] = new MyLinkedList<T>();
                }
                this._buckets[bucketNumber].Add(input[i]);
            }
            int outIndex = 0;
            for (int i = 0; i < this._buckets.Length; i++) {
                // если блок пуст
                if (this._buckets[i] == null || this._buckets[i].Count == 0) {
                    continue;
                }
                var enumerator = this._buckets[i].GetEnumerator();
                while (enumerator.MoveNext()) {
                    input[outIndex++] = enumerator.Current;
                }
            }
            return input;
        }
    }
}
