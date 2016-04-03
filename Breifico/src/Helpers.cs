using System;
using System.Drawing;
using Breifico.Algorithms.Formats;

namespace Breifico
{
    /// <summary>
    /// Общие расширения
    /// </summary>
    public static class CommonHelpers
    {
        /// <summary>
        /// Меняет значения двух переменных местами
        /// </summary>
        /// <typeparam name="T">Тип переменных</typeparam>
        /// <param name="lhs">Первая переменная</param>
        /// <param name="rhs">Вторая переменная</param>
        public static void Swap<T>(ref T lhs, ref T rhs) {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        /// <summary>
        /// Укладывает переданное число в указанные рамки
        /// </summary>
        /// <param name="number">Укладываемое в рамки число</param>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <returns>Число в границах от<see cref="min"/> и до <see cref="max"/></returns>
        public static int ToRange(this int number, int min, int max) {
            return number < min ? min : (number > max ? max : number);
        }
    }

    /// <summary>
    /// Расширение функций сортировки
    /// </summary>
    public static class SortExtensions
    {
        /// <summary>
        /// Сортирует массив (in-place), используя указанный сортировщик (<see cref="ISorter{T}"/>)
        /// </summary>
        /// <typeparam name="T">Тип элементов в массиве</typeparam>
        /// <param name="input">сходный массив</param>
        /// <param name="sorter">Сортировщик</param>
        /// <returns>Отсортированная коллекция</returns>
        public static T[] MySort<T>(this T[] input, ISorter<T> sorter) where T : IComparable<T> {
            return sorter.Sort(input);
        }

        public static uint[] MySort(this uint[] input, ISorter<uint> sorter) {
            return sorter.Sort(input);
        }
    }

    /// <summary>
    /// Расширения для работы с изображениями
    /// </summary>
    public static class ImageExtenstions
    {
        /// <summary>
        /// Трансформирует изображение, применяя указанную функцию к каждому пикселю
        /// </summary>
        /// <param name="image">Исходное изображение</param>
        /// <param name="f">Функция, принимающая цвет пикселя и возвращающая новый цвет</param>
        /// <returns>Новый цвет пикселя</returns>
        public static IImage Transform(this IImage image, Func<Color, Color> f) {
            var bitmap = new BmpFile(image.Width, image.Height);
            for (int i = 0; i < image.Width; i++) {
                for (int j = 0; j < image.Height; j++) {
                    bitmap[i, j] = f(image[i, j]);
                }
            }
            return bitmap;
        }

        /// <summary>
        /// Трансформирует изображение, применяя указанную функцию к каждому пикселю
        /// </summary>
        /// <param name="image">Исходное изображение</param>
        /// <param name="f">Функция, принимающая цвет пикселя, X и Y координату
        ///  и возвращающая новый цвет</param>
        /// <returns>Новый цвет пикселя</returns>
        public static IImage Transform(this IImage image, Func<int, int, Color, Color> f) {
            var bitmap = new BmpFile(image.Width, image.Height);
            for (int i = 0; i < image.Width; i++) {
                for (int j = 0; j < image.Height; j++) {
                    bitmap[i, j] = f(i, j, image[i, j]);
                }
            }
            return bitmap;
        }
    }
}