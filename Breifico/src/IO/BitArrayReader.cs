using System.IO;
using Breifico.DataStructures;

namespace Breifico.IO
{
    /// <summary>
    /// Предназначен для чтения данных из битового массива
    /// </summary>
    public class BitArrayReader
    {
        private readonly MyBitArray _internalArray;
        private int _currentPosition = 0;

        /// <summary>
        /// Создает новый экземпляр <see cref="BitArrayReader"/> с указанным битовым массивом
        /// </summary>
        /// <param name="array"></param>
        public BitArrayReader(MyBitArray array) {
            this._internalArray = array;
        }

        /// <summary>
        /// Считывает следующий бит из битового массива
        /// </summary>
        /// <returns>Прочитанный бит (в виде булевого значения)</returns>
        /// <exception cref="EndOfStreamException">Если достигнут конец битового массива</exception>
        public bool ReadBit() {
            if (this._currentPosition >= this._internalArray.Count) {
                throw new EndOfStreamException();
            }
            bool res = this._internalArray[this._currentPosition];
            this._currentPosition += 1;
            return res;
        }

        /// <summary>
        /// Считывает следующий байт из битового массива
        /// </summary>
        /// <returns>Прочитанный байт</returns>
        /// <exception cref="EndOfStreamException">Если достигнут конец битового массива</exception>
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
