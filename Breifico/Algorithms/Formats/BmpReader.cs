using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Breifico.Algorithms.Formats
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BitmapFileHeader
    {
        public ushort Signature { get; set; }
        public uint FileSize { get; set; }
        public ushort Res1 { get; set; }
        public ushort Res2 { get; set; }
        public uint StartOffset { get; set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DibHeader
    {
        public uint HeaderSize { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public ushort ColorPlanes { get; set; }
        public ushort BitsPerPixel { get; set; }
        public uint CompressionMethod { get; set; }
        public uint Size { get; set; }
        public uint HorizontalRes { get; set; }
        public uint VertcalRes { get; set; }
        public uint ColorsInPalette { get; set; }
        public uint ImportantColors { get; set; }
    }

    public class BmpReader : IDisposable
    {
        private readonly Stream _internalStream;

        public BmpReader(string fileName) {
            this._internalStream = File.OpenRead(fileName);
        }

        public void Read() {
            using (var reader = new StreamBinaryReader(this._internalStream)) {
                // Чтение BMP заголовка
                var signature = reader.ReadUInt16();
                if (signature != 0x4d42) {
                    throw new InvalidBmpImageException($"Unknown BMP signature: 0x{signature:X4}");
                }
                var bitMapHeader = new BitmapFileHeader {
                    Signature = signature,
                    FileSize = reader.ReadUInt32(),
                    Res1 = reader.ReadUInt16(),
                    Res2 = reader.ReadUInt16(),
                    StartOffset = reader.ReadUInt32()
                };

                // Чтение DIB-заголовка (поддерживается только 40битный BITMAPINFOHEADER заголовок)
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
                if (dibHeader.CompressionMethod != 0) {
                    throw new InvalidBmpImageException("Compressed BMP images is not supported");
                }
            }
        }

        public void Dispose() {
            this._internalStream?.Dispose();
            ;
        }
    }
}