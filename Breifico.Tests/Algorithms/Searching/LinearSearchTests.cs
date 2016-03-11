using Breifico.Algorithms.Searching;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Searching
{
    [TestClass]
    public class LinearSearchTests
    {
        [TestMethod]
        public void Search_WhenEmpty_MinusOne() {
            new LinearSearch<int>().Search(new int[] { }, 0).Should().Be(-1);
            new LinearSearch<int>().Search(new int[] { }, 1).Should().Be(-1);
            new LinearSearch<int>().Search(new int[] { }, -1).Should().Be(-1);
        }

        [TestMethod]
        public void Search_WhenOneElement_Index() {
            new LinearSearch<int>().Search(new int[] { 2}, 2).Should().Be(0);
            new LinearSearch<int>().Search(new int[] {2 }, 1).Should().Be(-1);
        }

        [TestMethod]
        public void Search_WhenTwoElements_Index() {
            new LinearSearch<int>().Search(new int[] { 10, 20 }, 10).Should().Be(0);
            new LinearSearch<int>().Search(new int[] { 10, 20 }, 20).Should().Be(1);
            new LinearSearch<int>().Search(new int[] { 10, 20 }, 30).Should().Be(-1);
            new LinearSearch<int>().Search(new int[] { 10, 20 }, 15).Should().Be(-1);
        }

        [TestMethod]
        public void Search_WhenManyElements_Index() {
            new LinearSearch<int>().Search(new int[] { 10, 20, 30, 40 }, 20).Should().Be(1);
            new LinearSearch<int>().Search(new int[] { 10, 20, 30, 40, 50 }, 30).Should().Be(2);
            new LinearSearch<int>().Search(new int[] { 10, 20, 30, 40, 50, 60 }, 60).Should().Be(5);
            new LinearSearch<int>().Search(new int[] { 10, 20, 30, 40 }, 45 ).Should().Be(-1);
        }
    }
}
