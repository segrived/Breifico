using System;
using System.Collections.Generic;

namespace Breifico.Algorithms.Searching
{
    public class BinarySearch<T> : ISearcher<T> where T : IComparable<T>
    {
        /// <summary>
        /// Выполняет поиск в коллекции и возвращает индекс искомного элемента
        /// Если элемент отсутствует в коллекции функция должна вернуть -1
        /// </summary>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="element">Искомый элемент</param>
        /// <returns>Индекс найденного элемента</returns>
        public int Search(IList<T> input, T element) {
            // для пустой коллекции всегда возвращаем -1
            if (input.Count == 0) {
                return -1;
            }
            // если в коллекции один элемент, определят срау же, является ли он искомым
            if (input.Count == 1) {
                return input[0].Equals(element) ? 0 : -1;
            }
            int left = 0;
            int right = input.Count - 1;

            while (left < right) {
                int midPoint = left + (right - left) / 2;

                switch (input[midPoint].CompareTo(element)) {
                    case 0:
                        return midPoint;
                    case -1:
                        left = midPoint + 1;
                        break;
                    default:
                        right = midPoint - 1;
                        break;
                }
            }
            return input[left].CompareTo(element) == 0 ? left : -1;
        }
    }
}
