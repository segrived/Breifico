using System.Drawing;

namespace Breifico.Algorithms.ImageProcessing
{
    public class InvertImageTransformation : IImageTransformation
    {
        public IImage Tranform(IImage input) {
            return input.Transform((x, y, clr) => {
                return Color.FromArgb(~clr.R, ~clr.G, ~clr.B);
            });
        }
    }
}
