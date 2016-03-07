using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.DataStructures
{
    public class MyBitArray : IEnumerable<bool>
    {
        private const int DefaultBufferSize = 4;

        private readonly MyList<byte> _internalBuffer;

        /// <summary>
        /// Размер битового массива в битах
        /// </summary>
        public int Count { get; private set; } = 0;

        private int BytePosition => this.Count / 8;
        private int BitPosition => this.Count % 8;

        public MyBitArray() : this(DefaultBufferSize) {}

        public MyBitArray(int initSizeInBytes) {
            this._internalBuffer = new MyList<byte>(initSizeInBytes);
        }

        /// <summary>
        /// Добавыляет бит в битовый массив
        /// True - единичный бит, False - нулевой
        /// </summary>
        /// <param name="bit">Добавляемый бит</param>
        public void Append(bool bit) {
            if (this._internalBuffer.Count <= this.BytePosition) {
                this._internalBuffer.Add(0x00);
            }
            if (bit) {
                byte op = (byte)(1 << (7 - this.BitPosition));
                this._internalBuffer[this.BytePosition] ^= op;
            }
            this.Count++;
        }

        /// <summary>
        /// Добавляет байт в битовый массив
        /// </summary>
        /// <param name="b">Добавляемый бит</param>
        public void Append(byte b) {
            if (this.BitPosition == 0) {
                this._internalBuffer.Add(b);
            } else {
                for (int i = 7; i >= 0; i--) {
                    this.Append((b & (1 << i)) != 0);
                }
            }
            this.Count += 8;
        }

        /// <summary>
        /// Добавляет несколько битов в битовый массив
        /// </summary>
        /// <param name="b">Добавляемые биты</param>
        public void Append(params bool[] b) {
            for (int i = 0; i < b.Length; i++) {
                this.Append(b[i]);
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

    [TestClass]
    public class MyBitArrayTests
    {
        [TestMethod]
        public void NewInstance_ShouldBeEmpty() {
            var bitArray = new MyBitArray();
            bitArray.Should().BeEmpty();
        }

        [TestMethod]
        public void Count_ShouldReflectCollectionChanges() {
            var bitArray = new MyBitArray();
            bitArray.Count.Should().Be(0);
            bitArray.Append(true);
            bitArray.Count.Should().Be(1);
            bitArray.Append(true, false);
            bitArray.Count.Should().Be(3);
            bitArray.Append(0x00);
            bitArray.Count.Should().Be(11);
        }

        [TestMethod]
        public void Append_ShouldAddBitsToBitArray() {
            var bitArray = new MyBitArray();
            // append single bit
            bitArray.Append(true);
            bitArray.Should().Equal(true);
            // append multiple bits
            bitArray.Append(false, false, true);
            bitArray.Should().Equal(true, false, false, true);
            // append byte
            bitArray.Append(0xff);
            bitArray.ToByteArray().Should().Equal(159, 240);
            bitArray.Should().Equal(true, false, false, true,
                                    true, true, true, true, true, true, true, true);
            bitArray.Append(false);
            bitArray.Should().Equal(true, false, false, true,
                                    true, true, true, true, true, true, true, true, false);
        }

        [TestMethod]
        public void IndexerGet_WhenEmpty_ShouldThrowException() {
            var bitArray = new MyBitArray();
            bitArray.Invoking(b => b[0].DoNothing())
                    .ShouldThrow<IndexOutOfRangeException>();
            bitArray.Invoking(b => b[-1].DoNothing())
                    .ShouldThrow<IndexOutOfRangeException>();
            bitArray.Invoking(b => b[1].DoNothing())
                    .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void IndexerGet_WhenInvalidIndex_ShouldThrowException() {
            var bitArray = new MyBitArray();
            bitArray.Append(true, false, false);
            bitArray.Invoking(b => b[-1].DoNothing())
                    .ShouldThrow<IndexOutOfRangeException>();
            bitArray.Invoking(b => b[3].DoNothing())
                    .ShouldThrow<IndexOutOfRangeException>();
            bitArray.Invoking(b => b[99].DoNothing())
                    .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void IndexerGet_ShouldReturnElementByIndex() {
            var bitArray = new MyBitArray();
            bitArray.Append(true, false, false);
            bitArray[0].Should().BeTrue();
            bitArray[1].Should().BeFalse();
            bitArray[2].Should().BeFalse();
            bitArray.Append(129);
            bitArray[3].Should().Be(true);
            bitArray[10].Should().Be(true);
        }

        [TestMethod]
        public void IndexerSet_WhenInvalidIndex_ShouldThrowException() {
            var bitArray = new MyBitArray();
            bitArray.Append(true, false, false);
            bitArray.Invoking(b => b[-1] = true)
                    .ShouldThrow<IndexOutOfRangeException>();
            bitArray.Invoking(b => b[3] = true)
                    .ShouldThrow<IndexOutOfRangeException>();
            bitArray.Invoking(b => b[99] = true)
                    .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void IndexerSet_ShouldReturnElementByIndex() {
            var bitArray = new MyBitArray();
            bitArray.Append(true, false, false);
            bitArray[0].Should().BeTrue();
            bitArray[1].Should().BeFalse();
            bitArray[2].Should().BeFalse();
            bitArray[0] = false;
            bitArray[0].Should().BeFalse();
            bitArray[0] = true;
            bitArray[0].Should().BeTrue();
            bitArray.Should().Equal(true, false, false);
            bitArray.Append(129);
            bitArray[3] = false;
            bitArray[3].Should().BeFalse();
            bitArray[10] = false;
            bitArray[10].Should().BeFalse();
            bitArray.Should().Equal(true, false, false, false, false,
                                    false, false, false, false, false, false);
        }
    }
}