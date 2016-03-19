using System;
using System.Drawing;
using System.Xml.Schema;
using Breifico.Algorithms.Formats.BMP;

namespace Breifico
{
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

        public static int ToRange(this int number, int min, int max) {
            return number < min ? min : (number > max ? max : number);
        }
    }

    public static class SortExtensions
    {
        public static T[] MySort<T>(this T[] input, ISorter<T> sorter) where T : IComparable<T> {
            return sorter.Sort(input);
        }

        public static uint[] MySort(this uint[] input, ISorter<uint> sorter) {
            return sorter.Sort(input);
        }
    }


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