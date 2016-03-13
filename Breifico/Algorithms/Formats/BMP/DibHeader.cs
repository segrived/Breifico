using System.Runtime.InteropServices;

namespace Breifico.Algorithms.Formats.BMP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DibHeader
    {
        /// <summary>
        /// Размер этого заголовка
        /// </summary>
        public uint HeaderSize { get; set; }

        /// <summary>
        /// Ширина картинки
        /// </summary>
        public uint Width { get; set; }

        /// <summary>
        /// Высотка картинки
        /// </summary>
        public uint Height { get; set; }
        public ushort ColorPlanes { get; set; }
        public ushort BitsPerPixel { get; set; }

        /// <summary>
        /// Метод сжатия BMP-файла
        /// </summary>
        public uint CompressionMethod { get; set; }
        public uint Size { get; set; }
        public uint HorizontalRes { get; set; }
        public uint VertcalRes { get; set; }
        public uint ColorsInPalette { get; set; }
        public uint ImportantColors { get; set; }
    }
}