using System;
using System.Collections.Generic;

namespace Breifico.Algorithms.Searching
{
    /// <summary>
    /// Имплементация линейного поиска. Работает за O(N)
    /// </summary>
    /// <typeparam name="T">Тип элементов в коллекции</typeparam>
    public sealed class LinearSearch<T> : ISearcher<T> where T : IComparable<T>
    {
        /// <summary>
        /// Выполняет поиск в коллекции и возвращает индекс искомного элемента
        /// Если элемент отсутствует в коллекции функция должна вернуть -1
        /// </summary>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="element">Искомый элемент</param>
        /// <returns>Индекс найденного элемента</returns>
        public int Search(IList<T> input, T element) {
            for (int i = 0; i < input.Count; i++) {
                if (input[i].Equals(element)) {
                    return i;
                }
            }
            return -1;
        }
    }
}