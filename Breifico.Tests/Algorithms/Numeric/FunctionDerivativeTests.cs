using System;
using Breifico.Algorithms.Numeric;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Numeric
{
    [TestClass]
    public class FunctionDerivativeTests
    {
        [TestMethod]
        public void GetDerivativeThreePoint_Test()
        {
            var f1 = new FunctionDerivative(x => x * x * x).GetDerivativeThreePoint();
            f1(2).Should().BeApproximately(12.0, 0.001);
            f1(3).Should().BeApproximately(27.0, 0.001);

            var f2 = new FunctionDerivative(Math.Sqrt).GetDerivativeThreePoint();
            f2(4).Should().BeApproximately(0.25, 0.001);
        }

        [TestMethod]
        public void GetDerivativeFivePoint_Test()
        {
            var f1 = new FunctionDerivative(x => x * x * x).GetDerivativeFivePoint();
            f1(2).Should().BeApproximately(12.0, 0.00001);
            f1(3).Should().BeApproximately(27.0, 0.00001);

            var f2 = new FunctionDerivative(Math.Sqrt).GetDerivativeFivePoint();
            f2(4).Should().BeApproximately(0.25, 0.00001);
        }
    }
}
