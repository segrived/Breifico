using System;
using System.IO;
using System.Runtime.InteropServices;
using Breifico.DataStructures;

namespace Breifico.IO
{
    /// <summary>
    /// Порядок считывания байт
    /// </summary>
    public enum Endianness
    {
        /// <summary>
        /// От младшего к старшему
        /// </summary>
        LittleEndian,

        /// <summary>
        /// От старшего к младшему
        /// </summary>
        BigEndian
    }

    /// <summary>
    /// Читает бинарные данные из потока
    /// </summary>
    public sealed class StreamBinaryReader : IDisposable
    {
        private readonly Endianness _endianness;

        public Stream InternalStream { get; }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="StreamBinaryReader" />
        /// </summary>
        /// <param name="internalStream">Исходный поток, из которого будут считываться данные</param>
        /// <param name="endianness">Порядок байт при считывании (BigEndian или LittleEndian)</param>
        public StreamBinaryReader(Stream internalStream, Endianness endianness = Endianness.LittleEndian) {
            if (!internalStream.CanRead) {
                throw new InvalidOperationException();
            }
            this.InternalStream = internalStream;
            this._endianness = endianness;
        }

        public StreamBinaryReader(string fileName)
            : this(File.OpenRead(fileName)) {}

        /// <summary>
        /// Читает указанное количество байт из потока в массив и возвращает его
        /// </summary>
        /// <param name="bytesCount">Количество считываемых байт</param>
        /// <returns>Байтовый массив со считанными данными</returns>
        /// <exception cref="IOException">Если количество считанных данных меньше, чем запрошено</exception>
        public byte[] ReadBytes(int bytesCount) {
            var b = new byte[bytesCount];
            int c = this.InternalStream.Read(b, 0, bytesCount);
            if (c != bytesCount) {
                throw new IOException();
            }
            return b;
        }

        /// <summary>
        /// Читает следующий байт из потока
        /// </summary>
        /// <returns>Значение типа <see cref="Byte" /></returns>
        public byte ReadByte() {
            int b = this.InternalStream.ReadByte();
            if (b == -1) {
                throw new IOException();
            }
            return (byte)b;
        }

        /// <summary>
        /// Читает следующее 16-битное целое число из потока
        /// </summary>
        /// <returns>Значение типа <see cref="Int16" /></returns>
        /// <exception cref="IOException">Если количество считанных данных меньше, чем запрошено</exception>
        public short ReadInt16() {
            byte[] b = this.ReadBytes(2);
            return this._endianness == Endianness.LittleEndian
                ? (short)(b[0] + (b[1] << 8))
                : (short)(b[1] + (b[0] << 8));
        }

        /// <summary>
        /// Читает следующее беззнаковое 16-битное целое число из потока
        /// </summary>
        /// <returns>Значение типа <see cref="UInt16" /></returns>
        /// <exception cref="IOException">Если количество считанных данных меньше, чем запрошено</exception>
        public ushort ReadUInt16() {
            byte[] b = this.ReadBytes(2);
            return this._endianness == Endianness.LittleEndian
                ? (ushort)(b[0] + (b[1] << 8))
                : (ushort)(b[1] + (b[0] << 8));
        }

        /// <summary>
        /// Читает следующее 32-битное целое число из потока
        /// </summary>
        /// <returns>Значение типа <see cref="Int32" /></returns>
        /// <exception cref="IOException">Если количество считанных данных меньше, чем запрошено</exception>
        public int ReadInt32() {
            byte[] b = this.ReadBytes(4);
            return this._endianness == Endianness.LittleEndian
                ? b[0] + (b[1] << 8) + (b[2] << 16) + (b[3] << 24)
                : b[3] + (b[2] << 8) + (b[1] << 16) + (b[0] << 24);
        }

        /// <summary>
        /// Читает следующее беззнаковое 32-битное целое число из потока
        /// </summary>
        /// <returns>Значение типа <see cref="UInt32" /></returns>
        /// <exception cref="IOException">Если количество считанных данных меньше, чем запрошено</exception>
        public uint ReadUInt32() {
            byte[] b = this.ReadBytes(4);
            return this._endianness == Endianness.LittleEndian
                ? (uint)(b[0] + (b[1] << 8) + (b[2] << 16) + (b[3] << 24))
                : (uint)(b[3] + (b[2] << 8) + (b[1] << 16) + (b[0] << 24));
        }

        /// <summary>
        /// Читает следующее 64-битное целое число из потока
        /// </summary>
        /// <returns>Значение типа <see cref="Int64" /></returns>
        /// <exception cref="IOException">Если количество считанных данных меньше, чем запрошено</exception>
        public long ReadInt64() {
            byte[] b = this.ReadBytes(8);
            return this._endianness == Endianness.LittleEndian
                ? b[0] + (b[1] << 8) + (b[2] << 16) + (b[3] << 24) + (b[4] << 32)
                  + (b[5] << 40) + (b[6] << 48) + (b[7] << 56)
                : b[7] + (b[6] << 8) + (b[5] << 16) + (b[4] << 24) + (b[3] << 32)
                  + (b[2] << 40) + (b[1] << 48) + (b[0] << 56);
        }

        /// <summary>
        /// Читает следующее беззнаковое 64-битное целое число из потока
        /// </summary>
        /// <returns>Значение типа <see cref="UInt64" /></returns>
        /// <exception cref="IOException">Если количество считанных данных меньше, чем запрошено</exception>
        public ulong ReadUInt64() {
            byte[] b = this.ReadBytes(8);
            return this._endianness == Endianness.LittleEndian
                ? (ulong)(b[0] + (b[1] << 8) + (b[2] << 16) + (b[3] << 24) + (b[4] << 32)
                          + (b[5] << 40) + (b[6] << 48) + (b[7] << 56))
                : (ulong)(b[7] + (b[6] << 8) + (b[5] << 16) + (b[4] << 24) + (b[3] << 32)
                          + (b[2] << 40) + (b[1] << 48) + (b[0] << 56));
        }

        public T ReadStruct<T>() where T : struct {
            byte[] bytes = this.ReadBytes(Marshal.SizeOf(typeof(T)));
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var structData = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return structData;
        }

        /// <summary>
        /// Читает следующий битовый массив из потока
        /// </summary>
        /// <returns>Значение типа <see cref="MyBitArray" /></returns>
        public MyBitArray ReadBitArray() {
            int length = this.ReadInt32();
            int freeBits = this.ReadByte();
            byte[] content = this.ReadBytes(length);
            return new MyBitArray(content, freeBits);
        }

        /// <summary>
        /// Освобождает ресурсы
        /// </summary>
        public void Dispose() {
            this.InternalStream?.Dispose();
        }
    }
}