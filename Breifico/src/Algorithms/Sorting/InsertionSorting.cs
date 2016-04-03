using System;

namespace Breifico.Algorithms.Sorting
{
    /// <summary>
    /// Сортировка вставками. Работает за O(N^2)
    /// </summary>
    /// <typeparam name="T">Тип элементов, которые необходимо отсортировать</typeparam>
    public sealed class InsertionSorting<T> : ISorter<T> where T : IComparable<T>
    {
        /// <summary>
        /// Сортирует in-place исходный массив методом вставок и возвращает его
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>Отсортированный массив</returns>
        public T[] Sort(T[] input) {
            for (int i = 1; i < input.Length; i++) {
                var item = input[i];
                int insertIndex = 0;
                for (int j = insertIndex; j <= i; j++) {
                    if (item.CompareTo(input[j]) > 0) {
                        continue;
                    }
                    insertIndex = j;
                    break;
                }
                for (int k = i; k > insertIndex; k--) {
                    input[k] = input[k - 1];
                }
                input[insertIndex] = item;
            }
            return input;
        }
    }
}