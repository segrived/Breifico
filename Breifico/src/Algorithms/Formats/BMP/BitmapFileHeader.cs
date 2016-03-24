using System.Runtime.InteropServices;

namespace Breifico.Algorithms.Formats.BMP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BitmapFileHeader
    {
        /// <summary>
        /// Сингатура BMP-файла
        /// </summary>
        public ushort Signature { get; set; }

        /// <summary>
        /// Полный размер BMP-файла в байтах
        /// </summary>
        public uint FileSize { get; set; }

        /// <summary>
        /// Зарезевированное значение
        /// </summary>
        public ushort Res1 { get; set; }

        /// <summary>
        /// Зарезевированное значение
        /// </summary>
        public ushort Res2 { get; set; }

        /// <summary>
        /// Оффсет, с которого начинается последовательность пикселей
        /// </summary>
        public uint StartOffset { get; set; }
    }
}