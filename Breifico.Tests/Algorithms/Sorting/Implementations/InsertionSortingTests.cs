using Breifico.Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Sorting.Implementations
{
    [TestClass]
    public class InsertionSortingTests : GenericSortingTests
    {
        public InsertionSortingTests() : base(new InsertionSorting<int>())
        {
        }
    }
}