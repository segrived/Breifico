using System;
using Breifico.Algorithms.Sorting;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Sorting
{
    [TestClass]
    public class CountingSortingTests
    {
        [TestMethod]
        public void CountingSorting_Sort_WhenArrayIsEmpty_ShouldBeEmpty() {
            var input = new uint[] { };
            input.MySort<uint>(new CountingSorting()).Should().BeEmpty();
        }

        [TestMethod]
        public void CountingSorting_Sort_WhenOneElementInArray() {
            var input = new uint[] { 12 };
            input.MySort(new CountingSorting()).Should().Equal(12);
        }

        [TestMethod]
        public void CountingSorting_Sort_SimpleCases_ShouldSort() {
            var input = new uint[] { 1, 2 };
            input.MySort(new CountingSorting()).Should().Equal(1, 2);

            var input2 = new uint[] { 2, 1 };
            input2.MySort(new CountingSorting()).Should().Equal(1, 2);

            var input3 = new uint[] { 9, 5, 4, 3 };
            input3.MySort(new CountingSorting()).Should().Equal(3, 4, 5, 9);

            var input4 = new uint[] { 5, 9, 5, 9, 2, 1, 2, 6, 5, 4, 7 };
            input4.MySort(new CountingSorting()).Should().Equal(1, 2, 2, 4, 5, 5, 5, 6, 7, 9, 9);

            var input5 = new uint[] { 1, 2, 3, 4, 5, 6, 7 };
            input5.MySort(new CountingSorting()).Should().Equal(1, 2, 3, 4, 5, 6, 7);
        }

        [TestMethod]
        public void CountingSorting_Sort_WhenElementGreaterTheMaxElement_ShouldThrowExeption() {
            var sorter = new CountingSorting(100);
            var input = new uint[] {1, 12, 57, 102, 55};
            sorter.Invoking(s => s.Sort(input)).ShouldThrow<Exception>();;
        }

        [TestMethod]
        public void CountingSorting_Sort_WhenBigNumbers_ShouldSort() {
            var sorter = new CountingSorting(10000);
            var input = new uint[] { 2554, 3695, 1856, 9999, 10000, 2, 54, 897, 3636, 7496 };
            sorter.Sort(input).Should().Equal(2, 54, 897, 1856, 2554, 3636, 3695, 7496, 9999, 10000);

            var sorter2 = new CountingSorting(10000);
            var input2 = new uint[] { 10000, 10000, 122, 122, 122, 122, 9999, 9999 };
            sorter2.Sort(input2).Should().Equal(122, 122, 122, 122, 9999, 9999, 10000, 10000);
        }
    }
}
