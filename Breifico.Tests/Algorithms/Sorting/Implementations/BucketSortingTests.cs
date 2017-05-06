using System;
using System.Linq;
using Breifico.Algorithms.Numeric;
using Breifico.Algorithms.Sorting;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Sorting
{
    [TestClass]
    public class BucketSortingTests : GenericSortingTests
    {
        public BucketSortingTests() : base(new BucketIntSorting())
        {
        }

        [TestMethod]
        public void CountingSorting_Sort_WhenBigNumbers_ShouldSort() {
            var rndGen = new LinearCongruentialGenerator();
            var input = rndGen.GenerateInRange(500, 1000).Take(1000).ToArray();
            new BucketIntSorting(50).Sort(input).Should().BeInAscendingOrder();
        }
    }
}
