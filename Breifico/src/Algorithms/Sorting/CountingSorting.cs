using System;

namespace Breifico.Algorithms.Sorting
{
    /// <summary>
    /// Сортировка подсчетом. Работает за O(N+K)
    /// </summary>
    public sealed class CountingSorting : ISorter<uint>
    {
        private const int DefaulMaxElement = 100000;

        private readonly uint _maxElement;

        public CountingSorting() : this(DefaulMaxElement) {}

        public CountingSorting(uint maxElement) {
            this._maxElement = maxElement;
        }

        /// <summary>
        /// Ищет максимальный элемент в массиве. Если элемент превышает 
        /// значение <see cref="_maxElement"/>, функция вернет null
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>Максимальный элемент в массиве или null, если максимальный 
        /// элемент превышает значение <see cref="_maxElement"/></returns>
        private uint? FindMaxElement(uint[] input) {
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

        /// <summary>
        /// Сортирует in-place исходный массив методом подсчета и возвращает его
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>Отсортированный массив</returns>
        public uint[] Sort(uint[] input) {
            if (input.Length <= 1) {
                return input;
            }
            uint? maxElement = this.FindMaxElement(input);
            if (maxElement == null) {
                throw new Exception($"Array contains number which exceeds the limit ({this._maxElement})");
            }
            var outCollection = new int[maxElement.Value + 9];
            foreach (uint s in input) {
                outCollection[s] += 1;
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