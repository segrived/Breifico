using System;

namespace Breifico.Algorithms.Numeric
{
    /// <summary>
    /// Предоставляет функции для численного интегрирования функций
    /// </summary>
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

        /// <summary>
        /// Возвращает функцию, вычисляющую интеграл в указанном отрезке методом левых прямоугольников
        /// </summary>
        /// <param name="lower">Нижняя граница</param>
        /// <param name="upper">Верхняя граница</param>
        /// <param name="steps">Количество шагов, чем выше - тем точнее результат</param>
        /// <returns>Функция, вычисляющая интеграл в указанном отрезке методом левых прямоугольников</returns>
        public double RectangleIntegration(double lower, double upper, int steps = 500) {
            return this.Integrate(lower, upper, steps, (a, b) => {
                return (b - a) * this._func(a);
            });
        }

        /// <summary>
        /// Возвращает функцию, вычисляющую интеграл в указанном отрезке методом средних прямоугольников
        /// </summary>
        /// <param name="lower">Нижняя граница</param>
        /// <param name="upper">Верхняя граница</param>
        /// <param name="steps">Количество шагов, чем выше - тем точнее результат</param>
        /// <returns>Функция, вычисляющая интеграл в указанном отрезке методом средних прямоугольников</returns>
        public double MidpointIntegration(double lower, double upper, int steps = 500) {
            return this.Integrate(lower, upper, steps, (a, b) => {
                return (b - a) * this._func((a + b) / 2.0);
            });
        }

        /// <summary>
        /// Возвращает функцию, вычисляющую интеграл в указанном отрезке методом трапеций
        /// </summary>
        /// <param name="lower">Нижняя граница</param>
        /// <param name="upper">Верхняя граница</param>
        /// <param name="steps">Количество шагов, чем выше - тем точнее результат</param>
        /// <returns>Функция, вычисляющая интеграл в указанном отрезке методом трапеций</returns>
        public double TrapezoidIntegration(double lower, double upper, int steps = 500) {
            return this.Integrate(lower, upper, steps, (a, b) => {
                return (b - a) * (this._func(a) + this._func(b)) / 2.0;
            });
        }

        /// <summary>
        /// Возвращает функцию, вычисляющую интеграл в указанном отрезке по формуле Симпсона
        /// </summary>
        /// <param name="lower">Нижняя граница</param>
        /// <param name="upper">Верхняя граница</param>
        /// <param name="steps">Количество шагов, чем выше - тем точнее результат</param>
        /// <returns>Функция, вычисляющая интеграл в указанном отрезке по формуле Симпсона</returns>
        public double SimpsonIntegration(double lower, double upper, int steps = 500) {
            return this.Integrate(lower, upper, steps, (a, b) => {
                return (b - a) / 6.0 * (this._func(a) + this._func(b) + 4 * this._func((a + b) / 2.0));
            });
        }
    }
}