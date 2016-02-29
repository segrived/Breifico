using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.DataStructures.UnitTests
{
    [TestClass]
    public class MyListTests
    {
        [TestMethod]
        public void NewInstance_SouldBeEmpty() {
            
        }

        [TestMethod]
        public void CapacityTest() {
            var list = new MyList<int>(2);
            list.Capacity.Should().Be(2);
            var list2 = new MyList<int>(12);
            list2.Capacity.Should().Be(12);

            var list3 = new MyList<int>(2);
            list3.Add(0); // 1 element
            list3.Capacity.Should().Be(2);
            list3.Add(0); // 2 elements
            list3.Capacity.Should().Be(2);
            list3.Add(0); // 3 elements
            list3.Capacity.Should().Be(4);
            list3.Add(0); // 4 elements
            list3.Capacity.Should().Be(4);
            list3.Add(0); // 5 elements
            list3.Capacity.Should().Be(8);
            list3.AddRange(Enumerable.Range(0, 3).ToArray()); // 8 elements
            list3.Capacity.Should().Be(8);
            list3.Add(0); // 9 elements
            list3.Capacity.Should().Be(16);

            list3.Invoking(l => l.Capacity = 9).ShouldNotThrow();
            list3.Invoking(l => l.Capacity = 8).ShouldThrow<ArgumentException>();
            list3.Capacity.Should().Be(9);
        }

        [TestMethod]
        public void NewInstance_WithNegativeOrZeroCapacity_ShouldThrowException() {
            Action a = () => new MyList<int>(0);
            a.ShouldThrow<ArgumentOutOfRangeException>();
            Action b = () => new MyList<int>(-2);
            b.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void NewInstance_ShouldHasSpecifiedCapacity() {
            new MyList<int>(20).Capacity.Should().Be(20);
            new MyList<int>(1).Capacity.Should().Be(1);
        }


        [TestMethod]
        public void Add_ShouldAddNewElements() {
            var list = new MyList<int>(2) {1, 2, 3, 4, 5, 6};
            list.Count.Should().Be(6);
        }

        [TestMethod]
        public void AddRange_ShouldAddRange() {
            var list = new MyList<int>(2);
            list.AddRange(Enumerable.Range(0, 5).ToArray());
            list.Count.Should().Be(5);
            list.Should().Equal(0, 1, 2, 3, 4);
            list.AddRange(Enumerable.Range(20, 2));
            list.Should().Equal(0, 1, 2, 3, 4, 20, 21);
        }

        [TestMethod]
        public void Remove_ShouldRemoveElements() {
            var list = new MyList<int> {0,1,2,3,4,5,6,7,8,9,10};
            list.RemoveAt(4); // remove element in middle
            list.Should().Equal(0, 1, 2, 3, 5, 6, 7, 8, 9, 10);
            list.RemoveAt(0); // remove first element
            list.Should().Equal(1, 2, 3, 5, 6, 7, 8, 9, 10);
            list.RemoveAt(list.Count - 1); // remove last element
            list.Should().Equal(1, 2, 3, 5, 6, 7, 8, 9);

            var list2 = new MyList<int> {0};
            list2.RemoveAt(0);
            list2.Should().BeEmpty();
        }

        [TestMethod]
        public void Remove_WhenIndexOutOfRange_ShouldThrowException() {
            var list = new MyList<int> { 0, 1, 2, 3, 4, 5 };
            list.Invoking(l => l.RemoveAt(-1)).ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l.RemoveAt(6)).ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void IndexerGet_Test() {
            var list = new MyList<int> { 0, 1, 2, 3, 4, 5 };
            list[0].Should().Be(0);
            list[3].Should().Be(3);
            list[5].Should().Be(5);
            list.Add(22);
            list[6].Should().Be(22);
        }

        [TestMethod]
        public void IndexerGet_WhenOutOfRange_ShouldThrowException() {
            var list = new MyList<int> { 0, 1, 2, 3, 4, 5 };
            list.Invoking(l => l[-1].ToString())
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[6].ToString())
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[20].ToString())
                .ShouldThrow<IndexOutOfRangeException>();

            var list2 = new MyList<int>();
            list2.Invoking(l => l[0]++)
                 .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void IndexerSet_Test() {
            var list = new MyList<int> { 0, 1, 2, 3, 4, 5 };
            list[0].Should().Be(0);
            list[0] = 12;
            list[0].Should().Be(12);
            list[5] = 10;
            list[5].Should().Be(10);
        }

        [TestMethod]
        public void IndexerSet_WhenOutOfRange_ShouldThrowException() {
            var list = new MyList<int> { 0, 1, 2, 3, 4, 5 };
            list.Invoking(l => l[-1] = 1)
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[6] = 1)
                .ShouldThrow<IndexOutOfRangeException>();
            list.Invoking(l => l[20] = 1)
                .ShouldThrow<IndexOutOfRangeException>();

            var list2 = new MyList<int>();
            list2.Invoking(l => l[0] = 1)
                 .ShouldThrow<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Contains_WhenEmpty_ShouldAlwaysReturnFalse() {
            var list = new MyList<int>();
            list.Contains(0).Should().BeFalse(); 
        }

        [TestMethod]
        public void Contains_WhenContainElement_ShouldReturnTrue() {
            var list = new MyList<int> {10,20,30};
            list.Contains(10).Should().BeTrue();
            list.Contains(15).Should().BeFalse();
            list.Contains(30).Should().BeTrue();
            list.Contains(20).Should().BeTrue();
            list.Contains(25).Should().BeFalse();
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements() {
            var list = new MyList<int> { 0, 1, 2, 3, 4, 5 };
            list.Clear();
            list.Count.Should().Be(0);
            list.Should().BeEmpty();
        }

        [TestMethod]
        public void SyncRoot_ShouldBeObject() {
            new MyList<int>().SyncRoot.Should().NotBeNull()
                .And.BeOfType<object>();
        }
    }
}
