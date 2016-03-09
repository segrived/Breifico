using System;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
    [TestClass]
    public class MyStackTests
    {
        [TestMethod]
        public void NewInstance_ShouldBeEmpty() {
            new MyStack<int>().Should().BeEmpty();
        }

        [TestMethod]
        public void Count_ShouldReflectCollectionChanges() {
            var stack = new MyStack<int>();
            stack.Count.Should().Be(0);
            stack.Push(10);
            stack.Count.Should().Be(1);
            stack.Push(12);
            stack.Count.Should().Be(2);
            stack.Peek();
            stack.Count.Should().Be(2);
            stack.Pop();
            stack.Count.Should().Be(1);
            stack.Pop();
            stack.Count.Should().Be(0);
            stack.Push(10);
            stack.Clear();
            stack.Should().BeEmpty();
        }

        [TestMethod]
        public void IsEmpty_WhenCollectionEmpty_ShouldReturnTrue() {
            var queue = new MyStack<int>();
            queue.IsEmpty.Should().BeTrue();
            queue.Push(10);
            queue.Pop();
            queue.IsEmpty.Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_WhenCollectionNotEmpty_ShouldReturnFalse() {
            var stack = new MyStack<int>();
            stack.Push(12);
            stack.IsEmpty.Should().BeFalse();
            stack.Push(10);
            stack.IsEmpty.Should().BeFalse();
            stack.Pop();
            stack.IsEmpty.Should().BeFalse();
        }

        [TestMethod]
        public void Push_ShouldAddNewElements() {
            var stack = new MyStack<int>();
            stack.Push(12);
            stack.Should().Equal(12);
            stack.Push(10);
            stack.Should().Equal(10, 12);
            stack.Push(8);
            stack.Should().Equal(8, 10, 12);
        }

        [TestMethod]
        public void Pop_WhenEmpty_ShouldThrowException() {
            var stack = new MyStack<int>();
            stack.Invoking(s => s.Pop())
                 .ShouldThrow<InvalidOperationException>();

            var stack2 = new MyStack<int>();
            stack2.Push(10);
            stack2.Pop();
            stack2.Invoking(s => s.Pop())
                  .ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Pop_WhenAnyElements_ShouldReturnItems() {
            var stack = new MyStack<int>();
            stack.Push(10);
            stack.Push(12);
            stack.Pop().Should().Be(12);
            stack.Pop().Should().Be(10);
        }

        [TestMethod]
        public void Peek_WhenEmpty_ShouldThrowException() {
            var stack = new MyStack<int>();
            stack.Invoking(s => s.Peek())
                 .ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Peek_Should_ReturnTopElement() {
            var stack = new MyStack<int>();
            stack.Push(10);
            stack.Push(12);
            stack.Peek().Should().Be(12);
            stack.Peek().Should().Be(12);
            stack.Pop();
            stack.Peek().Should().Be(10);
            stack.Peek().Should().Be(10);
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements() {
            var stack = new MyStack<int>();
            stack.Push(10);
            stack.Push(12);
            stack.Should().NotBeEmpty();
            stack.Clear();
            stack.Should().BeEmpty();
        }

        [TestMethod]
        public void SyncRoot_ShouldBeObject() {
            new MyStack<int>().SyncRoot.Should().NotBeNull().And.BeOfType<object>();
        }

        [TestMethod]
        public void IsSynchronized_ShouldBeFalse() {
            new MyStack<int>().IsSynchronized.Should().BeFalse();
        }
    }
}