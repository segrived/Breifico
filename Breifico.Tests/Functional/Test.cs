using Breifico.Functional;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Functional
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestMethod() {
            var tree = LinkedList.LinkedListNode<int>.NilNode;

            tree = LinkedList.add(12, tree);
            tree = LinkedList.add(14, tree);
            tree = LinkedList.add(16, tree);
        }
    }
}
