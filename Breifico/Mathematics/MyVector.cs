using System;
using System.Collections.Generic;
using System.Linq;
using Breifico.Algorithms.Sorting;

namespace Breifico.Mathematics
{
    public class MyVector
    {
        public double[] Values { get; }

        public MyVector(IEnumerable<double> values) {
            this.Values = values.ToArray();
        }

        public MyVector(params double[] values) {
            this.Values = values;
        }

        public static MyVector operator +(MyVector v, double value) {
            return new MyVector(v.Values.Select(i => i + value));
        }

        public static MyVector operator *(MyVector v, double value) {
            return new MyVector(v.Values.Select(i => i * value));
        }

        public static MyVector operator -(MyVector v, double value) {
            return new MyVector(v.Values.Select(i => i - value));
        }

        public static MyVector operator /(MyVector v, double value) {
            return new MyVector(v.Values.Select(i => i / value));
        }

        public void Negative() {
            for (int i = 0; i < this.Values.Length; i++) {
                this.Values[i] = -this.Values[i];
            }
        }

        public double Sum() {
            double sum = 0.0;
            for (int i = 0; i < this.Values.Length; i++) {
                sum += this.Values[i];
            }
            return sum;
        }

        /// <summary>
        /// Возвращает среднее арифметическое вектора
        /// </summary>
        /// <returns>Среднее арифметическое вектора</returns>
        public double ArithmeticMean() {
            if (this.Values.Length == 0) {
                return double.NaN;
            }
            return this.Sum() / this.Values.Length;
        }

        /// <summary>
        /// Возвращает среднее геометрическое вектора
        /// </summary>
        /// <returns>Среднее геометрическое вектора</returns>
        public double GeometricMean() {
            if (this.Values.Length == 0) {
                return double.NaN;
            }
            double mul = 1.0;
            for (int i = 0; i < this.Values.Length; i++) {
                mul *= this.Values[i];
            }
            return Math.Pow(mul, 1.0 / this.Values.Length);
        }

        /// <summary>
        /// Возвращает среднее гармническое вектора
        /// </summary>
        /// <returns>Среднее гармническое вектора</returns>
        public double HarmonicMean() {
            if (this.Values.Length == 0) {
                return double.NaN;
            }
            double sum = 0.0;
            for (int i = 0; i < this.Values.Length; i++) {
                sum += 1.0 / this.Values[i];
            }
            return this.Values.Length / sum;
        }
    }
}
