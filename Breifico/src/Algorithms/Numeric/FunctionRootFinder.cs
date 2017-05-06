using System;

namespace Breifico.Algorithms.Numeric
{
    public sealed class FunctionRootFinder
    {
        private readonly Func<double, double> _func;
        private readonly Func<double, double> _derivativeFunc;

        public FunctionRootFinder(Func<double, double> func)
        {
            this._func = func;
            this._derivativeFunc = new FunctionDerivative(this._func).GetDerivativeThreePoint();
        }

        public double NewtonRaphsonSolver(double l, double h, double delta = 0.01, int maxSteps = 1000)
        {
            double xi = l + (h - l) / 2;
            while (maxSteps > 0)
            {
                double funcValue = this._func(xi);
                if (Math.Abs(funcValue) < delta)
                    return xi;

                xi = xi - funcValue / this._derivativeFunc(xi);
                maxSteps--;
            }
            return double.NaN;
        }
    }
}