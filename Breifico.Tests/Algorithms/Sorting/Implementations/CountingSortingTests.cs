using System;
using Breifico.Algorithms.Sorting;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Sorting.Implementations
{
    [TestClass]
    public class CountingSortingTests : GenericSortingTests
    {
        public CountingSortingTests() : base(new CountingSorting())
        {
        }

        [TestMethod]
        public void CountingSorting_Sort_WhenElementGreaterTheMaxElement_ShouldThrowExeption()
        {
            var sorter = new CountingSorting(100);
            var input = new[] {1, 12, 57, 102, 55};
            sorter.Invoking(s => s.Sort(input)).ShouldThrow<Exception>();
        }

        [TestMethod]
        public void CountingSorting_Sort_WhenContainsNegativeElement_ShoulThrowException()
        {
            var sorter = new CountingSorting(10);
            var input = new[] {1, 2, 3, -2};
            sorter.Invoking(s => s.Sort(input)).ShouldThrow<Exception>();
        }

        [TestMethod]
        public void CountingSorting_Sort_WhenBigNumbers_ShouldSort()
        {
            var sorter = new CountingSorting(10000);
            var input = new[]  { 2554, 3695, 1856, 9999, 10000, 2, 54, 897, 3636, 7496 };
            sorter.Sort(input).Should().Equal(2, 54, 897, 1856, 2554, 3636, 3695, 7496, 9999, 10000);

            var sorter2 = new CountingSorting(10000);
            var input2 = new[] { 10000, 10000, 122, 122, 122, 122, 9999, 9999 };
            sorter2.Sort(input2).Should().Equal(122, 122, 122, 122, 9999, 9999, 10000, 10000);
        }
    }
}
