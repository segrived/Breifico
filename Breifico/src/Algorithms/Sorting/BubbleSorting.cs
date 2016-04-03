using System;

namespace Breifico.Algorithms.Sorting
{
    /// <summary>
    /// Пузырьковая сортировка. Работает за O(N^2)
    /// </summary>
    /// <typeparam name="T">Тип элементов, которые необходимо отсортировать</typeparam>
    public sealed class BubbleSorting<T> : ISorter<T> where T : IComparable<T>
    {
        /// <summary>
        /// Сортирует in-place исходный массив методом пузырька и возвращает его
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>Отсортированный массив</returns>
        public T[] Sort(T[] input) {
            if (input.Length <= 1) {
                return input;
            }
            if (input.Length == 2) {
                if (input[0].CompareTo(input[1]) > 0)
                    CommonHelpers.Swap(ref input[0], ref input[1]);
                return input;
            }
            for (int i = input.Length - 2; i >= 0; i--) {
                bool wasChanged = false;
                for (int j = 0; j <= i; j++) {
                    if (input[j].CompareTo(input[j + 1]) < 0) {
                        // если за последний проход ничего не изменилось - 
                        // значит уже коллекция отсортирована
                        continue;
                    }
                    CommonHelpers.Swap(ref input[j], ref input[j + 1]);
                    wasChanged = true;
                }
                if (!wasChanged) {
                    break;
                }
            }
            return input;
        }
    }
}
