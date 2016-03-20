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
        public void Insert_Test() {
            var heap = MyBinaryHeap<int>.DefaultHeap();
            heap.AddRange(new []{1,7,15,4,16,12,11,4,31,22,14,11,11,15,18,22});
            heap.Extract().Should().Be(31);
            heap.Extract().Should().Be(22);
            heap.Extract().Should().Be(22);
            heap.Extract().Should().Be(18);
            heap.Extract().Should().Be(16);
            heap.Extract().Should().Be(15);
            heap.Extract().Should().Be(15);
            heap.Extract().Should().Be(14);
            heap.Extract().Should().Be(12);
            heap.Extract().Should().Be(11);
            heap.Extract().Should().Be(11);
            heap.Extract().Should().Be(11);
            heap.Extract().Should().Be(7);
            heap.Extract().Should().Be(4);
            heap.Extract().Should().Be(4);
            heap.Extract().Should().Be(1);
            heap.Invoking(h => h.Extract()).ShouldThrow<IndexOutOfRangeException>();
        }
    }
}
