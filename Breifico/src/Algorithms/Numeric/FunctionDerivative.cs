using System;

namespace Breifico.Algorithms.Numeric
{
    /// <summary>
    /// Предоставляет функции для численного дифференцирования функций
    /// </summary>
    public class FunctionDerivative
    {
        private readonly Func<double, double> _func;

        public FunctionDerivative(Func<double, double> func) {
            this._func = func;
        }

        /// <summary>
        /// Возвращает функцию, которая вычисляет первую производную для указанного значения x
        /// Symmetric difference quotient
        /// </summary>
        /// <param name="h">Изменение x</param>
        /// <returns>Функция, которая вычисляет первую производную для указанного значения x</returns>
        public Func<double, double> GetDerivativeThreePoint(double h = 0.0001) {
            return x => (this._func(x + h) - this._func(x - h)) / (2 * h);
        }

        /// <summary>
        /// Возвращает функцию, которая вычисляет первую производную для указанного значения x
        /// </summary>
        /// <param name="h">Изменение x</param>
        /// <returns>Функция, которая вычисляет первую производную для указанного значения x</returns>
        public Func<double, double> GetDerivativeFivePoint(double h = 0.0001) {
            return x => {
                double fa = this._func(x - 2 * h) - 8 * this._func(x - h);
                double fb = 8 * this._func(x + h) - this._func(x + 2 * h);
                return (fa + fb) / (12 * h);
            };
        }
    }
}
