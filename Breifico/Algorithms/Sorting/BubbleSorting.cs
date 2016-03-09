using System;

namespace Breifico.Algorithms.Sorting
{
    /// <summary>
    /// Пузьрькова сортировка. Работает за O(N^2)
    /// </summary>
    /// <typeparam name="T">Тип элементов, которые необходимо отсортировать</typeparam>
    public class BubbleSorting<T> : ISorter<T> where T : IComparable<T>
    {
        public T[] Sort(T[] input) {
            if (input.Length <= 1) {
                return input;
            }
            if (input.Length == 2) {
                if (input[0].CompareTo(input[1]) == 1)
                    CommonHelpers.Swap(ref input[0], ref input[1]);
                return input;
            }
            for (int i = input.Length - 2; i >= 0; i--) {
                bool wasChanged = false;
                for (int j = 0; j <= i; j++) {
                    if (input[j].CompareTo(input[j + 1]) == -1) {
                        // если за последний проход ничего не изменилось - значит коллекция отсортирована
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
