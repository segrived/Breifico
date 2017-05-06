using System.Linq;
using Breifico.Algorithms.Numeric;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Sorting
{
    public abstract class GenericSortingTests
    {
        private readonly ISorter<int> _sorter;

        protected GenericSortingTests(ISorter<int> sorter)
        {
            this._sorter = sorter;
        }

        [TestMethod]
        public void SelectionSorting_Sort_WhenArrayIsEmpty_ShouldBeEmpty()
        {
            var input = new int[] { };
            input.MySort(this._sorter).Should().BeEmpty();
        }

        [TestMethod]
        public void SelectionSorting_Sort_WhenOneElementInArray()
        {
            var input1 = new[] { 0 };
            input1.MySort(this._sorter).Should().Equal(0);

            var input2 = new[] { 12 };
            input2.MySort(this._sorter).Should().Equal(12);
        }

        [TestMethod]
        public void SelectionSorting_Sort_WhenMultipleElementsInArray_ShouldSort()
        {
            var input1 = new[] { 1, 2 };
            input1.MySort(this._sorter).Should().Equal(1, 2);

            var input2 = new[] { 2, 1 };
            input2.MySort(this._sorter).Should().Equal(1, 2);

            var input3 = new[] { 9, 5, 4, 3 };
            input3.MySort(this._sorter).Should().Equal(3, 4, 5, 9);

            var input4 = new[] { 5, 9, 5, 9, 2, 1, 2, 6, 5, 4, 7 };
            input4.MySort(this._sorter).Should().Equal(1, 2, 2, 4, 5, 5, 5, 6, 7, 9, 9);

            var input5 = new[] { 1, 2, 3, 4, 5, 6, 7 };
            input5.MySort(this._sorter).Should().Equal(1, 2, 3, 4, 5, 6, 7);

            var generator = new LinearCongruentialGenerator();
            var randomInput = generator.GenerateInRange(0, 100).Take(500).ToArray();
            randomInput.MySort(this._sorter).Should().BeInAscendingOrder();
        }
    }
}
