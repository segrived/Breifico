using System;
using System.Collections.Generic;

namespace Breifico.Algorithms.Numeric
{
    /// <summary>
    /// Реализация генератора псевдослучайных числе (линейный конгруэнтный метод)
    /// </summary>
    public class LinearCongruentialGenerator
    {
        #region Constants
        private const long A = 1103515245;
        private const long C = 12345;
        private const long M = 2147483648;
        #endregion

        private int _currentState;

        public LinearCongruentialGenerator() :
            this(DateTime.Now.Millisecond) {}

        public LinearCongruentialGenerator(int seed) {
            this._currentState = seed;
        }

        /// <summary>
        /// Возвращает следующее псевдослучайное целое число от 0 до M
        /// </summary>
        /// <returns>Сгенерированное псевдослучайное целое число</returns>
        public int Next() {
            this._currentState = (int)((A * this._currentState + C) % M);
            return this._currentState;
        }

        /// <summary>
        /// Возвращает следующее псевдослучайное целое число в указанных границах
        /// </summary>
        /// <param name="min">Миниально возможное значение</param>
        /// <param name="max">Максимально возможное значение</param>
        /// <returns>Сгенерированное псевдослучайное целое число в указанных границах</returns>
        public int Next(int min, int max) {
            if (min >= max) {
                throw new ArgumentException("Minimum should be greater then maximum");
            }
            double newValue = this.NextDouble();
            double result = newValue * (max - min + 1) + min;
            return (int)result;
        }

        /// <summary>
        /// Возвращает следующее псевдослучайное число с плавающей точкой
        /// </summary>
        /// <returns>Сгенерированное псевдослучайное число с плавающей точкой</returns>
        public double NextDouble() {
            int value = this.Next();
            return value / (double)M;
        }

        /// <summary>
        /// Возвращает следующее псевдослучайное 8-битное число (байт)
        /// </summary>
        /// <returns>Сгенерированное псевдослучайное 8-битное число (байт)</returns>
        public byte NextByte() {
            return (byte)this.Next(0, 255);
        }

        #region Generators
        public IEnumerable<int> Generate() {
            while (true) {
                yield return this.Next();
            }
        }

        public IEnumerable<int> GenerateInRange(int min, int max) {
            while (true) {
                yield return this.Next(min, max);
            }
        }

        public IEnumerable<double> GenerateDoubles() {
            while (true) {
                yield return this.NextDouble();
            }
        }

        public IEnumerable<byte> GenerateBytes() {
            while (true) {
                yield return this.NextByte();
            }
        } 
        #endregion
    }
}