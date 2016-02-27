using System;
using System.Collections.Generic;
using Breifico.DataStructures.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.DataStructures.UnitTests.TestHelpers
{
    public static class LinkedListTestHelpers
    {
        public static void NewInstance_ShouldHasZeroElements(ILinkedList<int> list) {
            Assert.AreEqual(list.Count, 0);
        }

        public static void Add_ShouldAddElements(ILinkedList<int> list) {
            list.Add(2);
            list.Count.Should().Be(1);
            list.Should().Equal(2);

            list.Add(7);
            list.Count.Should().Be(2);
            list.Should().Equal(2, 7);
        }

        public static void Insert_ShouldInsertElement(ILinkedList<int> list) {
            list.AddRange(10, 20, 30);

            list.Insert(15, 1);
            list.Count.Should().Be(4);
            list.Should().Equal(10, 15, 20, 30);

            list.Insert(5, 0);
            list.Count.Should().Be(5);
            list.Should().Equal(5, 10, 15, 20, 30);

            list.Insert(100, 4);
            list.Count.Should().Be(6);
            list.Should().Equal(5, 10, 15, 20, 100, 30);

            list.Insert(120, 6);
            list.Count.Should().Be(7);
            list.Should().Equal(5, 10, 15, 20, 100, 30, 120);
        }

        public static void Insert_ShouldInsertCorrectlyInEmptyList(ILinkedList<int> list) {
            list.Insert(100, 0);
            list.Count.Should().Be(1);
            list.Should().Equal(100);
        }

        public static void Insert_OutOfRangeIndex_ShouldThrowException(ILinkedList<int> list) {
            list.AddRange(10, 20, 30);
            list.Invoking(x => x.Insert(0, -1)).ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(x => x.Insert(0, 4)).ShouldThrow<IndexOutOfRangeException>();
            list.Count.Should().Be(3);
        }

        public static void Remove_OutOfRangeIndex_ShouldThrowException(ILinkedList<int> list) {
            list.AddRange(10, 20, 30);

            list.Invoking(x => x.Remove(-1)).ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(x => x.Remove(-4)).ShouldThrow<IndexOutOfRangeException>();
        }

        public static void Remove_ShouldRemoveElements(ILinkedList<int> list) {
            list.AddRange(10, 20, 30, 40);

            list.Remove(2);
            list.Count.Should().Be(3);
            list.Should().Equal(10, 20, 40);

            list.Remove(0);
            list.Count.Should().Be(2);
            list.Should().Equal(20, 40);

            list.Remove(1);
            list.Count.Should().Be(1);
            list.Should().Equal(20);

            list.Remove(0);
            list.Count.Should().Be(0);
            list.Should().BeEmpty();
        }

        public static void Clear_ShouldRemoveAllElements(ILinkedList<int> list) {
            list.AddRange(10, 20, 30);

            list.Clear();
            list.Count.Should().Be(0);
            list.Should().BeEmpty();
        }

        public static void Get_OutOfRangeIndex_ShouldThrowException(ILinkedList<int> list) {
            list.AddRange(10, 20, 30);

            list.Invoking(x => x.Get(-1)).ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(x => x.Get(3)).ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(x => x.Get(4)).ShouldThrow<IndexOutOfRangeException>();
        }

        public static void Get_ShouldThrowExceptionOnEmptyList(ILinkedList<int> list) {
            list.Invoking(x => x.Get(0)).ShouldThrow<IndexOutOfRangeException>();
        }

        public static void Get_ShouldReturnElementByIndex(ILinkedList<int> list) {
            list.AddRange(10, 20, 30);

            list.Get(0).Should().Be(10);
            list.Get(1).Should().Be(20);
            list.Get(2).Should().Be(30);
        }

        public static void ToString_Test(ILinkedList<int> list) {
            list.AddRange(10, 20, 30);

            list.ToString().Should().Be("S: 10, E: 30");
        }

        public static void ToString_ShouldCorrectlyDisplayNullValues(ILinkedList<int> list) {
            list.ToString().Should().Be("S: null, E: null");
        }
    }
}
