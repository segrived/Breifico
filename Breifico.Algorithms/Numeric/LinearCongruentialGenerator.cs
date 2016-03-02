using System;
using System.Collections.Generic;

namespace Breifico.Algorithms.Numeric
{
    public class LinearCongruentialGenerator
    {
        private const long A = 1103515245;
        private const long C = 12345;
        private const long M = 2147483648;

        private long _currentState;

        public LinearCongruentialGenerator() : 
            this(DateTime.Now.Ticks) {}

        public LinearCongruentialGenerator(long seed) {
            this._currentState = seed;
        }

        public int Next() {
            this._currentState = (A * this._currentState + C) % M;
            return (int)this._currentState;
        }

        public int Next(int min, int max) {
            if (min >= max) {
                throw new ArgumentException("Minimum should be greater then maximum");
            }
            double newValue = this.NextDouble();
            double result = newValue * (max - min + 1) + min;
            return (int)result;
        }

        public double NextDouble() {
            int value = this.Next();
            return value / (double)M;
        }

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
    }
}
