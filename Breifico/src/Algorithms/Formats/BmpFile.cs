using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using Breifico.IO;

namespace Breifico.Algorithms.Formats
{
    /// <summary>
    /// Заголовок BMP-файла
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct BitmapFileHeader
    {
        /// <summary>
        /// Сингатура BMP-файла
        /// </summary>
        [FieldOffset(0)]
        public readonly ushort Signature;

        /// <summary>
        /// Полный размер BMP-файла в байтах
        /// </summary>
        [FieldOffset(2)]
        public readonly uint FileSize;

        /// <summary>
        /// Зарезевированное значение
        /// </summary>
        [FieldOffset(6)]
        public readonly ushort Res1;

        /// <summary>
        /// Зарезевированное значение
        /// </summary>
        [FieldOffset(8)]
        public readonly ushort Res2;

        /// <summary>
        /// Оффсет, с которого начинается последовательность пикселей
        /// </summary>
        [FieldOffset(10)]
        public readonly uint StartOffset;
    }

    /// <summary>
    /// DIB-заголовок (BITMAPINFOHEADER)
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct DibHeader
    {
        /// <summary>
        /// Размер этого заголовка
        /// </summary>
        [FieldOffset(0)]
        public readonly uint HeaderSize;

        /// <summary>
        /// Ширина картинки
        /// </summary>
        [FieldOffset(4)]
        public readonly uint Width;

        /// <summary>
        /// Высотка картинки
        /// </summary>
        [FieldOffset(8)]
        public readonly uint Height;

        /// <summary>
        /// Количество цветовых плоскостей
        /// </summary>
        [FieldOffset(12)]
        public readonly ushort ColorPlanes;

        /// <summary>
        /// Бит на пиксель
        /// </summary>
        [FieldOffset(14)]
        public readonly ushort BitsPerPixel;

        /// <summary>
        /// Метод сжатия BMP-файла
        /// </summary>
        [FieldOffset(16)]
        public readonly uint CompressionMethod;

        /// <summary>
        /// Размер изображения
        /// </summary>
        [FieldOffset(20)]
        public readonly uint Size;

        /// <summary>
        /// Горизонтальное разрешение
        /// </summary>
        [FieldOffset(24)]
        public readonly uint HorizontalRes;

        /// <summary>
        /// Вертикальное разрешение
        /// </summary>
        [FieldOffset(28)]
        public readonly uint VertcalRes;

        /// <summary>
        /// Количество цветов в палитре
        /// </summary>
        [FieldOffset(32)]
        public readonly uint ColorsInPalette;

        /// <summary>
        /// Количество "важных" цветов
        /// </summary>
        [FieldOffset(36)]
        public readonly uint ImportantColors;
    }

    /// <summary>
    /// Представляет структуру BMP-файла
    /// </summary>
    public sealed class BmpFile : IImage
    {
        public BmpFile(string fileName)
        {
            this.Read(File.OpenRead(fileName));
        }

        /// <summary>
        /// Создает новое изображение указанной размерности
        /// </summary>
        /// <param name="width">Ширина изображения</param>
        /// <param name="height">Высота изображения</param>
        public BmpFile(int width, int height)
        {
            this.Height = height;
            this.Width = width;
            this.ImageData = new Color[width, height];
        }

        /// <summary>
        /// Цвета всех пикселей в изображении
        /// </summary>
        private Color[,] ImageData { get; set; }

        /// <summary>
        /// Ширина картинки
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Высота картинки
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Количество бит на пиксель (24 или 32)
        /// </summary>
        public int BitsPerPixel { get; private set; }

        /// <summary>
        /// Получает или устанавливает значение указанного пикселя
        /// </summary>
        /// <param name="x">Строка в изображении</param>
        /// <param name="y">Ряд в строке</param>
        /// <returns>Значение пикселя в указанных координатах</returns>
        /// <exception cref="IndexOutOfRangeException">Бросается, если x или y меньше 
        /// нуля, либо выходят за пределы количества строк и столбцов соответсвенно</exception>
        public Color this[int x, int y]
        {
            get
            {
                if (x < 0 || x > this.Width || y < 0 || y > this.Height)
                    throw new IndexOutOfRangeException();
                return this.ImageData[x, y];
            }
            set
            {
                if (x < 0 || x > this.Width || y < 0 || y > this.Height)
                    throw new IndexOutOfRangeException();
                this.ImageData[x, y] = value;
            }
        }

        /// <summary>
        /// Читает изображение из потока
        /// </summary>
        /// <param name="s">Исходный поток, из которого читается BMP-изображение</param>
        private void Read(Stream s)
        {
            using (var reader = new StreamBinaryReader(s))
            {
                // Чтение BMP заголовка
                var bitmapHeader = reader.ReadStruct<BitmapFileHeader>();

                if (bitmapHeader.Signature != 0x4D42)
                    throw new InvalidBmpImageException($"Unknown BMP signature (0x{bitmapHeader.Signature:X4})");

                // Чтение DIB-заголовка (поддерживается только 40-байтный BITMAPINFOHEADER заголовок)
                var dibHeader = reader.ReadStruct<DibHeader>();

                if (dibHeader.HeaderSize != 40)
                    throw new InvalidBmpImageException("Only BITMAPINFOHEADER header is supported");

                // пока поддерживаются только 24- и 32-битные BMP
                if (dibHeader.BitsPerPixel != 24 && dibHeader.BitsPerPixel != 32)
                    throw new InvalidBmpImageException("Only 24bit/pixel BMP images is supported");

                if (dibHeader.CompressionMethod != 0)
                    throw new InvalidBmpImageException("Compressed BMP images is not supported");

                this.BitsPerPixel = dibHeader.BitsPerPixel;
                this.Width = (int)dibHeader.Width;
                this.Height = (int)dibHeader.Height;

                this.ImageData = new Color[(int)dibHeader.Width, (int)dibHeader.Height];

                // перемещаемся к оффсету, с которого начинаются пиксели
                reader.InternalStream.Seek(bitmapHeader.StartOffset, SeekOrigin.Begin);

                switch (this.BitsPerPixel)
                {
                    case 24:
                        this.Read24BitPixelData(reader);
                        break;
                    case 32:
                        this.Read32BitPixelData(reader);
                        break;
                }
            }
        }

        private void Read24BitPixelData(StreamBinaryReader reader)
        {
            for (int i = this.Height - 1; i >= 0; i--)
            {
                int imageBytes = (this.Width * 3 + 3) & ~0x03;
                var b = reader.ReadBytes(imageBytes);
                for (int j = 0; j < this.Width; j++)
                {
                    byte bComp = b[j * 3];
                    byte gComp = b[j * 3 + 1];
                    byte rComp = b[j * 3 + 2];
                    this.ImageData[j, i] = Color.FromArgb(rComp, gComp, bComp);
                }
            }
        }

        private void Read32BitPixelData(StreamBinaryReader reader)
        {
            for (int i = this.Height - 1; i >= 0; i--)
            {
                // выравнивание в 4 байта не нужно
                var b = reader.ReadBytes(this.Width * 4);
                for (int j = 0; j < this.Width; j++)
                {
                    byte bComp = b[j * 4];
                    byte gComp = b[j * 4 + 1];
                    byte rComp = b[j * 4 + 2];
                    byte aComp = b[j * 4 + 3];
                    this.ImageData[j, i] = Color.FromArgb(aComp, rComp, gComp, bComp);
                }
            }
        }

        /// <summary>
        /// Конвертирует изображение в экземпляр класса <see cref="Bitmap"/> 
        /// </summary>
        /// <returns>Экземпляр класса <see cref="Bitmap"/>, представляющее данное изображение</returns>
        public Bitmap ToBitmap()
        {
            // TODO: использовать LockBits
            var bitmap = new Bitmap(this.Width, this.Height);
            for (int i = 0; i < this.Width; i++)
                for (int j = 0; j < this.Height; j++)
                    bitmap.SetPixel(i, j, this.ImageData[i, j]);
            return bitmap;
        }
    }
}