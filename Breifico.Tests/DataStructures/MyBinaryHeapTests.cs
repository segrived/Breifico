using System;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
    [TestClass]
    public class MyBinaryHeapTests
    {
        [TestMethod]
        public void Insert_WhenMaxHeap_ShouldInsertItems() {
            var heap = MyBinaryHeap<int>.CreateMaxHeap();
            heap.AddRange(new []{1, 7, 4, 9, 11, 8, 2});
            heap.Extract().Should().Be(11);
            heap.Extract().Should().Be(9);
            heap.Extract().Should().Be(8);
            heap.Extract().Should().Be(7);
            heap.Extract().Should().Be(4);
            heap.Extract().Should().Be(2);
            heap.Extract().Should().Be(1);
            heap.Invoking(h => h.Extract())
                .ShouldThrow<ArgumentOutOfRangeException>();

            heap.AddRange(new[] { 1, 2, 3, 4, 5, 6 });
            heap.Extract().Should().Be(6);
            heap.Extract().Should().Be(5);
            heap.Extract().Should().Be(4);
            heap.Extract().Should().Be(3);
            heap.Extract().Should().Be(2);
            heap.Extract().Should().Be(1);
            heap.Invoking(h => h.Extract())
                .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void GetEnumeratorTest() {
            var heap = MyBinaryHeap<int>.CreateMaxHeap();
            heap.AddRange(new[] { 5, 4, 9, 11, 2 });
            heap.Should().Equal(11, 9, 5, 4, 2);
        }

        [TestMethod]
        public void ClearTest() {
            var heap = MyBinaryHeap<int>.CreateMaxHeap();
            heap.AddRange(new[] { 5, 4, 9, 11, 2 });
            heap.Should().Equal(11, 9, 5, 4, 2);
            heap.Clear();
            heap.Should().BeEmpty();
            heap.Count.Should().Be(0);
        }
    }
}
