using System;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
    [TestClass]
    public class MyPriorityQueueTests
    {
        [TestMethod]
        public void Enqueue_ShouldAppendElements() {
            var queue = new MyPriorityQueue<int>();
            queue.Count.Should().Be(0);
            queue.Enqueue(10, 7);
            queue.Should().BeEquivalentTo(10);
            queue.Enqueue(20, 8);
            queue.Should().BeEquivalentTo(20, 10);
            queue.Enqueue(30, 4);
            queue.Should().BeEquivalentTo(20, 10, 30);
            queue.Enqueue(40, 6);
            queue.Should().BeEquivalentTo(20, 10, 40, 30);
            queue.Count.Should().Be(4);
        }

        [TestMethod]
        public void Enqueue_WhenAnyElements_ShouldExtractElements() {
            var queue = new MyPriorityQueue<int>();
            queue.Enqueue(10, 7);
            queue.Enqueue(20, 4);
            queue.Enqueue(30, 8);

            queue.Dequeue().Should().Be(30);
            queue.Dequeue().Should().Be(10);
            queue.Dequeue().Should().Be(20);
        }

        [TestMethod]
        public void Enqueue_WhenEmpty_ShouldThrowException() {
            var queue = new MyPriorityQueue<int>();
            queue.Invoking(q => q.Dequeue()).ShouldThrow<ArgumentOutOfRangeException>();

            var queue2 = new MyPriorityQueue<int>();
            queue2.Enqueue(12, 1);
            queue2.Dequeue();
            queue2.Invoking(q => q.Dequeue()).ShouldThrow<ArgumentOutOfRangeException>();
        }
    }
}
