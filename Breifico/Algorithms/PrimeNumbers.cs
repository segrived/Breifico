using System;
using System.Numerics;
using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Algorithms
{
    public static class PrimeNumbers
    {
        public static bool IsPrime(long number) {
            if (number == 0 || number == 1 || number == 2) {
                return true;
            }
            if (number % 2 == 0) {
                return false;
            }
            int maxValue = (int)Math.Sqrt(number);
            for (int i = 3; i <= maxValue; i += 2) {
                if (number % i == 0) {
                    return false;
                }
            }
            return true;
        }

        public static bool IsProbablyPrimeFermat(BigInteger p, int tests = 200) {
            if (p == 0 || p == 1 || p == 2) {
                return true;
            }
            var randonGen = new LinearCongruentialGenerator();
            for(int i = 0; i < tests; i++) {
                int n = randonGen.Next(1, Int32.MaxValue);
                if (BigInteger.ModPow(n, p - 1, p) != 1) {
                    return false;
                }
            }
            return true;
        }

        public static MyList<long> GetPrimesBruteforce(long to) {
            return GetPrimesBruteforce(2, to);
        }

        public static MyList<long> GetPrimesBruteforce(long from, long to) {
            var primes = new MyList<long>();
            for (long i = from; i <= to; i++) {
                if (IsPrime(i)) {
                    primes.Add(i);
                }
            }
            return primes;
        }

        public static MyList<long> GetPrimes(long max) {
            var sieve = new bool[max + 1];
            long boundary = (long)Math.Ceiling(Math.Sqrt(max)) + 1;
            for (long i = 2; i <= boundary; i++) {
                long p = i * 2;
                while (p <= max) {
                    sieve[p] = true;
                    p += i;
                }
            }
            var output = new MyList<long>();
            for (long i = 2; i < sieve.LongLength; i++) {
                if (!sieve[i]) {
                    output.Add(i);
                }
            }
            return output;
        }
    }

    [TestClass]
    public class PrimeNumbersTests
    {
        [TestMethod]
        public void IsPrime_ShouldReturnTreeIfPrime() {
            PrimeNumbers.IsPrime(0).Should().BeTrue();
            PrimeNumbers.IsPrime(1).Should().BeTrue();
            PrimeNumbers.IsPrime(2).Should().BeTrue();
            PrimeNumbers.IsPrime(7).Should().BeTrue();
            PrimeNumbers.IsPrime(12).Should().BeFalse();
            PrimeNumbers.IsPrime(13).Should().BeTrue();
            PrimeNumbers.IsPrime(14).Should().BeFalse();
            PrimeNumbers.IsPrime(15).Should().BeFalse();
            PrimeNumbers.IsPrime(1763).Should().BeFalse();
            PrimeNumbers.IsPrime(7400854980481283).Should().BeFalse();
        }

        [TestMethod]
        public void IsProbablyPrimeFermat() {
            PrimeNumbers.IsProbablyPrimeFermat(BigInteger.Parse("247899471379052853718361744194218517777"))
                .Should().BeTrue();
            PrimeNumbers.IsProbablyPrimeFermat(BigInteger.Parse("11765973102324847295134644699013290089"))
                .Should().BeTrue();
            PrimeNumbers.IsProbablyPrimeFermat(BigInteger.Parse("302437715042644897617455869893451911197"))
                .Should().BeTrue();
            PrimeNumbers.IsProbablyPrimeFermat(BigInteger.Parse("194937212581051615743485406925431679543"))
                .Should().BeTrue();
            PrimeNumbers.IsProbablyPrimeFermat(BigInteger.Parse("95806547455429676690768173487414793227"))
                .Should().BeTrue();

            PrimeNumbers.IsProbablyPrimeFermat(BigInteger.Parse("247899471379052853718361744194218517778"))
                .Should().BeFalse();
            PrimeNumbers.IsProbablyPrimeFermat(BigInteger.Parse("8888888888888888888888"))
                .Should().BeFalse();
            PrimeNumbers.IsProbablyPrimeFermat(9).Should().BeFalse();
            PrimeNumbers.IsProbablyPrimeFermat(6).Should().BeFalse();
            PrimeNumbers.IsProbablyPrimeFermat(8).Should().BeFalse();
            PrimeNumbers.IsProbablyPrimeFermat(12).Should().BeFalse();
            // special cases
            PrimeNumbers.IsProbablyPrimeFermat(0).Should().BeTrue();
            PrimeNumbers.IsProbablyPrimeFermat(1).Should().BeTrue();
            PrimeNumbers.IsProbablyPrimeFermat(2).Should().BeTrue();
        }

        [TestMethod]
        public void GetPrimesBruteforce() {
            PrimeNumbers.GetPrimesBruteforce(50).Should().Equal(
                2, 3, 5, 7, 11, 13, 17, 
                19, 23, 29, 31, 37, 41, 43, 47);
            PrimeNumbers.GetPrimesBruteforce(5, 19).Should().Equal(5, 7, 11, 13, 17, 19);
            PrimeNumbers.GetPrimesBruteforce(480, 486).Should().BeEmpty();
        }

        [TestMethod]
        public void GetPrimes() {
            PrimeNumbers.GetPrimes(0).Should().BeEmpty();
            PrimeNumbers.GetPrimes(1).Should().BeEmpty();
            PrimeNumbers.GetPrimes(2).Should().Equal(2);
            PrimeNumbers.GetPrimes(3).Should().Equal(2, 3);
            PrimeNumbers.GetPrimes(4).Should().Equal(2, 3);
            PrimeNumbers.GetPrimes(5).Should().Equal(2, 3, 5);
            PrimeNumbers.GetPrimes(6).Should().Equal(2, 3, 5);
            PrimeNumbers.GetPrimes(7).Should().Equal(2, 3, 5, 7);
            PrimeNumbers.GetPrimes(50).Should().Equal(
                2, 3, 5, 7, 11, 13, 17,
                19, 23, 29, 31, 37, 41, 43, 47);
        }
    }
}
