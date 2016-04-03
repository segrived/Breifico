using System;

namespace Breifico.Algorithms.Sorting
{
    /// <summary>
    /// Сортировка вставками. Работает за O(N^2)
    /// </summary>
    /// <typeparam name="T">Тип элементов, которые необходимо отсортировать</typeparam>
    public sealed class SelectionSorting<T> : ISorter<T> where T : IComparable<T>
    {
        /// <summary>
        /// Сортирует in-place исходный массив методом сортировки выбором и возвращает его
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>Отсортированный массив</returns>
        public T[] Sort(T[] input) {
            for (int i = 0; i < input.Length - 1; i++) {
                int minIndex = i;
                for (int j = i + 1; j < input.Length; j++) {
                    if (input[minIndex].CompareTo(input[j]) > 0) {
                        minIndex = j;
                    }
                }
                CommonHelpers.Swap(ref input[i], ref input[minIndex]);
            }
            return input;
        }
    }
}