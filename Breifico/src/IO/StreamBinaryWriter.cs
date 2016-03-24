using System;
using System.IO;
using Breifico.DataStructures;

namespace Breifico.IO
{
    public class StreamBinaryWriter : IDisposable
    {
        private readonly Stream _internalStream;

        public StreamBinaryWriter(string fileName) 
            : this(File.OpenWrite(fileName)) { }

        public StreamBinaryWriter(Stream stream) {
            if (!stream.CanWrite) {
                throw new InvalidOperationException();
            }
            this._internalStream = stream;
        }

        public void WriteByte(byte b) {
            this._internalStream.WriteByte(b);
        }

        public void WriteBitArray(MyBitArray bitArray) {
            this.WriteInt32(bitArray.ByteCount);
            this.WriteByte((byte)bitArray.FreeBits);
            this.WriteByteArtray(bitArray.ToByteArray());
        }

        public void WriteInt32(int number) {
            byte[] arr = {
                (byte)(number),
                (byte)(number >> 8),
                (byte)(number >> 16),
                (byte)(number >> 24),
            };
            this.WriteByteArtray(arr);
        }

        public void WriteByteArtray(byte[] input) {
            this._internalStream.Write(input, 0, input.Length);
        }

        public void Dispose() {
            this._internalStream?.Dispose();
        }
    }
}
