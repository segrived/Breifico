using System;
using System.Collections.Generic;
using System.Drawing;

namespace Breifico
{
    /// <summary>
    /// Изображение, с указанной высотой, шириной и возможностью получения/установки
    /// значения пикселя по указанным координатам
    /// </summary>
    public interface IImage
    {
        /// <summary>
        /// Ширина изображения
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Высота изображения
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Получает или устанавливает значение указанного пикселя
        /// </summary>
        /// <param name="x">Строка в изображении</param>
        /// <param name="y">Ряд в строке</param>
        /// <returns>Значение пикселя в указанных координатах</returns>
        /// <exception cref="IndexOutOfRangeException">Бросается, если x или y меньше 
        /// нуля, либо выходят за пределы количества строк и столбцов соответсвенно</exception>
        Color this[int x, int y] { get; set; }

        /// <summary>
        /// Конвертирут изображение в экземпляр класса <see cref="Bitmap"/>
        /// </summary>
        /// <returns>Экземпляр класса <see cref="Bitmap"/></returns>
        Bitmap ToBitmap();
    }

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

    /// <summary>
    /// Предоставляет функцию для трансформирования исходного изображения
    /// </summary>
    public interface IImageTransformation
    {
        /// <summary>
        /// Трансформирует указанное изображение и возвращает его.
        /// Изображение должно меняться на месте
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IImage Tranform(IImage input);
    }
}
