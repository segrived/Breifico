using System;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
    [TestClass]
    public class MySortedLinkedListTests
    {
        [TestMethod]
        public void Add_ShouldAddElementsInSortedOrder() {
            var list = new MySortedLinkedList<int>();
            list.Add(10);
            list.Should().Equal(10);
            list.Add(5);
            list.Should().Equal(5, 10);
            list.Add(12);
            list.Should().Equal(5, 10, 12);
            list.AddRange(new[] {1,3,20});
            list.Should().Equal(1,3,5,10,12,20);
            list.Add(-50);
            list.Should().Equal(-50, 1, 3, 5, 10, 12, 20);
            list.Add(1);
            list.Should().Equal(-50, 1, 1, 3, 5, 10, 12, 20);
        }

        [TestMethod]
        public void AddAfter_ShouldThrowException() {
            var list = new MySortedLinkedList<int>();
            list.Invoking(l => l.AddAfter(0)).ShouldThrow<NotSupportedException>();
        }

        [TestMethod]
        public void AddFirst_ShouldThrowException() {
            var list = new MySortedLinkedList<int>();
            list.Invoking(l => l.AddFirst(0)).ShouldThrow<NotSupportedException>();
        }

        [TestMethod]
        public void AddLast_ShouldThrowException() {
            var list = new MySortedLinkedList<int>();
            list.Invoking(l => l.AddLast(0)).ShouldThrow<NotSupportedException>();
        }

        [TestMethod]
        public void Insert_ShouldThrowException() {
            var list = new MySortedLinkedList<int>();
            list.Invoking(l => l.Insert(0, 0)).ShouldThrow<NotSupportedException>();
        }
    }
}