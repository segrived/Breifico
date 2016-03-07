using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Algorithms.Sorting
{
    class InsertionSorting<T> : ISorter<T> where T : IComparable<T>
    {
        public T[] Sort(T[] input) {
            for (int i = 1; i < input.Length; i++) {
                var item = input[i];
                int insertIndex = 0;
                for (int j = insertIndex; j <= i; j++) {
                    if (item.CompareTo(input[j]) > 0) {
                        continue;
                    }
                    insertIndex = j;
                    break;
                }
                for (int k = i; k > insertIndex; k--) {
                    input[k] = input[k - 1];
                }
                input[insertIndex] = item;
            }
            return input;
        }
    }

    [TestClass]
    public class InsertionSortingTests
    {
        [TestMethod]
        public void InsertionSorting_Sort_WhenArrayIsEmpty_ShouldBeEmpty() {
            var input = new int[] {};
            input.MySort(new SelectionSorting<int>()).Should().BeEmpty();
        }

        [TestMethod]
        public void InsertionSorting_Sort_WhenOneElementInArray() {
            var input = new[] {12};
            input.MySort(new SelectionSorting<int>()).Should().Equal(12);
        }

        [TestMethod]
        public void InsertionSorting_Sort_WhenMultipleElementsInArray_ShouldSort() {
            var input = new[] {1, 2};
            input.MySort(new InsertionSorting<int>()).Should().Equal(1, 2);

            var input2 = new[] {2, 1};
            input2.MySort(new InsertionSorting<int>()).Should().Equal(1, 2);

            var input3 = new[] {9, 5, 4, 3};
            input3.MySort(new InsertionSorting<int>()).Should().Equal(3, 4, 5, 9);

            var input4 = new[] {5, 9, 5, 9, 2, 1, 2, 6, 5, 4, 7};
            input4.MySort(new InsertionSorting<int>()).Should().Equal(1, 2, 2, 4, 5, 5, 5, 6, 7, 9, 9);

            var input5 = new[] {1, 2, 3, 4, 5, 6, 7};
            input5.MySort(new InsertionSorting<int>()).Should().Equal(1, 2, 3, 4, 5, 6, 7);
        }
    }
}