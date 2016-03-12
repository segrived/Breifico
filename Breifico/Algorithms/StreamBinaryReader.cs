using System;
using System.IO;

namespace Breifico.Algorithms
{
    public class StreamBinaryReader : IDisposable
    {
        private readonly Stream _internalStream;
        private readonly bool _isLittleEndian;

        public StreamBinaryReader(Stream internalStream, bool isLittleEndian = true) {
            if (!internalStream.CanRead) {
                throw new InvalidOperationException();
            }
            this._internalStream = internalStream;
            this._isLittleEndian = isLittleEndian;
        }

        public byte ReadByte() {
            return (byte)this._internalStream.ReadByte();
        }

        public short ReadInt16() {
            var b = new byte[2];
            this._internalStream.Read(b, 0, 2);
            return this._isLittleEndian 
                ? (short)(b[0] + (b[1] << 8))
                : (short)(b[1] + (b[0] << 8));
        }

        public ushort ReadUInt16() {
            var b = new byte[2];
            this._internalStream.Read(b, 0, 2);
            return this._isLittleEndian
                ? (ushort)(b[0] + (b[1] << 8))
                : (ushort)(b[1] + (b[0] << 8));
        }

        public int ReadInt32() {
            var b = new byte[4];
            this._internalStream.Read(b, 0, 4);
            return this._isLittleEndian
                ? b[0] + (b[1] << 8) + (b[2] << 16) + (b[3] << 24)
                : b[3] + (b[2] << 8) + (b[1] << 16) + (b[0] << 24);
        }

        public uint ReadUInt32() {
            var b = new byte[4];
            this._internalStream.Read(b, 0, 4);
            return this._isLittleEndian
                ? (uint)(b[0] + (b[1] << 8) + (b[2] << 16) + (b[3] << 24))
                : (uint)(b[3] + (b[2] << 8) + (b[1] << 16) + (b[0] << 24));
        }

        public long ReadInt64() {
            var b = new byte[8];
            this._internalStream.Read(b, 0, 8);
            return this._isLittleEndian
                ? b[0] + (b[1] << 8) + (b[2] << 16) + (b[3] << 24) + (b[4] << 32) 
                + (b[5] << 40) + (b[6] << 48) + (b[7] << 56)
                : b[7] + (b[6] << 8) + (b[5] << 16) + (b[4] << 24) + (b[3] << 32) 
                + (b[2] << 40) + (b[1] << 48) + (b[0] << 56);
        }

        public void Dispose() {
            this._internalStream?.Dispose();
        }
    }
}
