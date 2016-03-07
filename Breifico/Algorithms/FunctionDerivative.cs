using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Algorithms
{
    public class FunctionDerivative
    {
        private readonly Func<double, double> _func;

        public FunctionDerivative(Func<double, double> func) {
            this._func = func;
        }

        public Func<double, double> GetDerivativeThreePoint(double h = 0.0001) {
            return x => (this._func(x + h) - this._func(x - h)) / (2 * h);
        }

        public Func<double, double> GetDerivativeFivePoint(double h = 0.0001) {
            return x => {
                double fa = this._func(x - 2 * h) - 8 * this._func(x - h);
                double fb = 8 * this._func(x + h) - this._func(x + 2 * h);
                return (fa + fb) / (12 * h);
            };
        }
    }

    [TestClass]
    public class FunctionDerivativeTests
    {
        [TestMethod]
        public void GetDerivativeThreePoint_Test() {
            var f = new FunctionDerivative(x => x * x * x);
            f.GetDerivativeThreePoint()(2)
                .Should().BeApproximately(12.0, 0.001);
            f.GetDerivativeThreePoint()(3)
                .Should().BeApproximately(27.0, 0.001);

            var f2 = new FunctionDerivative(Math.Sqrt);
            f2.GetDerivativeThreePoint()(4)
                .Should().BeApproximately(0.25, 0.001);
        }

        [TestMethod]
        public void GetDerivativeFivePoint_Test() {
            var f = new FunctionDerivative(x => x * x * x);
            f.GetDerivativeFivePoint()(2)
                .Should().BeApproximately(12.0, 0.00001);
            f.GetDerivativeFivePoint()(3)
                .Should().BeApproximately(27.0, 0.00001);

            var f2 = new FunctionDerivative(Math.Sqrt);
            f2.GetDerivativeFivePoint()(4)
                .Should().BeApproximately(0.25, 0.00001);
        }
    }
}
