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

        private readonly MySortedLinkedList<T>[] _buckets;
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
            this._buckets = new MySortedLinkedList<T>[bucketsCount];
            this._bucketSelectorFunction = f;
        }

        /// <summary>
        /// Ищет минимальный и максимальный элемент в массиве
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>Кортеж, где первое значение - минимальный элемент, а
        /// второе - максимальное</returns>
        private Tuple<T, T> FindMinMaxElement(T[] input) {
            var minElement = input[0];
            var maxElement = input[0];
            foreach (var iterItem in input) {
                if (iterItem.CompareTo(maxElement) > 0) {
                    maxElement = iterItem;
                } else if (iterItem.CompareTo(minElement) < 0) {
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
            if (input.Length <= 1) {
                return input;
            }
            var minmax = this.FindMinMaxElement(input);
            for (int i = 0; i < input.Length; i++) {
                int bucketNumber = this._bucketSelectorFunction(input[i], 
                    minmax.Item1, minmax.Item2, this._buckets.Length - 1);
                // функция не должна возвращать значение меньше нуля или больше, чем
                // общее число блоков
                if (bucketNumber < 0 || bucketNumber >= this._buckets.Length) {
                    throw new Exception("Invalid bucket number, please check bucket selector function");
                }
                if (this._buckets[bucketNumber] == null) {
                    this._buckets[bucketNumber] = new MySortedLinkedList<T>();
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

    /// <summary>
    /// Реализация блочной сортировки для целых 32-битных чисел
    /// </summary>
    public sealed class BucketIntSorting : BucketSorting<int>
    {
        private static readonly Func<int, int, int, int, int> Sorter = (i, min, max, len) 
            => (int)Math.Round(((double)i - min) / (max - min) * len);

        /// <summary>
        /// Создает новый экземпляр <see cref="BucketIntSorting"/>
        /// </summary>
        public BucketIntSorting() : base(Sorter) {}

        /// <summary>
        /// Создает новый экземпляр <see cref="BucketIntSorting"/> с указанным количеством блоков
        /// </summary>
        /// <param name="bucketsCount">Количество блоков</param>
        public BucketIntSorting(int bucketsCount) 
            : base(bucketsCount, Sorter) {}
    }
}
