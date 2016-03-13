using System;
using System.Drawing;

namespace Breifico.Algorithms.ImageProcessing
{
    public static class ImageTransformationExtenstions
    {
        public static IImage Transform(this IImage image, Func<int, int, Color, Color> f) {
            var 
            for (int i = 0; i < image.Height; i++) {
                for (int j = 0; j < image.Width; j++) {
                    image[i, j] = f(i, j, image[i, j]);
                }
            }
            return image;
        } 
    }
}
