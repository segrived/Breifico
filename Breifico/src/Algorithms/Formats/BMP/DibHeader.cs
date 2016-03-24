using System.Runtime.InteropServices;

namespace Breifico.Algorithms.Formats.BMP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DibHeader
    {
        /// <summary>
        /// ������ ����� ���������
        /// </summary>
        public uint HeaderSize { get; set; }

        /// <summary>
        /// ������ ��������
        /// </summary>
        public uint Width { get; set; }

        /// <summary>
        /// ������� ��������
        /// </summary>
        public uint Height { get; set; }
        public ushort ColorPlanes { get; set; }
        public ushort BitsPerPixel { get; set; }

        /// <summary>
        /// ����� ������ BMP-�����
        /// </summary>
        public uint CompressionMethod { get; set; }
        public uint Size { get; set; }
        public uint HorizontalRes { get; set; }
        public uint VertcalRes { get; set; }
        public uint ColorsInPalette { get; set; }
        public uint ImportantColors { get; set; }
    }
}