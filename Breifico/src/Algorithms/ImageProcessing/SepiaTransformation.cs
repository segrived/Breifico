﻿using System.Drawing;

namespace Breifico.Algorithms.ImageProcessing
{
    /// <summary>
    /// Применение эффекта "Сепия" к изображению
    /// </summary>
    public sealed class SepiaTransformation : IImageTransformation
    {
        public IImage Tranform(IImage input)
        {
            Color ColorToSepia(Color p)
            {
                byte newR = (byte) ((int) (p.R * 0.393 + p.G * 0.769 + p.B * 0.189)).ToRange(0, 255);
                byte newG = (byte) ((int) (p.R * 0.349 + p.G * 0.686 + p.B * 0.168)).ToRange(0, 255);
                byte newB = (byte) ((int) (p.R * 0.272 + p.G * 0.534 + p.B * 0.131)).ToRange(0, 255);
                return Color.FromArgb(newR, newG, newB);
            }
            return input.Transform(ColorToSepia);
        }
    }
}
