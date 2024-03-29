﻿using System;

namespace Breifico.Algorithms.Numeric
{
    /// <summary>
    /// Предоставляет функции для численного интегрирования функций
    /// </summary>
    public sealed class FunctionIntegration
    {
        private const int DEFAULT_STEPS = 500;

        private readonly Func<double, double> _func;

        public FunctionIntegration(Func<double, double> func)
        {
            this._func = func;
        }

        private delegate double IntegrateFunction(double a, double b);

        private double IntegrateInternal(double lower, double upper, int steps, IntegrateFunction f)
        {
            double dx = (upper - lower) / steps;
            double totalArea = 0.0;
            double x = lower;

            for (int i = 0; i < steps; i++)
            {
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
        public double RectangleIntegration(double lower, double upper, int steps = DEFAULT_STEPS) 
            => this.IntegrateInternal(lower, upper, steps, (a, b) => (b - a) * this._func(a));

        /// <summary>
        /// Возвращает функцию, вычисляющую интеграл в указанном отрезке методом средних прямоугольников
        /// </summary>
        /// <param name="lower">Нижняя граница</param>
        /// <param name="upper">Верхняя граница</param>
        /// <param name="steps">Количество шагов, чем выше - тем точнее результат</param>
        /// <returns>Функция, вычисляющая интеграл в указанном отрезке методом средних прямоугольников</returns>
        public double MidpointIntegration(double lower, double upper, int steps = DEFAULT_STEPS) 
            => this.IntegrateInternal(lower, upper, steps, (a, b) => (b - a) * this._func((a + b) / 2.0));

        /// <summary>
        /// Возвращает функцию, вычисляющую интеграл в указанном отрезке методом трапеций
        /// </summary>
        /// <param name="lower">Нижняя граница</param>
        /// <param name="upper">Верхняя граница</param>
        /// <param name="steps">Количество шагов, чем выше - тем точнее результат</param>
        /// <returns>Функция, вычисляющая интеграл в указанном отрезке методом трапеций</returns>
        public double TrapezoidIntegration(double lower, double upper, int steps = DEFAULT_STEPS) 
            => this.IntegrateInternal(lower, upper, steps, (a, b) => (b - a) * (this._func(a) + this._func(b)) / 2.0);

        /// <summary>
        /// Возвращает функцию, вычисляющую интеграл в указанном отрезке по формуле Симпсона
        /// </summary>
        /// <param name="lower">Нижняя граница</param>
        /// <param name="upper">Верхняя граница</param>
        /// <param name="steps">Количество шагов, чем выше - тем точнее результат</param>
        /// <returns>Функция, вычисляющая интеграл в указанном отрезке по формуле Симпсона</returns>
        public double SimpsonIntegration(double lower, double upper, int steps = DEFAULT_STEPS)
        {
            double IntegrateFunction(double a, double b) 
                => (b - a) / 6.0 * (this._func(a) + this._func(b) + 4 * this._func((a + b) / 2.0));
            return this.IntegrateInternal(lower, upper, steps, IntegrateFunction);
        }
    }
}