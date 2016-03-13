using System;
using System.Drawing;
using Breifico.Algorithms.Formats;

namespace Breifico
{
    public static class CommonHelpers
    {
        public static void Swap<T>(ref T lhs, ref T rhs) {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
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