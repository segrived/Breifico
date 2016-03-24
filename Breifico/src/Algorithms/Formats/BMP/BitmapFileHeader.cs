using System.Runtime.InteropServices;

namespace Breifico.Algorithms.Formats.BMP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BitmapFileHeader
    {
        /// <summary>
        /// ��������� BMP-�����
        /// </summary>
        public ushort Signature { get; set; }

        /// <summary>
        /// ������ ������ BMP-����� � ������
        /// </summary>
        public uint FileSize { get; set; }

        /// <summary>
        /// ���������������� ��������
        /// </summary>
        public ushort Res1 { get; set; }

        /// <summary>
        /// ���������������� ��������
        /// </summary>
        public ushort Res2 { get; set; }

        /// <summary>
        /// ������, � �������� ���������� ������������������ ��������
        /// </summary>
        public uint StartOffset { get; set; }
    }
}