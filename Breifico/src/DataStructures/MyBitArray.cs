using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Breifico.IO;

namespace Breifico.DataStructures
{
    /// <summary>
    /// Имплементация битового массива
    /// </summary>
    public sealed class MyBitArray : ICollection, IEnumerable<bool>, IEquatable<MyBitArray>
    {
        /// <summary>
        /// Размер буфера по умолчанию
        /// </summary>
        private const int DEFAULT_BUFFER_SIZE = 4;

        private object _syncRoot;

        private readonly MyList<byte> _internalBuffer;

        /// <summary>
        /// Размер битового массива в битах
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Количество байт, которым представлен битовый массив
        /// </summary>
        public int ByteCount => this._internalBuffer.Count;

        /// <summary>
        /// Количестве неиспользуемых бит в последнем байте
        /// </summary>
        public int FreeBits => (8 - this.NextBitPosition) % 8;

        /// <summary>
        /// Индекс байта, в который будет записан следующий элемент
        /// </summary>
        private int ByteCursorPosition => this.Count / 8;

        /// <summary>
        /// Индекс бита, в который будет запсан следующий элемент
        /// </summary>
        private int NextBitPosition => this.Count % 8;

        /// <summary>
        /// Инициализирует новый битовый массив с размером по умолчанию
        /// </summary>
        public MyBitArray() : this(DEFAULT_BUFFER_SIZE) {}

        /// <summary>
        /// Инициализирует новый битовый массив с указанным размером
        /// </summary>
        /// <param name="initSizeInBytes">Начальный размер битового массива</param>
        public MyBitArray(int initSizeInBytes) {
            this._internalBuffer = new MyList<byte>(initSizeInBytes);
        }

        /// <summary>
        /// Инициализирует новый битовый массив указанными данными
        /// </summary>
        /// <param name="input">Массив байт, которым будет инициализирован битовый массив</param>
        /// <param name="freeBits">Количество неиспользуемых байт в последнем байте масива</param>
        public MyBitArray(byte[] input, int freeBits = 0) {
            if (freeBits > 7) {
                throw new ArgumentException("Количество неиспользуемых битов должено быть меньше 7");
            }
            var newList = new MyList<byte>();
            newList.AddRange(input);
            this._internalBuffer = newList;
            this.Count = input.Length * 8 - freeBits;
        }

        /// <summary>
        /// Добавляет бит в битовый массив
        /// True - единичный бит, False - нулевой
        /// </summary>
        /// <param name="bit">Добавляемый бит</param>
        public void Append(bool bit) {
            if (this._internalBuffer.Count <= this.ByteCursorPosition) {
                this._internalBuffer.Add(0x00);
            }
            if (bit) {
                byte op = (byte)(1 << (7 - this.NextBitPosition));
                this._internalBuffer[this.ByteCursorPosition] ^= op;
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
            if (this.ByteCursorPosition > 0) {
                for (int i = 0; i < this.ByteCursorPosition; i++) {
                    this._internalBuffer[i] = unchecked ((byte)~this._internalBuffer[i]);
                } 
            }
            int startIndex = this.ByteCursorPosition * 8;
            for (int i = startIndex; i < this.Count; i++) {
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
                    this._internalBuffer[byteIndex] |= unchecked((byte)mask);
                } else {
                    this._internalBuffer[byteIndex] &= unchecked((byte)~mask);
                }
            }
        }

        /// <summary>
        /// Возвращает байт начиная с бита, находящегося на указанной позиции
        /// </summary>
        /// <param name="position">Начальная позиция</param>
        /// <returns>Байт, начиная с указанного индекса</returns>
        public byte GetByteFromPosition(int position) {
            if (position + 8 > this.Count) {
                throw new IndexOutOfRangeException();
            }
            if (position % 8 == 0) {
                return this._internalBuffer[position / 8];
            }
            var ba = new MyBitArray();
            for (int i = 0; i < 8; i++) {
                ba.Append(this[position + i]);
            }
            return ba._internalBuffer[0];
        }

        /// <summary>
        /// Заменяет все биты на указанное значение
        /// </summary>
        /// <param name="value">Бит, которым будут заменены все значения</param>
        public void SetAll(bool value) {
            if (this.ByteCursorPosition > 0) {
                for (int i = 0; i < this.ByteCursorPosition; i++) {
                    this._internalBuffer[i] = value ? (byte)0xff : (byte)0x00;
                }
            }
            int startIndex = this.ByteCursorPosition * 8;
            for (int i = startIndex; i < this.Count; i++) {
                this[i] = value;
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
        /// Возвращает экземпляр <see cref="BitArrayReader"/>, предназначенный для чтения данных из битового массива
        /// </summary>
        /// <returns>Экземпляр класса <see cref="BitArrayReader"/></returns>
        public BitArrayReader GetReader() {
            return new BitArrayReader(this);
        }

        /// <summary>
        /// Сравнивает два битовых массивов
        /// </summary>
        /// <param name="other">Второй массив</param>
        /// <returns>True - если битовые массивы равны, иначе False</returns>
        public bool Equals(MyBitArray other) {
            if (other != null && this.Count != other.Count) {
                return false;
            }
            for (int i = 0; i < this._internalBuffer.Count; i++) {
                if (other != null && this._internalBuffer[i] != other._internalBuffer[i]) {
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
        public override int GetHashCode() 
            => this._internalBuffer.Aggregate(0, (current, t) => current ^ t);

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

        #region IEnumerable<T> implementation
        /// <summary>
        /// Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="IEnumerator{T}" />, который может использоваться
        /// для перебора коллекции
        /// </returns>
        public IEnumerator<bool> GetEnumerator() {
            for (int i = 0; i < this.Count; i++) {
                yield return this[i];
            }
        }

        /// <summary>
        /// Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="IEnumerator" />, который может использоваться для 
        /// перебора коллекции
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        #endregion

        #region ICollectiom implementation
        public void CopyTo(Array array, int index) {
            throw new NotImplementedException();
        }

        public object SyncRoot {
            get
            {
                if (this._syncRoot == null) {
                    Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
                }
                return this._syncRoot;
            }
        }

        public bool IsSynchronized { get; } = false;
        #endregion
    }
}