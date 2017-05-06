using Breifico.Algorithms.Searching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Searching.Implementations
{
    [TestClass]
    public class LinearGenericSearchTests : GenericSearchTests
    {
        public LinearGenericSearchTests() : base(new LinearSearch<int>())
        {
        }
    }
}
