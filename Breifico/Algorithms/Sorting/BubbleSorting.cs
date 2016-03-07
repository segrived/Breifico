using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Algorithms.Sorting
{
    public class BubbleSorting<T> : ISorter<T> where T : IComparable<T>
    {
        public T[] Sort(T[] input) {
            for (int i = 1; i < input.Length; i++) {
                bool wasChanged = false;
                for (int j = 0; j < input.Length; j++) {
                    if (input[i].CompareTo(input[j]) == 1) {
                        continue;
                    }
                    var tmp = input[i];
                    input[i] = input[j];
                    input[j] = tmp;
                    wasChanged = true;
                }
                if (!wasChanged) {
                    break;
                }
            }
            return input;
        }
    }

    [TestClass]
    public class BubbleSortingTests
    {
        [TestMethod]
        public void BubbleSorting_Sort_WhenArrayIsEmpty_ShouldBeEmpty() {
            var input = new int[] { };
            input.MySort(new BubbleSorting<int>()).Should().BeEmpty();
        }

        [TestMethod]
        public void BubbleSorting_Sort_WhenOneElementInArray() {
            var input = new[] { 12 };
            input.MySort(new BubbleSorting<int>()).Should().Equal(12);
        }

        [TestMethod]
        public void BubbleSorting_Sort_WhenMultipleElementsInArray_ShouldSort() {
            var input = new[] { 1, 2 };
            input.MySort(new BubbleSorting<int>()).Should().Equal(1, 2);

            var input2 = new[] { 2, 1 };
            input2.MySort(new BubbleSorting<int>()).Should().Equal(1, 2);

            var input3 = new[] { 9, 5, 4, 3 };
            input3.MySort(new BubbleSorting<int>()).Should().Equal(3, 4, 5, 9);

            var input4 = new[] { 5, 9, 5, 9, 2, 1, 2, 6, 5, 4, 7 };
            input4.MySort(new BubbleSorting<int>()).Should().Equal(1, 2, 2, 4, 5, 5, 5, 6, 7, 9, 9);

            var input5 = new[] { 1, 2, 3, 4, 5, 6, 7 };
            input5.MySort(new BubbleSorting<int>()).Should().Equal(1, 2, 3, 4, 5, 6, 7);
        }
    }
}
