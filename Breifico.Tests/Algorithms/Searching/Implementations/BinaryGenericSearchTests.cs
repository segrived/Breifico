using Breifico.Algorithms.Searching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Searching.Implementations
{
    [TestClass]
    public class BinaryGenericSearchTests : GenericSearchTests
    {
        public BinaryGenericSearchTests() : base(new BinarySearch<int>())
        {
        }
    }
}
