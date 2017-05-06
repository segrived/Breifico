using System;
using System.Collections.Generic;
using System.Linq;

namespace Breifico.Algorithms.Sorting
{
    /// <summary>
    /// Сортировка подсчетом. Работает за O(N+K)
    /// </summary>
    public sealed class CountingSorting : ISorter<int>
    {
        private const int DEFAULT_MAX_ELEMENT = 100000;

        private readonly uint _maxElement;

        public CountingSorting() : this(DEFAULT_MAX_ELEMENT) {}

        public CountingSorting(uint maxElement)
        {
            this._maxElement = maxElement;
        }

        /// <summary>
        /// Сортирует in-place исходный массив методом подсчета и возвращает его
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>Отсортированный массив</returns>
        public int[] Sort(int[] input)
        {
            if (input.Length <= 1)
                return input;

            var maxElement = this.FindMaxElement(input);

            if (maxElement == null)
                throw new Exception("Array contains negative number or number which exceeds the limit");

            var outCollection = new int[maxElement.Value + 9];

            foreach (int s in input)
                outCollection[s] += 1;

            int outIndex = 0;

            for (int i = 0; i < outCollection.Length; i++)
            {
                if (outCollection[i] == 0)
                    continue;

                for (int j = 0; j < outCollection[i]; j++)
                    input[outIndex++] = i;
            }
            return input;
        }

        #region Utils

        /// <summary>
        /// Ищет максимальный элемент в массиве. Если элемент меньше нуля или превышает
        /// значение <see cref="_maxElement"/>, функция вернет null
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <returns>
        /// Максимальный элемент в массиве или null, если один из элементов
        /// равен нулю или превышает значение <see cref="_maxElement"/>
        /// </returns>
        private int? FindMaxElement(IReadOnlyList<int> input)
        {
            int maxElement = input[0];
            for (int i = 1; i < input.Count; i++)
            {
                if (input[i] <= maxElement)
                    continue;

                if (input[i] < 0 || input[i] > this._maxElement)
                    return null;

                maxElement = input[i];
            }
            return maxElement;
        }
        
        #endregion
    }
}