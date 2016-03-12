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
            hashSet.Remove(20).Should().BeTrue();
            hashSet.Should().Equal(10, 30, 40);
            hashSet.Remove(20).Should().BeFalse();
            hashSet.Should().Equal(10, 30, 40);
            hashSet.Remove(10).Should().BeTrue();
            hashSet.Should().Equal(30, 40);
        }

        [TestMethod]
        public void Remove_WhenEmpty_ShouldDoNothing() {
            var hashSet = new MyHashSet<int>();
            hashSet.Remove(0).Should().BeFalse();
            hashSet.Should().BeEmpty();
        }

        [TestMethod]
        public void Union_Test() {
            var hashSet1 = new MyHashSet<int>();
            hashSet1.AddRange(new[] { 2, 3, 4 });
            var hashSet2 = new MyHashSet<int>();
            hashSet2.AddRange(new[] { 4, 5, 6 });
            hashSet1.Union(hashSet2).Should().Equal(2, 3, 4, 5, 6);

            var hashSet3 = new MyHashSet<int>();
            hashSet3.AddRange(new[] { 5, 10, 20 });
            var hashSet4 = new MyHashSet<int>();
            hashSet4.AddRange(new[] { 20, 10, 5 });
            hashSet3.Union(hashSet4).Should().Equal(5, 10, 20);

            var hashSet5 = new MyHashSet<int>();
            hashSet5.AddRange(new int[0]);
            var hashSet6 = new MyHashSet<int>();
            hashSet6.AddRange(new[] { -5 });
            hashSet5.Union(hashSet6).Should().Equal(-5);
        }

        [TestMethod]
        public void Intersection_Test() {
            var hashSet1 = new MyHashSet<int>();
            hashSet1.AddRange(new[] { 2, 3, 4 });
            var hashSet2 = new MyHashSet<int>();
            hashSet2.AddRange(new[] { 4, 5, 6 });
            hashSet1.Intersection(hashSet2).Should().Equal(4);

            var hashSet3 = new MyHashSet<int>();
            hashSet3.AddRange(new[] { 5, 10, 20 });
            var hashSet4 = new MyHashSet<int>();
            hashSet4.AddRange(new[] { 20, 10, 5 });
            hashSet3.Intersection(hashSet4).Should().Equal(5, 10, 20);

            var hashSet5 = new MyHashSet<int>();
            hashSet5.AddRange(new int[0]);
            var hashSet6 = new MyHashSet<int>();
            hashSet6.AddRange(new[] { -5 });
            hashSet5.Intersection(hashSet6).Should().Equal();
        }

        [TestMethod]
        public void Complement_Test() {
            var hashSet1 = new MyHashSet<int>();
            hashSet1.AddRange(new[] { 2, 3, 4 });
            var hashSet2 = new MyHashSet<int>();
            hashSet2.AddRange(new[] { 4, 5, 6 });
            hashSet1.Complement(hashSet2).Should().Equal(2, 3);
            hashSet2.Complement(hashSet1).Should().Equal(5, 6);

            var hashSet3 = new MyHashSet<int>();
            hashSet3.AddRange(new[] { 5, 10, 20 });
            var hashSet4 = new MyHashSet<int>();
            hashSet4.AddRange(new[] { 20, 10, 5 });
            hashSet3.Complement(hashSet4).Should().BeEmpty();
            hashSet4.Complement(hashSet3).Should().BeEmpty();

            var hashSet5 = new MyHashSet<int>();
            hashSet5.AddRange(new int[0]);
            var hashSet6 = new MyHashSet<int>();
            hashSet6.AddRange(new[] { -5 });
            hashSet5.Complement(hashSet6).Should().BeEmpty();
            hashSet6.Complement(hashSet5).Should().Equal(-5);
        }

        [TestMethod]
        public void Difference_Test() {
            var hashSet1 = new MyHashSet<int>();
            hashSet1.AddRange(new[] { 2, 3, 4 });
            var hashSet2 = new MyHashSet<int>();
            hashSet2.AddRange(new[] { 4, 5, 6 });
            hashSet1.Difference(hashSet2).Should().Equal(2, 3, 5, 6);

            var hashSet3 = new MyHashSet<int>();
            hashSet3.AddRange(new[] { 5, 10, 20 });
            var hashSet4 = new MyHashSet<int>();
            hashSet4.AddRange(new[] { 20, 10, 5 });
            hashSet3.Difference(hashSet4).Should().BeEmpty();

            var hashSet5 = new MyHashSet<int>();
            hashSet5.AddRange(new int[0]);
            var hashSet6 = new MyHashSet<int>();
            hashSet6.AddRange(new[] { -5 });
            hashSet5.Difference(hashSet6).Should().Equal(-5);
        }

        [TestMethod]
        public void IsSubsetOf_Test() {
            var hashSet1 = new MyHashSet<int>();
            hashSet1.AddRange(new [] { 1, 2, 3, 4, 5 });

            var hashSet2 = new MyHashSet<int>();
            hashSet2.AddRange(new[] { 2, 3, 4 });

            var hashSet3 = new MyHashSet<int>();
            hashSet3.AddRange(new[] { 2, 3, 4 });

            hashSet2.IsSubsetOf(hashSet1).Should().BeTrue();
            hashSet1.IsSubsetOf(hashSet2).Should().BeFalse();

            hashSet2.IsSubsetOf(hashSet3).Should().BeTrue();
            hashSet3.IsSubsetOf(hashSet2).Should().BeTrue();
        }

        [TestMethod]
        public void IsSupersetOf_Test() {
            var hashSet1 = new MyHashSet<int>();
            hashSet1.AddRange(new[] { 1, 2, 3, 4, 5 });

            var hashSet2 = new MyHashSet<int>();
            hashSet2.AddRange(new[] { 2, 3, 4 });

            var hashSet3 = new MyHashSet<int>();
            hashSet3.AddRange(new[] { 2, 3, 4 });

            hashSet2.IsSupersetOf(hashSet1).Should().BeFalse();
            hashSet1.IsSupersetOf(hashSet2).Should().BeTrue();

            hashSet2.IsSupersetOf(hashSet3).Should().BeTrue();
            hashSet3.IsSupersetOf(hashSet2).Should().BeTrue();
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
