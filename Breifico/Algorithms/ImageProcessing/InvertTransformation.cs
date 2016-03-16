using System.Drawing;

namespace Breifico.Algorithms.ImageProcessing
{
    public class InvertTransformation : IImageTransformation
    {
        public IImage Tranform(IImage input) {
            return input.Transform(clr => Color.FromArgb(255 - clr.R, 255 - clr.G, 255 - clr.B));
        }
    }
}
