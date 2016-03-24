using System.IO;
using Breifico.DataStructures;

namespace Breifico.IO
{
    public class BitArrayReader
    {
        public bool IsEndOfStream 
            => this._currentPosition == this._internalArray.Count - 1;

        private readonly MyBitArray _internalArray;
        private int _currentPosition = 0;

        public BitArrayReader(MyBitArray array) {
            this._internalArray = array;
        }

        public bool ReadBit() {
            if (this._currentPosition >= this._internalArray.Count) {
                throw new EndOfStreamException();
            }
            bool res = this._internalArray[this._currentPosition];
            this._currentPosition += 1;
            return res;
        }

        public byte ReadByte() {
            if (this._currentPosition + 8 > this._internalArray.Count) {
                throw new EndOfStreamException();
            }
            byte res = this._internalArray.GetByteFromPosition(this._currentPosition);
            this._currentPosition += 8;
            return res;
        }
    }
}
