using System;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
    [TestClass]
    public class MyHashSetTests
    {
        [TestMethod]
        public void NewInstance_WhenPositionBacketsNumber_ShouldNotThrowException() {
            var c = new Action(() => new MyHashSet<int>(20));
            c.ShouldNotThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void NewInstance_WhenInvalidBacketsNumber_ShouldThrowException() {
            var a = new Action(() => new MyHashSet<int>(0));
            var b = new Action(() => new MyHashSet<int>(-1));
            a.ShouldThrow<ArgumentOutOfRangeException>();
            b.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void Add_WhenDuplicate_ShouldNotBeAdded() {
            var hashSet = new MyHashSet<int>();
            hashSet.Add(2).Should().BeTrue();
            hashSet.Count.Should().Be(1);
            hashSet.Add(2).Should().BeFalse();
            hashSet.Count.Should().Be(1);
            hashSet.Add(3).Should().BeTrue();
            hashSet.Count.Should().Be(2);
            hashSet.Add(2).Should().BeFalse();
            hashSet.Add(3).Should().BeFalse();
            hashSet.Count.Should().Be(2);
        }

        [TestMethod]
        public void AddRange_ShouldAddElements() {
            var hashSet = new MyHashSet<int>();
            hashSet.AddRange(new[] { 1, 2, 2, 3, 4, 4, 1, 5, 6, 7, 4, 5, 8, 4, 8, 4, 8, 9 });
            hashSet.Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        [TestMethod]
        public void Contains_WhenElementExists_ShouldReturnTrue() {
            var hashSet = new MyHashSet<int> { 10, 20, 30 };
            hashSet.Contains(10).Should().BeTrue();
            hashSet.Contains(20).Should().BeTrue();
            hashSet.Contains(30).Should().BeTrue();
        }

        [TestMethod]
        public void Contains_WhenElementNotExists_ShouldReturnFalse() {
            var hashSet = new MyHashSet<int> { 10, 20, 30 };
            hashSet.Contains(11).Should().BeFalse();
            hashSet.Contains(22).Should().BeFalse();
            hashSet.Contains(33).Should().BeFalse();
            hashSet.Add(33);
            hashSet.Contains(33).Should().BeTrue();
        }

        [TestMethod]
        public void Remove_WhenExists_ShouldRemoveElement() {
            var hashSet = new MyHashSet<int> { 10, 20, 30, 40 };
            hashSet.Remove(20);
            hashSet.Should().Equal(10, 30, 40);
            hashSet.Remove(20);
            hashSet.Should().Equal(10, 30, 40);
            hashSet.Remove(10);
            hashSet.Should().Equal(30, 40);
        }

        [TestMethod]
        public void Remove_WhenEmpty_ShouldDoNothing() {
            var hashSet = new MyHashSet<int>();
            hashSet.Remove(0);
            hashSet.Should().BeEmpty();
        }

        [TestMethod]
        public void SyncRoot_ShouldBeObject() {
            new MyHashSet<int>().SyncRoot.Should().NotBeNull()
                .And.BeOfType<object>();
        }

        [TestMethod]
        public void IsSynchronized_ShouldBeFalse() {
            new MyHashSet<int>().IsSynchronized.Should().BeFalse();
        }
    }
}
