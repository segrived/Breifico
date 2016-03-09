using System;
using System.Numerics;
using Breifico.Algorithms.Numeric;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Numeric
{
    [TestClass]
    public class FibonacciNumbersTests
    {
        [TestMethod]
        public void GetFibonacciRecursive_Test() {
            Action f = () => FibonacciNumbers.GetFibonacciRecursive(-1);
            f.ShouldThrow<ArgumentException>();
            FibonacciNumbers.GetFibonacciRecursive(0).Should().Be(0);
            FibonacciNumbers.GetFibonacciRecursive(1).Should().Be(1);
            FibonacciNumbers.GetFibonacciRecursive(2).Should().Be(1);
            FibonacciNumbers.GetFibonacciRecursive(3).Should().Be(2);
            FibonacciNumbers.GetFibonacciRecursive(4).Should().Be(3);
            FibonacciNumbers.GetFibonacciRecursive(5).Should().Be(5);
            FibonacciNumbers.GetFibonacciRecursive(6).Should().Be(8);
            FibonacciNumbers.GetFibonacciRecursive(7).Should().Be(13);
            FibonacciNumbers.GetFibonacciRecursive(8).Should().Be(21);
        }

        [TestMethod]
        public void GetFibonacciIterative_Test() {
            Action f = () => FibonacciNumbers.GetFibonacciIterative(-1);
            f.ShouldThrow<ArgumentException>();
            FibonacciNumbers.GetFibonacciIterative(0).Should().Be(0);
            FibonacciNumbers.GetFibonacciIterative(1).Should().Be(1);
            FibonacciNumbers.GetFibonacciIterative(2).Should().Be(1);
            FibonacciNumbers.GetFibonacciIterative(3).Should().Be(2);
            FibonacciNumbers.GetFibonacciIterative(4).Should().Be(3);
            FibonacciNumbers.GetFibonacciIterative(5).Should().Be(5);
            FibonacciNumbers.GetFibonacciIterative(6).Should().Be(8);
            FibonacciNumbers.GetFibonacciIterative(7).Should().Be(13);
            FibonacciNumbers.GetFibonacciIterative(8).Should().Be(21);
            var n = BigInteger.Parse("7896325826131730509282738943634332893686268675876375");
            FibonacciNumbers.GetFibonacciIterative(250).Should().Be(n);
        }
    }
}
