using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Breifico.Algorithms.Formats
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

    public class BmpFile : IImage
    {
        public BmpFile(string fileName) {
            this.Read(File.OpenRead(fileName));
        }

        /// <summary>
        /// Создает новое изображение указанной размерности
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public BmpFile(int width, int height) {
            this.Width = width;
            this.Height = height;
            this.ImageData = new Color[height, width];
        }

        /// <summary>
        /// Цвета всех пикселей в изображении
        /// </summary>
        public Color[,] ImageData { get; private set; }

        /// <summary>
        /// Ширина картинки
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Высота картинки
        /// </summary>
        public int Height { get; private set; }

        public Color this[int x, int y]
        {
            get
            {
                if (x < 0 || x > this.Width || y < 0 || y > this.Height) {
                    throw new IndexOutOfRangeException();
                }
                return this.ImageData[x, y];
            }
            set
            {
                if (x < 0 || x > this.Width || y < 0 || y > this.Height) {
                    throw new IndexOutOfRangeException();
                }
                this.ImageData[x, y] = value;
            }
        }

        public void Read(Stream s) {
            using (var reader = new StreamBinaryReader(s)) {
                // Чтение BMP заголовка
                ushort signature = reader.ReadUInt16();
                if (signature != 0x4D42) {
                    throw new InvalidBmpImageException($"Unknown BMP signature (0x{signature:X4})");
                }
                var bitMapHeader = new BitmapFileHeader {
                    Signature = signature,
                    FileSize = reader.ReadUInt32(),
                    Res1 = reader.ReadUInt16(),
                    Res2 = reader.ReadUInt16(),
                    StartOffset = reader.ReadUInt32()
                };

                // Чтение DIB-заголовка (поддерживается только 40-байтный BITMAPINFOHEADER заголовок)
                uint headerSize = reader.ReadUInt32();
                if (headerSize != 40) {
                    throw new InvalidBmpImageException("Only BITMAPINFOHEADER header is supported");
                }
                var dibHeader = new DibHeader {
                    HeaderSize = headerSize,
                    Width = reader.ReadUInt32(),
                    Height = reader.ReadUInt32(),
                    ColorPlanes = reader.ReadUInt16(),
                    BitsPerPixel = reader.ReadUInt16(),
                    CompressionMethod = reader.ReadUInt32(),
                    Size = reader.ReadUInt32(),
                    HorizontalRes = reader.ReadUInt32(),
                    VertcalRes = reader.ReadUInt32(),
                    ColorsInPalette = reader.ReadUInt32(),
                    ImportantColors = reader.ReadUInt32()
                };

                if (dibHeader.BitsPerPixel != 24) {
                    throw new InvalidBmpImageException("Only 24bit/pixel BMP images is supported");
                }

                if (dibHeader.CompressionMethod != 0) {
                    throw new InvalidBmpImageException("Compressed BMP images is not supported");
                }

                this.Width = (int)dibHeader.Width;
                this.Height = (int)dibHeader.Height;

                this.ImageData = new Color[(int)dibHeader.Height, (int)dibHeader.Width];

                // перемещаемся к оффсету, с которого начинаются пиксели
                reader.InternalStream.Seek(bitMapHeader.StartOffset, SeekOrigin.Begin);

                for (int i = (int)(dibHeader.Height - 1); i >= 0; i--) {
                    int imageBytes = (int)((dibHeader.Width * 3 + 3) & ~0x03);
                    byte[] b = reader.ReadBytes(imageBytes);
                    for (int j = 0; j < dibHeader.Width; j++) {
                        byte bComp = b[j * 3];
                        byte gComp = b[j * 3 + 1];
                        byte rComp = b[j * 3 + 2];
                        this.ImageData[i, j] = Color.FromArgb(rComp, gComp, bComp);
                    }
                }
            }
        }

        /// <summary>
        /// Конвертирует изображение в экземпляр класса <see cref="Bitmap"/> 
        /// </summary>
        /// <returns></returns>
        public Bitmap ToBitmap() {
            var bitmap = new Bitmap(this.Width, this.Height);
            for (int i = 0; i < this.Width; i++) {
                for (int j = 0; j < this.Height; j++) {
                    bitmap.SetPixel(i, j, this.ImageData[j, i]);
                }
            }
            return bitmap;
        }
    }
}