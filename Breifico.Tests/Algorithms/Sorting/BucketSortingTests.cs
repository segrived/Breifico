using System;
using System.Linq;
using Breifico.Algorithms.Numeric;
using Breifico.Algorithms.Sorting;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Sorting
{
    [TestClass]
    public class BucketSortingTests
    {
        private readonly Func<int, int, int, int, int> _sorter = (i, min, max, len) => {
            return (int)Math.Round(((double)i - min) / (max - min) * len);
        };

        [TestMethod]
        public void CountingSorting_Sort_WhenArrayIsEmpty_ShouldBeEmpty() {
            var input = new int[] { };
            new BucketSorting<int>(this._sorter).Sort(input).Should().BeEmpty();
        }

        [TestMethod]
        public void CountingSorting_Sort_WhenOneElementInArray() {
            var input = new[] { 12 };
            new BucketSorting<int>(this._sorter).Sort(input).Should().Equal(12);
        }

        [TestMethod]
        public void CountingSorting_Sort_SimpleCases_ShouldSort() {
            var input = new[] { 1, 2 };
            new BucketSorting<int>(this._sorter).Sort(input).Should().Equal(1, 2);

            var input2 = new[] { 2, 1 };
            new BucketSorting<int>(this._sorter).Sort(input2).Should().Equal(1, 2);

            var input3 = new[] { 9, 5, 4, 3 };
            new BucketSorting<int>(this._sorter).Sort(input3).Should().Equal(3, 4, 5, 9);

            var input4 = new[] { 5, 9, 5, 9, 2, 1, 2, 6, 5, 4, 7 };
            new BucketSorting<int>(this._sorter).Sort(input4).Should().Equal(1, 2, 2, 4, 5, 5, 5, 6, 7, 9, 9);

            var input5 = new[] { 1, 2, 3, 4, 5, 6, 7 };
            new BucketSorting<int>(this._sorter).Sort(input5).Should().Equal(1, 2, 3, 4, 5, 6, 7);
        }

        [TestMethod]
        public void CountingSorting_Sort_WhenBigNumbers_ShouldSort() {
            var rndGen = new LinearCongruentialGenerator();
            int[] input = rndGen.GenerateInRange(500, 1000).Take(1000).ToArray();
            new BucketSorting<int>(50, this._sorter).Sort(input).Should().BeInAscendingOrder();
        }
    }
}
