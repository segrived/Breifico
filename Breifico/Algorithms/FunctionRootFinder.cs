using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Algorithms
{
    public class FunctionRootFinder
    {
        private Func<double, double> _func;

        public FunctionRootFinder(Func<double, double> func) {
            this._func = func;
        }

        public double NewtonRaphsonSolve(double l, double h, double delta = 0.01, int maxSteps = 1000) {
            var xi = l + (h - l) / 2;
            var funcDer = new FunctionDerivative(this._func).GetDerivativeThreePoint();
            while (maxSteps > 0) {
                double funcValue = this._func(xi);
                if (Math.Abs(funcValue) < delta) {
                    return xi;
                }
                xi = xi - funcValue / funcDer(xi);
                maxSteps--;
            }
            return double.NaN;
        }
    }

    [TestClass]
    public class FunctionRootFinderTests
    {
        [TestMethod]
        public void NewtonRaphsonSolve_Test() {
            var f = new FunctionRootFinder(x => x * x - 1);
            f.NewtonRaphsonSolve(0.0, 2.0).Should().BeApproximately(1.0, 0.001);
            var f2 = new FunctionRootFinder(Math.Cos);
            f2.NewtonRaphsonSolve(-2.0, 1.0, 0.0001).Should().BeApproximately(-Math.PI / 2, 0.0001);
            var f3 = new FunctionRootFinder(x => Math.Pow(x, 3) + Math.Pow(x, 2) + 2);
            f3.NewtonRaphsonSolve(-2.0, 2.0, 0.0001).Should().BeApproximately(-1.69562, 0.001);
        }
    }
}