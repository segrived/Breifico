using System.Numerics;
using Breifico.Algorithms.Numeric;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Numeric
{
    [TestClass]
    public class PrimeNumbersTests
    {
        [TestMethod]
        public void IsPrime_ShouldReturnTreeIfPrime()
        {
            PrimeNumbers.IsPrime(0).Should().BeTrue();
            PrimeNumbers.IsPrime(1).Should().BeTrue();
            PrimeNumbers.IsPrime(2).Should().BeTrue();
            PrimeNumbers.IsPrime(7).Should().BeTrue();
            PrimeNumbers.IsPrime(12).Should().BeFalse();
            PrimeNumbers.IsPrime(13).Should().BeTrue();
            PrimeNumbers.IsPrime(14).Should().BeFalse();
            PrimeNumbers.IsPrime(15).Should().BeFalse();
            PrimeNumbers.IsPrime(1763).Should().BeFalse();
        }

        [TestMethod]
        public void IsProbablyPrimeFermat()
        {
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
        public void GetPrimesBruteforce()
        {
            PrimeNumbers.GetPrimesBruteforce(50).Should().Equal(
                2, 3, 5, 7, 11, 13, 17, 
                19, 23, 29, 31, 37, 41, 43, 47);
            PrimeNumbers.GetPrimesBruteforce(5, 19).Should().Equal(5, 7, 11, 13, 17, 19);
            PrimeNumbers.GetPrimesBruteforce(480, 486).Should().BeEmpty();
        }

        [TestMethod]
        public void GetPrimes()
        {
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
