using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Breifico.Mathematics
{
    public class MyVector : IReadOnlyCollection<double>
    {
        private double[] Values { get; }

        /// <summary>
        /// Количество элементов в векторе
        /// </summary>
        public int Count => this.Values.Length;

        public MyVector(IEnumerable<double> values) {
            this.Values = values.ToArray();
        }

        public MyVector(params double[] values) {
            this.Values = values;
        }

        public double this[int index]
        {
            get { return this.Values[index]; }
            set { this.Values[index] = value; }
        }

        public static MyVector operator +(MyVector v, double value) {
            return new MyVector(v.Select(i => i + value));
        }

        public static MyVector operator +(MyVector v, MyVector v2) {
            if (v.Count != v2.Count) {
                throw new DifferentSizeException();
            }
            return new MyVector(v.Zip(v2, (a, b) => a + b));
        }

        public static MyVector operator *(MyVector v, double value) {
            return new MyVector(v.Select(i => i * value));
        }

        public static MyVector operator -(MyVector v, double value) {
            return new MyVector(v.Select(i => i - value));
        }

        public static MyVector operator /(MyVector v, double value) {
            return new MyVector(v.Select(i => i / value));
        }

        public void NegativeAll() {
            for (int i = 0; i < this.Count; i++) {
                this[i] = -this[i];
            }
        }

        /// <summary>
        /// Возвращает сумму всех элементов коллекции
        /// </summary>
        /// <returns>Сумма всех элементов коллекции</returns>
        public double GetSum() {
            double sum = 0.0;
            for (int i = 0; i < this.Count; i++) {
                sum += this[i];
            }
            return sum;
        }

        /// <summary>
        /// Возвращает среднее арифметическое вектора
        /// </summary>
        /// <returns>Среднее арифметическое вектора</returns>
        public double GetArithmeticMean() {
            if (this.Count == 0) {
                return double.NaN;
            }
            return this.GetSum() / this.Count;
        }

        /// <summary>
        /// Возвращает среднее геометрическое вектора
        /// </summary>
        /// <returns>Среднее геометрическое вектора</returns>
        public double GetGeometricMean() {
            if (this.Count == 0) {
                return double.NaN;
            }
            double mul = 1.0;
            for (int i = 0; i < this.Count; i++) {
                mul *= this[i];
            }
            return Math.Pow(mul, 1.0 / this.Count);
        }

        /// <summary>
        /// Возвращает среднее гармническое вектора
        /// </summary>
        /// <returns>Среднее гармническое вектора</returns>
        public double GetHarmonicMean() {
            if (this.Count == 0) {
                return double.NaN;
            }
            double sum = 0.0;
            for (int i = 0; i < this.Count; i++) {
                sum += 1.0 / this[i];
            }
            return this.Count / sum;
        }

        /// <summary>
        /// Возвращает дисперсию элементов вектора
        /// </summary>
        /// <returns>Дисперсия элементов вектора</returns>
        public double GetVariance() {
            double sum = 0.0;
            double avg = this.GetArithmeticMean();
            for (int i = 0; i < this.Count; i++) {
                sum += Math.Pow(this[i] - avg, 2.0);
            }
            return sum / (this.Count - 1);
        }

        /// <summary>
        /// Возвращает среднеквадратическое отклонение элементов вектора
        /// </summary>
        /// <returns>Среднеквадратическое отклонение элементов вектора</returns>
        public double GetStandardDeviation() => Math.Sqrt(this.GetVariance());

        public IEnumerator<double> GetEnumerator() {
            return ((IEnumerable<double>)this.Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }
}
