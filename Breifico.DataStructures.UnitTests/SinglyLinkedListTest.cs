using System;
using Breifico.DataStructures.Interfaces;
using Breifico.DataStructures.UnitTests.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.DataStructures.UnitTests
{
    [TestClass]
    public class SinglyLinkedListTest
    {
        private static ILinkedList<int> GetI() => new SinglyLinkedList<int>();

        #region Shared methods
        [TestMethod]
        public void NewInstance_ShouldHasZeroElements() =>
            LinkedListTestHelpers.NewInstance_ShouldHasZeroElements(GetI());

        [TestMethod]
        public void Add_ShouldAddElements() =>
            LinkedListTestHelpers.Add_ShouldAddElements(GetI());

        [TestMethod]
        public void Insert_ShouldInsertElement() =>
            LinkedListTestHelpers.Insert_ShouldInsertElement(GetI());

        [TestMethod]
        public void Insert_OutOfRangeIndex_ShouldThrowException() =>
            LinkedListTestHelpers.Insert_OutOfRangeIndex_ShouldThrowException(GetI());

        [TestMethod]
        public void Insert_ShouldInsertCorrectlyInEmptyList() =>
            LinkedListTestHelpers.Insert_ShouldInsertCorrectlyInEmptyList(GetI());

        [TestMethod]
        public void Remove_OutOfRangeIndex_ShouldThrowException() =>
            LinkedListTestHelpers.Remove_OutOfRangeIndex_ShouldThrowException(GetI());

        [TestMethod]
        public void Remove_ShouldRemoveElements() =>
            LinkedListTestHelpers.Remove_ShouldRemoveElements(GetI());

        [TestMethod]
        public void Clear_ShouldRemoveAllElements() =>
            LinkedListTestHelpers.Clear_ShouldRemoveAllElements(GetI());

        [TestMethod]
        public void Get_OutOfRangeIndex_ShouldThrowException() =>
            LinkedListTestHelpers.Get_OutOfRangeIndex_ShouldThrowException(GetI());

        [TestMethod]
        public void Get_ShouldThrowExceptionOnEmptyList() =>
            LinkedListTestHelpers.Get_ShouldThrowExceptionOnEmptyList(GetI());

        [TestMethod]
        public void Get_ShouldReturnElementByIndex() =>
            LinkedListTestHelpers.Get_ShouldReturnElementByIndex(GetI());

        [TestMethod]
        public void ToString_Test() =>
            LinkedListTestHelpers.ToString_Test(GetI());

        [TestMethod]
        public void ToString_ShouldCorrectlyDisplayNullValues() =>
            LinkedListTestHelpers.ToString_ShouldCorrectlyDisplayNullValues(GetI());

        [TestMethod]
        public void Reverse_ShouldReverseList() =>
            IReversibleTestHelpers.Reverse_ShouldReverseList(GetI() as IReversible<int>);

        [TestMethod]
        public void Reverse_ShouldCorrectlyProcessEmptyList() =>
            IReversibleTestHelpers.Reverse_ShouldCorrectlyProcessEmptyList(GetI() as IReversible<int>);
        #endregion
    }
}