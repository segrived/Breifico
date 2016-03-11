using System;

namespace Breifico.Algorithms.Sorting
{
    public class CountingSorting : ISorter<uint>
    {
        private const int DefaulMaxElement = 100000;

        private readonly uint _maxElement;

        public CountingSorting() : this(DefaulMaxElement) {}

        public CountingSorting(uint maxElement) {
            this._maxElement = maxElement;
        }

        /// <summary>
        /// Ищет максимальный элемент в массиве. Если элемент превышает 
        /// значение MaxElement, функция вернет null
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>Максимальный элемент в массиве</returns>
        private uint? FindMax(uint[] input) {
            if (input.Length == 0) {
                throw new ArgumentException("Empty array, can't find maximum element");
            }
            uint maxElement = input[0];
            for (int i = 1; i < input.Length; i++) {
                if (input[i] <= maxElement) {
                    continue;
                }
                if (input[i] > this._maxElement) {
                    return null;
                }
                maxElement = input[i];
            }
            return maxElement;
        }

        public uint[] Sort(uint[] input) {
            if (input.Length <= 1) {
                return input;
            }
            uint? maxElement = this.FindMax(input);
            if (maxElement == null) {
                throw new Exception($"Array contains number which exceeds the limit ({this._maxElement})");
            }
            var outCollection = new int[maxElement.Value + 9];
            for (int i = 0; i < input.Length; i++) {
                outCollection[input[i]] += 1;
            }
            int outIndex = 0;
            for (uint i = 0; i < outCollection.Length; i++) {
                if (outCollection[i] != 0) {
                    for (int j = 0; j < outCollection[i]; j++) {
                        input[outIndex++] = i;
                    }
                }
            }
            return input;
        }
    }
}