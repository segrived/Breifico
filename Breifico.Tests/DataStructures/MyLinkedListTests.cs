using System;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
    [TestClass]
    public class MyLinkedListTests
    {
        [TestMethod]
        public void NewInstance_ShouldBeEmpty() {
            new MyLinkedList<int>().Should().BeEmpty();
        }

        [TestMethod]
        public void Count_ShouldReflectCollectionChanges() {
            var list = new MyLinkedList<int>();
            list.Count.Should().Be(0);
            list.Add(10);
            list.Count.Should().Be(1);
            list.AddRange(new[] {20, 30});
            list.Count.Should().Be(3);
            list.RemoveAt(0);
            list.Count.Should().Be(2);
            list.Clear();
            list.Count.Should().Be(0);
        }

        [TestMethod]
        public void IndexerGet_WhenAnyElements_ShouldReturnElementByIndex() {
            var list = new MyLinkedList<int> {10, 20, 30};
            list[0].Should().Be(10);
            list[1].Should().Be(20);
            list[2].Should().Be(30);
        }

        [TestMethod]
        public void IndexerGet_WhenIndexOutOfRange_ShouldThrowException() {
            var list = new MyLinkedList<int> {10, 20, 30};
            list.Invoking(l => l[-1].DoNothing())
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[10].DoNothing())
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void IndexerSet_WhenAnyElements_ShouldAssignValue() {
            var list = new MyLinkedList<int> {10, 20, 30};
            list[0] = 100;
            list[0].Should().Be(100);
            list[1] = 200;
            list[1].Should().Be(200);
            list[2] = 300;
            list[2].Should().Be(300);
        }

        [TestMethod]
        public void IndexerSet_WhenIndexOutOfRange_ShouldThrowException() {
            var list = new MyLinkedList<int> {10, 20, 30};
            list.Invoking(l => l[-1] = 0)
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[10] = 0)
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Add_ShouldAddElements() {
            var list = new MyLinkedList<int> {10};
            list.Should().Equal(10);
            list.Add(20);
            list.Should().Equal(10, 20);
            list.Add(30);
            list.Should().Equal(10, 20, 30);
        }

        [TestMethod]
        public void AddRange_ShouldAddMultipleElements() {
            var list = new MyLinkedList<int>();
            list.AddRange(new[] {40, 50, 60});
            list.Should().Equal(40, 50, 60);
            list.AddRange(new[] {70, 80});
            list.Should().Equal(40, 50, 60, 70, 80);
        }

        [TestMethod]
        public void IndexOf_WhenItemNotExists_ShouldReturnMinusOne() {
            var list = new MyLinkedList<int>();
            list.AddRange(new[] { 40, 50, 60 });
            list.IndexOf(-1).Should().Be(-1);
            list.IndexOf(3).Should().Be(-1);
            list.IndexOf(12).Should().Be(-1);
        }

        [TestMethod]
        public void IndexOf_WhenItemExists_ShouldReturnIndex() {
            var list = new MyLinkedList<int>();
            list.AddRange(new[] { 40, 50, 60 });
            list.IndexOf(40).Should().Be(0);
            list.IndexOf(50).Should().Be(1);
            list.IndexOf(60).Should().Be(2);
        }

        [TestMethod]
        public void Insert_WhenEmptyList_ShouldThrowException() {
            var list = new MyLinkedList<int>();
            list.Invoking(l => l.Insert(-1, 0))
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l.Insert(1, 0))
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Insert_WhenValidIndex_ShouldInsertElements() {
            var list = new MyLinkedList<int>();
            list.Insert(0, 10);
            list.Should().Equal(10);
            list.Insert(0, 20);
            list.Should().Equal(20, 10);
            list.Insert(2, 30);
            list.Should().Equal(20, 10, 30);
            list.Insert(1, 40);
            list.Should().Equal(20, 40, 10, 30);
            list.Insert(1, 10);
            list.Should().Equal(20, 10, 40, 10, 30);
        }

        [TestMethod]
        public void Insert_WhenIndexOutOfRange_ShouldThrowException() {
            var list = new MyLinkedList<int>();
            list.AddRange(new[] {10, 20, 30, 40});
            list.Invoking(l => l.Insert(5, 0))
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l.Insert(-1, 0))
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Contains_WhenEmpty_ShouldAlwaysReturnFalse() {
            var list = new MyLinkedList<int>();
            list.Contains(0).Should().BeFalse();
        }

        [TestMethod]
        public void Contains_ShouldReturnTrueIfContains() {
            var list = new MyLinkedList<int> { 10, 20, 30 };
            list.Contains(10).Should().BeTrue();
            list.Contains(15).Should().BeFalse();
            list.Contains(30).Should().BeTrue();
            list.Contains(20).Should().BeTrue();
            list.Contains(25).Should().BeFalse();
        }

        [TestMethod]
        public void Remove_WhenEmpty_ShouldThrowExcepetion() {
            var list = new MyLinkedList<int>();
            list.Invoking(l => l.RemoveAt(0))
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Remove_WhenAnyElements_ShouldRemoveElement() {
            var list = new MyLinkedList<int> {1, 2, 3};
            list.RemoveAt(0);
            list.Should().Equal(2, 3);
            list.RemoveAt(1);
            list.Should().Equal(2);
            list.RemoveAt(0);
            list.Should().BeEmpty();
        }

        [TestMethod]
        public void Remove_WhenIndexOutOfRange_ShouldThrowException() {
            var list = new MyLinkedList<int> {1, 2, 3};
            list.Invoking(l => l.RemoveAt(-1))
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l.RemoveAt(10))
                .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Remove_WhenLastIndexMultipleTimes_ShouldNotThrowException() {
            var list = new MyLinkedList<int> {1, 2, 3};
            list.RemoveAt(2);
            list.Add(10);
            list.RemoveAt(2);
        }

        [TestMethod]
        public void RemoveFirst_WhenIncluded_ShouldRemoveItemAndReturnTrue() {
            var list = new MyLinkedList<int> {1, 2, 3};
            list.Remove(2).Should().BeTrue();
            list.Should().Equal(1, 3);

            var list2 = new MyLinkedList<int> {1, 2, 2, 2, 3};
            list2.Remove(2).Should().BeTrue();
            list2.Should().Equal(1, 2, 2, 3);
            list2.Remove(3).Should().BeTrue();
            list2.Should().Equal(1, 2, 2);
            list2.Remove(1).Should().BeTrue();
            list2.Should().Equal(2, 2);
        }

        [TestMethod]
        public void RemoveFirst_WhenNotIncluded_ShouldReturnFalse() {
            var list = new MyLinkedList<int> {1, 2, 3};
            list.Remove(4).Should().BeFalse();
            list.Should().Equal(1, 2, 3);
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements() {
            var list = new MyLinkedList<int> {10, 20};
            list.Should().NotBeEmpty();
            list.Clear();
            list.Should().BeEmpty();
        }

        [TestMethod]
        public void ReverseEnumerate_ShouldIterateBackward() {
            var list = new MyLinkedList<int> {10, 20, 30, 40};
            list.ReverseEnumerate().Should().Equal(40, 30, 20, 10);
            list.RemoveAt(0);
            list.ReverseEnumerate().Should().Equal(40, 30, 20);
            list.Clear();
            list.ReverseEnumerate().Should().BeEmpty();
        }

        [TestMethod]
        public void ToString_ShouldReturnString() {
            var list1 = new MyLinkedList<int> { 10, 20 };
            list1.ToString().Should().Be("S: 10, E: 20");

            var list2 = new MyLinkedList<int>();
            list2.ToString().Should().Be("S: null, E: null");
        }

        [TestMethod]
        public void SyncRoot_ShouldBeObject() {
            new MyLinkedList<int>().SyncRoot.Should().NotBeNull().And.BeOfType<object>();
        }

        [TestMethod]
        public void IsSynchronized_ShouldBeFalse() {
            new MyLinkedList<int>().IsSynchronized.Should().BeFalse();
        }
    }
}