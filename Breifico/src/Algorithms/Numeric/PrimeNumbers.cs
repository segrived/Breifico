using System;
using System.Linq;
using System.Numerics;
using Breifico.DataStructures;

namespace Breifico.Algorithms.Numeric
{
    /// <summary>
    /// Предоставляет функции для работы с простыми числами
    /// </summary>
    public static class PrimeNumbers
    {
        /// <summary>
        /// Проверяет, является ли числом простым (методом перебора)
        /// </summary>
        /// <param name="number">Проверяемое на простоту число</param>
        /// <returns>True - если число простое, иначе - False</returns>
        public static bool IsPrime(long number)
        {
            if (number == 0 || number == 1 || number == 2)
                return true;

            if (number % 2 == 0)
                return false;

            int maxValue = (int)Math.Sqrt(number);

            for (int i = 3; i <= maxValue; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        public static bool IsProbablyPrimeFermat(BigInteger p, int tests = 200)
        {
            if (p == 0 || p == 1 || p == 2)
                return true;

            var randonGen = new LinearCongruentialGenerator();
            for (int i = 0; i < tests; i++)
            {
                int n = randonGen.Next(1, Int32.MaxValue);
                if (BigInteger.ModPow(n, p - 1, p) != 1)
                    return false;
            }
            return true;
        }

        public static MyList<long> GetPrimesBruteforce(long to) => GetPrimesBruteforce(2, to);

        /// <summary>
        /// Возвращает все простые числа в указанных границах (методом перебора)
        /// </summary>
        /// <param name="from">Нижняя граница</param>
        /// <param name="to">Верхняя граница</param>
        /// <returns>Список с простыми числами в указанных границах</returns>
        public static MyList<long> GetPrimesBruteforce(long from, long to)
        {
            var primes = new MyList<long>();

            for (long i = from; i <= to; i++)
                if (IsPrime(i))
                    primes.Add(i);

            return primes;
        }

        /// <summary>
        /// Возвращает все простые числа, начиная с 2 до указанного числа
        /// (с помощью алгоритма решета Эратосфена)
        /// </summary>
        /// <param name="max">Верхняя граница</param>
        /// <returns>Список простых числе с 2 до верхней границы</returns>
        public static MyList<long> GetPrimes(long max)
        {
            var sieve = new bool[max + 1];
            long boundary = (long)Math.Ceiling(Math.Sqrt(max)) + 1;
            for (long i = 2; i <= boundary; i++)
            {
                long p = i * 2;
                while (p <= max) {
                    sieve[p] = true;
                    p += i;
                }
            }

            var output = new MyList<long>();

            for (long i = 2; i < sieve.LongLength; i++)
                if (!sieve[i])
                    output.Add(i);

            return output;
        }
    }
}
