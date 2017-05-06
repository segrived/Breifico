using System.Drawing;

namespace Breifico.Algorithms.ImageProcessing
{
    /// <summary>
    /// Инвертирование изображения
    /// </summary>
    public sealed class InvertTransformation : IImageTransformation
    {
        /// <summary>
        /// Инвертирует исходное изображение
        /// </summary>
        /// <param name="input">Исходное изображение</param>
        /// <returns>Инвертированное изображение</returns>
        public IImage Tranform(IImage input) 
            => input.Transform(clr => Color.FromArgb(255 - clr.R, 255 - clr.G, 255 - clr.B));
    }
}
