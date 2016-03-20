using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
    [TestClass]
    public class MyPriorityQueueTests
    {
        [TestMethod]
        public void Test1() {
            var x = new MyPriorityQueue<int>();
            x.Enqueue(122, 7);
            x.Enqueue(342, 4);
            x.Enqueue(234, 8);
            x.Enqueue(6754, 11);
            x.Enqueue(2334, 2);
            x.Enqueue(546, 6);

            x.Dequeue().Should().Be(6754);
            x.Dequeue().Should().Be(234);
            x.Dequeue().Should().Be(122);
            x.Dequeue().Should().Be(546);
            x.Dequeue().Should().Be(342);
            x.Dequeue().Should().Be(2334);
        }
    }
}
