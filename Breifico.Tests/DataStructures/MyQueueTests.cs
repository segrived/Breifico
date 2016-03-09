using System;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
    [TestClass]
    public class MyQueueTests
    {
        [TestMethod]
        public void NewInstance_ShouldBeEmpty() {
            new MyQueue<int>().Should().BeEmpty();
        }

        [TestMethod]
        public void Count_ShouldReflectCollectionChanges() {
            var queue = new MyQueue<int>();
            queue.Count.Should().Be(0);
            queue.Enqueue(1);
            queue.Count.Should().Be(1);
            queue.Enqueue(10);
            queue.Count.Should().Be(2);
            queue.Dequeue();
            queue.Count.Should().Be(1);
            queue.Peek();
            queue.Count.Should().Be(1);
            queue.Dequeue();
            queue.Count.Should().Be(0);
            queue.Enqueue(20);
            queue.Clear();
            queue.Count.Should().Be(0);
        }

        [TestMethod]
        public void IsEmpty_WhenCollectionEmpty_ShouldReturnTrue() {
            var queue = new MyQueue<int>();
            queue.IsEmpty.Should().BeTrue();
            queue.Enqueue(10);
            queue.Dequeue();
            queue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_WhenCollectionNotEmpty_ShouldReturnFalse() {
            var queue = new MyQueue<int>();
            queue.Enqueue(12);
            queue.IsEmpty.Should().BeFalse();
            queue.Enqueue(10);
            queue.IsEmpty.Should().BeFalse();
            queue.Dequeue();
            queue.IsEmpty.Should().BeFalse();
        }

        [TestMethod]
        public void Enqueue_ShouldAddNewElements() {
            var queue = new MyQueue<int>();
            queue.Should().BeEmpty();
            queue.Enqueue(12);
            queue.Should().Equal(12);
            queue.Enqueue(22);
            queue.Should().Equal(12, 22);
        }

        [TestMethod]
        public void Dequeue_WhenEmpty_ShouldThrowException() {
            var queue = new MyQueue<int>();
            queue.Invoking(q => q.Dequeue())
                 .ShouldThrow<InvalidOperationException>();

            queue.Enqueue(10);
            queue.Dequeue();
            queue.Invoking(q => q.Dequeue())
                 .ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Dequeue_WhenAnyElements_ShouldReturnItems() {
            var queue = new MyQueue<int>();
            queue.Enqueue(12);
            queue.Enqueue(15);
            queue.Should().Equal(12, 15);
            queue.Dequeue().Should().Be(12);
            queue.Should().Equal(15);
            queue.Dequeue().Should().Be(15);
            queue.Should().BeEmpty();
        }

        [TestMethod]
        public void Peek_WhenEmpty_ShouldThrowException() {
            var queue = new MyQueue<int>();
            queue.Invoking(q => q.Peek())
                 .ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Peek_Should_ReturnTopElement() {
            var queue = new MyQueue<int>();
            queue.Enqueue(10);
            queue.Enqueue(12);
            queue.Peek().Should().Be(10);
            queue.Peek().Should().Be(10);
            queue.Dequeue();
            queue.Peek().Should().Be(12);
            queue.Peek().Should().Be(12);
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements() {
            var queue = new MyQueue<int>();
            queue.Enqueue(10);
            queue.Enqueue(12);
            queue.Should().NotBeEmpty();
            queue.Clear();
            queue.Should().BeEmpty();
        }

        [TestMethod]
        public void SyncRoot_ShouldBeObject() {
            new MyQueue<int>().SyncRoot.Should().NotBeNull().And.BeOfType<object>();
        }

        [TestMethod]
        public void IsSynchronized_ShouldBeFalse() {
            new MyQueue<int>().IsSynchronized.Should().BeFalse();
        }
    }
}