using System;
using System.Numerics;

namespace Breifico.Algorithms.Numeric
{
    /// <summary>
    /// Предоставляет функции для работы с числами Фибонначи
    /// </summary>
    public static class FibonacciNumbers
    {
        /// <summary>
        /// Рекурсивная версия вычисления n-нного числа Фибоначчи
        /// Работает за O(2^n)
        /// </summary>
        /// <param name="n">Порядковый номер числа Фибоначчи</param>
        /// <returns>n-нное число Фибоначчи</returns>
        public static BigInteger GetFibonacciRecursive(int n)
        {
            if (n < 0)
                throw new ArgumentException();

            if (n == 0 || n == 1)
                return n;

            return GetFibonacciRecursive(n - 1) + GetFibonacciRecursive(n - 2);
        }

        /// <summary>
        /// Итеративная версия вычисления n-нного числа Фибоначчи
        /// Работает за O(n)
        /// </summary>
        /// <param name="n">Порядковый номер числа Фибоначчи</param>
        /// <returns>n-нное число Фибоначчи</returns>
        public static BigInteger GetFibonacciIterative(int n)
        {
            if (n < 0)
                throw new ArgumentException();

            if (n == 0 || n == 1)
                return n;

            var arr = new BigInteger[n + 1];
            arr[0] = 0; arr[1] = 1;

            for (int i = 2; i <= n; i++)
                arr[i] = arr[i - 1] + arr[i - 2];

            return arr[n];
        }
    }
}
