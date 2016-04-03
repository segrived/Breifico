﻿using System.Drawing;

namespace Breifico.Algorithms.ImageProcessing
{
    /// <summary>
    /// Инвертирование изображения
    /// </summary>
    public sealed class InvertTransformation : IImageTransformation
    {
        public IImage Tranform(IImage input) {
            return input.Transform(clr => Color.FromArgb(255 - clr.R, 255 - clr.G, 255 - clr.B));
        }
    }
}
