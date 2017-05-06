using Breifico.Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Sorting.Implementations
{
    [TestClass]
    public class BubbleSortingTests : GenericSortingTests
    {
        public BubbleSortingTests() : base(new BubbleSorting<int>())
        {
        }
    }
}