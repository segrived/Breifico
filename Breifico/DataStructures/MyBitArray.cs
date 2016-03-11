using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breifico.DataStructures
{
    public class MyBitArray : IEnumerable<bool>, IEquatable<MyBitArray>
    {
        /// <summary>
        /// Размер буфера по умолчанию
        /// </summary>
        private const int DefaultBufferSize = 4;

        private readonly MyList<byte> _internalBuffer;

        /// <summary>
        /// Размер битового массива в битах
        /// </summary>
        public int Count { get; private set; }

        public int FreeBits => (8 - this.NextBitPosition) % 8;

        private int BytePosition => this.Count / 8;
        private int NextBitPosition => this.Count % 8;

        /// <summary>
        /// Инициализирует новый битовый массив с размером по умолчанию
        /// </summary>
        public MyBitArray() : this(DefaultBufferSize) {}

        /// <summary>
        /// Инициализирует новый битовый массив с указанным размером
        /// </summary>
        /// <param name="initSizeInBytes">Начальный размер битового массива</param>
        public MyBitArray(int initSizeInBytes) {
            this._internalBuffer = new MyList<byte>(initSizeInBytes);
        }

        /// <summary>
        /// Добавляет бит в битовый массив
        /// True - единичный бит, False - нулевой
        /// </summary>
        /// <param name="bit">Добавляемый бит</param>
        public void Append(bool bit) {
            if (this._internalBuffer.Count <= this.BytePosition) {
                this._internalBuffer.Add(0x00);
            }
            if (bit) {
                byte op = (byte)(1 << (7 - this.NextBitPosition));
                this._internalBuffer[this.BytePosition] ^= op;
            }
            this.Count++;
        }

        /// <summary>
        /// Добавляет последовательно несколько битов в битовый массив
        /// </summary>
        /// <param name="b">Добавляемые биты</param>
        public void Append(params bool[] b) {
            for (int i = 0; i < b.Length; i++) {
                this.Append(b[i]);
            }
        }

        /// <summary>
        /// Добавляет байт в битовый массив
        /// </summary>
        /// <param name="b">Добавляемый бит</param>
        public void Append(byte b) {
            if (this.NextBitPosition == 0) {
                this._internalBuffer.Add(b);
                this.Count += 8;
                return;
            }
            for (int i = 7; i >= 0; i--) {
                this.Append((b & (1 << i)) != 0);
            }
        }

        /// <summary>
        /// Добавляет все элементы с другого битового массива
        /// </summary>
        /// <param name="other">Другой битовый массив</param>
        public void Append(MyBitArray other) {
            foreach (bool bit in other) {
                this.Append(bit);
            }
        }

        /// <summary>
        /// Инвертирует все биты в битовом массиве
        /// </summary>
        public void Negative() {
            if (this.BytePosition > 0) {
                for (int i = 0; i < this.BytePosition; i++) {
                    this._internalBuffer[i] = (byte)~this._internalBuffer[i];
                } 
            }
            for (int i = this.BytePosition * 8; i < this.Count; i++) {
                this[i] = !this[i];
            }
        }

        /// <summary>
        /// Получает доступ или устанавливает значение бита по индексу 
        /// в виде булевого значения
        /// </summary>
        /// <param name="position">Полизиция исходного бита</param>
        /// <returns>Бит по указанному индексу в виде булевого значения</returns>
        public bool this[int position]
        {
            get
            {
                if (position < 0 || position >= this.Count) {
                    throw new IndexOutOfRangeException();
                }
                int byteIndex = position / 8;
                int positionIndex = position % 8;
                byte b = this._internalBuffer[byteIndex];
                byte x = (byte)(1 << (7 - positionIndex));
                return (b & x) != 0;
            }
            set
            {
                if (position < 0 || position >= this.Count) {
                    throw new IndexOutOfRangeException();
                }
                int byteIndex = position / 8;
                int positionIndex = position % 8;
                int mask = 1 << (7 - positionIndex);
                if (value) {
                    this._internalBuffer[byteIndex] |= (byte)mask;
                } else {
                    this._internalBuffer[byteIndex] &= (byte)~mask;
                }
            }
        }

        /// <summary>
        /// Возвращает битовый массив в виде массива байт
        /// Все неплолные биты будут дополнены нулями
        /// </summary>
        /// <returns>Битовый массив в виде массива байт</returns>
        public byte[] ToByteArray() {
            return this._internalBuffer.ToArray();
        }

        /// <summary>
        /// Сбрасывает состояние байтового массива до начального состояния
        /// </summary>
        public void Clear() {
            this._internalBuffer.Clear();
            this.Count = 0;
        }

        /// <summary>
        /// Сравнивает два битовых массивов
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(MyBitArray other) {
            if (this.Count != other.Count) {
                return false;
            }
            for (int i = 0; i < this._internalBuffer.Count; i++) {
                if (this._internalBuffer[i] != other._internalBuffer[i]) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Переопределяет GetHashCode для битового массива
        /// Для получения хэшкода ксорит все байты
        /// </summary>
        /// <returns>Хэшкод битового массива</returns>
        public override int GetHashCode() {
            int hashCode = 0;
            for (int i = 0; i < this._internalBuffer.Count; i++) {
                hashCode ^= this._internalBuffer[i];
            }
            return hashCode;
        }

        /// <summary>
        /// Строковое представление
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            var sb = new StringBuilder();
            foreach (bool bit in this) {
                sb.Append(bit ? '1' : '0');
            }
            return sb.ToString();
        }

        public IEnumerator<bool> GetEnumerator() {
            for (int i = 0; i < this.Count; i++) {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }
}