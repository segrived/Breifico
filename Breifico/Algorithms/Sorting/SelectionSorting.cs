using System;

namespace Breifico.Algorithms.Sorting
{
    /// <summary>
    /// Сортировка вставками. Работает за O(N^2)
    /// </summary>
    /// <typeparam name="T">Тип элементов, которые необходимо отсортировать</typeparam>
    public class SelectionSorting<T> : ISorter<T> where T : IComparable<T>
    {
        public T[] Sort(T[] input) {
            for (int i = 0; i < input.Length - 1; i++) {
                int minIndex = i;
                for (int j = i + 1; j < input.Length; j++) {
                    if (input[minIndex].CompareTo(input[j]) == 1) {
                        minIndex = j;
                    }
                }
                CommonHelpers.Swap(ref input[i], ref input[minIndex]);
            }
            return input;
        }
    }
}