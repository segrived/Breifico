using System;
using Breifico.Algorithms.Numeric;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Numeric
{
    [TestClass]
    public class FunctionRootFinderTests
    {
        [TestMethod]
        public void NewtonRaphsonSolve_Test()
         {
            var f1 = new FunctionRootFinder(x => x * x - 1);
            f1.NewtonRaphsonSolver(0.0, 2.0).Should().BeApproximately(1.0, 0.001);

            var f2 = new FunctionRootFinder(Math.Cos);
            f2.NewtonRaphsonSolver(-2.0, 1.0, 0.0001).Should().BeApproximately(-Math.PI / 2, 0.0001);

            var f3 = new FunctionRootFinder(x => Math.Pow(x, 3) + Math.Pow(x, 2) + 2);
            f3.NewtonRaphsonSolver(-2.0, 2.0, 0.0001).Should().BeApproximately(-1.69562, 0.001);
        }
    }
}