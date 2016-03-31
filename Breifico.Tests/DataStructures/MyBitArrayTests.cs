using System;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
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

        [TestMethod]
        public void Negative_ShouldApplyNotOperationToAllBits() {
            var bitArray = new MyBitArray();
            bitArray.Append(189); // 10111101
            bitArray.Should().Equal(true, false, true, true, true, true, false, true);
            bitArray.Negative();
            bitArray.Should().Equal(false, true, false, false, false, false, true, false);
            bitArray.Append(true);
            bitArray.Should().Equal(false, true, false, false, false, false, true, false, true);
            bitArray.Negative();
            bitArray.Should().Equal(true, false, true, true, true, true, false, true, false);

            var bitArray2 = new MyBitArray();
            bitArray2.Append(true, false);
            bitArray2.Negative();
            bitArray2.Should().Equal(false, true);
        }

        [TestMethod]
        public void SetAll_ShouldSetAllBitsToValue() {
            var bitArray = new MyBitArray();
            bitArray.Append(true, true, true, false, false, true);
            bitArray.SetAll(true);
            bitArray.Should().Equal(true, true, true, true, true, true);
            bitArray.SetAll(false);
            bitArray.Should().Equal(false, false, false, false, false, false);
            var bitArray2 = new MyBitArray();
            bitArray2.Append(true, false, true, false, true, false, true, false, true, false);
            bitArray2.SetAll(false);
            bitArray2.Should().Equal(false, false, false, false, false, false, false, false, false, false);
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements() {
            var bitArray = new MyBitArray();
            bitArray.Append(125);
            bitArray.Count.Should().Be(8);
            bitArray.Clear();
            bitArray.Count.Should().Be(0);
            bitArray.Should().BeEmpty();
        }
    }
}