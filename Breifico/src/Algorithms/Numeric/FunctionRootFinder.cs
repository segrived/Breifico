using System;

namespace Breifico.Algorithms.Numeric
{
    public class FunctionRootFinder
    {
        private readonly Func<double, double> _func;

        public FunctionRootFinder(Func<double, double> func) {
            this._func = func;
        }

        public double NewtonRaphsonSolve(double l, double h, double delta = 0.01, int maxSteps = 1000) {
            double xi = l + (h - l) / 2;
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
}