using Breifico.Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Sorting.Implementations
{
    [TestClass]
    public class SelectionSortingTests : GenericSortingTests
    {
        public SelectionSortingTests() : base(new SelectionSorting<int>())
        {
        }
    }
}