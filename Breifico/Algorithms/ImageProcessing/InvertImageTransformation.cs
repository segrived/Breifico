using System.Drawing;

namespace Breifico.Algorithms.ImageProcessing
{
    public class InvertImageTransformation : IImageTransformation
    {
        public IImage Tranform(IImage input) {
            return input.Transform((x, y, clr) => {
                return Color.FromArgb(255 - clr.R, 255 - clr.G, 255 - clr.B);
            });
        }
    }
}
