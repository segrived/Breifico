using System;
using System.Collections.Generic;

namespace Breifico
{
    /// <summary>
    /// Предоставляет функцию для сортировки массивов
    /// </summary>
    /// <typeparam name="T">Тип элементов в массиве</typeparam>
    public interface ISorter<T> where T : IComparable<T>
    {
        /// <summary>
        /// Сортирует массив на месте
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>Отсортированный массив</returns>
        T[] Sort(T[] input);
    }

    /// <summary>
    /// Предоставляет функции для поиска в коллекции
    /// </summary>
    /// <typeparam name="T">Тип элементов в коллекции</typeparam>
    public interface ISearcher<T> where T : IComparable<T>
    {
        /// <summary>
        /// Выполняет поиск в коллекции и возвращает индекс искомного элемента
        /// Если элемент отсутствует в коллекции функция должна вернуть -1
        /// </summary>
        /// <param name="input">Исходная коллекция</param>
        /// <param name="element">Искомый элемент</param>
        /// <returns>Индекс найденного элемента</returns>
        int Search(IList<T> input, T element);
    }
}
