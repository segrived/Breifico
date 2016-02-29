using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.DataStructures.UnitTests
{
    [TestClass]
    public class MyHashSetTests
    {
        [TestMethod]
        public void TestBlaBlaBla() {
            var hashSet = new MyHashSet<int>();
            var rnd = new Random();
            var o = Enumerable.Range(0, 1000).Select(_ => rnd.Next(0, 100000));
            foreach (var x in o) {
                hashSet.Add(x);
            }
            var y = hashSet.ToString();
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
            hashSet.AddRange(new[] {1, 2, 2, 3, 4, 4, 1, 5, 6, 7, 4, 5, 8, 4, 8, 4, 8, 9});
            hashSet.Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }
    }
}