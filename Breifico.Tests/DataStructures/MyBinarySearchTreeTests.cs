using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
    [TestClass]
    public class MyBinarySearchTreeTests
    {
        [TestMethod]
        public void Test() {
            //var tree = new MyBinarySearchTree<int> {1, 2, 3, 4, 5};
            //tree.Should().Equal(1, 2, 3, 4, 5);
            //tree.Remove(1);
            //tree.Should().Equal(2, 3, 4, 5);
            //tree.Remove(5);
            //tree.Should().Equal(2, 3, 4);
            //tree.Remove(3);
            //tree.Should().Equal(2, 4);

            //// when only root node exists
            //var tree2 = new MyBinarySearchTree<int> {1};
            //tree2.Should().Equal(1);
            //tree2.Remove(1);
            //tree2.Should().BeEmpty();

            //// remove non-root node with only one child
            //var tree3 = new MyBinarySearchTree<int> { 2 , 4, 3 };
            //tree3.Should().Equal(2, 3, 4);
            //tree3.Remove(4);
            //tree3.Should().Equal(2,3);

            //// remove root node with only one child
            //var tree4 = new MyBinarySearchTree<int> { 4, 2 };
            //tree4.Should().Equal(2, 4);
            //tree4.Remove(4);
            //tree4.Should().Equal(2);

            // remove root node with only one child
            //var tree5 = new MyBinarySearchTree<int> { 10, 8, 20 };
            //tree5.Should().Equal(8, 10, 20);
            //tree5.Remove(10);
            //tree5.Should().Equal(8, 20);
        }

        [TestMethod]
        public void Contains_WhenContains_ShouldReturnTrue() {
            var tree = new MyBinarySearchTree<int> { 10, 20, 30, 40 };
            tree.Contains(10).Should().BeTrue();
            tree.Contains(20).Should().BeTrue();
            tree.Contains(30).Should().BeTrue();
            tree.Contains(40).Should().BeTrue();
        }

        [TestMethod]
        public void Contains_WhenNotContains_ShouldReturnFalse() {
            var tree = new MyBinarySearchTree<int> { 10, 20, 30, 40 };
            tree.Contains(0).Should().BeFalse();
            tree.Contains(11).Should().BeFalse();
            tree.Contains(-1).Should().BeFalse();
            tree.Contains(50).Should().BeFalse();
        }

        [TestMethod]
        public void SyncRoot_ShouldBeObject() {
            new MyBinarySearchTree<int>().SyncRoot.Should().NotBeNull().And.BeOfType<object>();
        }

        [TestMethod]
        public void IsSynchronized_ShouldBeFalse() {
            new MyBinarySearchTree<int>().IsSynchronized.Should().BeFalse();
        }
    }
}