using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Algorithms
{
    public class FunctionIntegration
    {
        private readonly Func<double, double> _func;

        public FunctionIntegration(Func<double, double> func) {
            this._func = func;
        }

        private double Integrate(double lower, double upper, int steps, Func<double, double, double> f) {

            double dx = (upper - lower) / steps;
            double totalArea = 0.0;
            double x = lower;
            for (int i = 0; i < steps; i++) {
                totalArea += f(x, x + dx);
                x += dx;
            }
            return totalArea;
        }

        public double RectangleIntegration(double lower, double upper, int steps = 500) {
            return this.Integrate(lower, upper, steps, (a, b) => {
                return (b - a) * this._func(a);
            });
        }

        public double MidpointIntegration(double lower, double upper, int steps = 500) {
            return this.Integrate(lower, upper, steps, (a, b) => {
                return (b - a) * this._func((a + b) / 2.0);
            });
        }

        public double TrapezoidIntegration(double lower, double upper, int steps = 500) {
            return this.Integrate(lower, upper, steps, (a, b) => {
                return (b - a) * (this._func(a) + this._func(b)) / 2.0;
            });
        }


        public double SimpsonIntegration(double lower, double upper, int steps = 500) {
            return this.Integrate(lower, upper, steps, (a, b) => {
                return (b - a) / 6.0 * (this._func(a) + this._func(b) + 4 * this._func((a + b) / 2.0));
            });
        }
    }

    [TestClass]
    public class FunctionIntegrationTests
    {
        [TestMethod]
        public void RectangleIntegration_Test() {
            var f = new FunctionIntegration(x => x * x);
            f.RectangleIntegration(0.0, 2.0, 5000)
                .Should().BeApproximately(2.6666666, 0.01);
            f.RectangleIntegration(2.0, 5.0, 5000)
                .Should().BeApproximately(39, 0.01);

            var f2 = new FunctionIntegration(Math.Sqrt);
            f2.RectangleIntegration(0.0, 10.0, 5000)
                .Should().BeApproximately(21.0818510677, 0.01);
            f2.RectangleIntegration(2.0, 2.5, 5000)
                .Should().BeApproximately(0.7496133, 0.01);
        }

        [TestMethod]
        public void MidpointIntegration_Test() {
            var f = new FunctionIntegration(x => x * x);
            f.MidpointIntegration(0.0, 2.0, 5000)
                .Should().BeApproximately(2.6666666, 0.01);
            f.MidpointIntegration(2.0, 5.0, 5000)
                .Should().BeApproximately(39, 0.01);

            var f2 = new FunctionIntegration(Math.Sqrt);
            f2.MidpointIntegration(0.0, 10.0, 5000)
                .Should().BeApproximately(21.0818510677, 0.01);
            f2.MidpointIntegration(2.0, 2.5, 5000)
                .Should().BeApproximately(0.7496133, 0.01);
        }

        [TestMethod]
        public void TrapezoidIntegration_Test() {
            var f = new FunctionIntegration(x => x * x);
            f.TrapezoidIntegration(0.0, 2.0, 5000)
                .Should().BeApproximately(2.6666666, 0.01);
            f.TrapezoidIntegration(2.0, 5.0, 5000)
                .Should().BeApproximately(39, 0.01);

            var f2 = new FunctionIntegration(Math.Sqrt);
            f2.TrapezoidIntegration(0.0, 10.0, 5000)
                .Should().BeApproximately(21.0818510677, 0.01);
            f2.TrapezoidIntegration(2.0, 2.5, 5000)
                .Should().BeApproximately(0.7496133, 0.01);
        }

        [TestMethod]
        public void SimpsonIntegration_Test() {
            var f = new FunctionIntegration(x => x * x);
            f.SimpsonIntegration(0.0, 2.0, 5000)
                .Should().BeApproximately(2.6666666, 0.01);
            f.SimpsonIntegration(2.0, 5.0, 5000)
                .Should().BeApproximately(39, 0.01);

            var f2 = new FunctionIntegration(Math.Sqrt);
            f2.SimpsonIntegration(0.0, 10.0, 5000)
                .Should().BeApproximately(21.0818510677, 0.01);
            f2.SimpsonIntegration(2.0, 2.5, 5000)
                .Should().BeApproximately(0.7496133, 0.01);
        }
    }
}
