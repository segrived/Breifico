using System.Linq;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
    [TestClass]
    public class MyCircularLinkedListTests
    {
        [TestMethod]
        public void Count_ShouldReflectCollectionChanges() {
            var list = new MyCircularLinkedList<int>();
            list.Count.Should().Be(0);
            list.Add(1);
            list.Count.Should().Be(1);
            list.AddRange(new[] {2, 4});
            list.Count.Should().Be(3);
            list.RemoveAt(0);
            list.Count.Should().Be(2);
            list.Clear();
            list.Count.Should().Be(0);
        }

        [TestMethod]
        public void CircularList_InsertTest() {
            var list = new MyCircularLinkedList<int>();
            list.Should().BeEmpty();

            list.Add(12);
            list.Take(3).Should().Equal(12, 12, 12);
            list.ReverseEnumerate().Take(3).Should().Equal(12, 12, 12);

            list.Add(10);
            list.Take(5).Should().Equal(12, 10, 12, 10, 12);
            list.ReverseEnumerate().Take(5).Should().Equal(10, 12, 10, 12, 10);

            list.AddFirst(9);
            list.Take(7).Should().Equal(9, 12, 10, 9, 12, 10, 9);
            list.ReverseEnumerate().Take(7).Should().Equal(10, 12, 9, 10, 12, 9, 10);

            list.Insert(1, 3);
            list.Take(7).Should().Equal(9, 3, 12, 10, 9, 3, 12);
            list.ReverseEnumerate().Take(7).Should().Equal(10, 12, 3, 9, 10, 12, 3);
        }

        [TestMethod]
        public void CircularList_RemoveTest() {
            var list = new MyCircularLinkedList<int>();
            list.AddRange(new[] {10, 20, 30, 40});
            list.Take(9).Should().Equal(10, 20, 30, 40, 10, 20, 30, 40, 10);
            list.RemoveAt(0);
            list.Take(5).Should().Equal(20, 30, 40, 20, 30);
            list.RemoveAt(2);
            list.Take(5).Should().Equal(20, 30, 20, 30, 20);
            list.Remove(20);
            list.Take(3).Should().Equal(30, 30, 30);
            list.RemoveAt(0);
            list.Should().BeEmpty();

            var list2 = new MyCircularLinkedList<int> {22};
            list2.Remove(22);
            list2.Should().BeEmpty();
        }
    }
}