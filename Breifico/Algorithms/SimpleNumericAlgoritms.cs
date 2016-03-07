using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Algorithms
{
    public static class SimpleNumericAlgoritms
    {
        public static int GcdFinder(int a, int b) {
            while (b != 0) {
                int rem = a % b;
                a = b;
                b = rem;
            }
            return a;
        }

        public static void RandomizeArray<T>(T[] input) {
            var generator = new LinearCongruentialGenerator();
            int maxIndex = input.Length - 1;
            for (int i = 0; i < maxIndex; i++) {
                int newIndex = generator.Next(i, maxIndex);
                var tempValue = input[newIndex];
                input[newIndex] = input[i];
                input[i] = tempValue;
            }
        }

        public static double ExpNumber(long number, long exp) {
            if (exp < 0) {
                return 1.0 / ExpNumber(number, -exp);
            }
            long result = 1;
            while (exp > 0) {
                if ((exp & 1) != 0) {
                    result *= number;
                }
                exp >>= 1;
                number *= number;
            }
            return result;
        }
    }

    [TestClass]
    public class SimpleNumericAlgoritmsTests
    {
        [TestMethod]
        public void GcdFinder_ShouldReturnGcd() {
            SimpleNumericAlgoritms.GcdFinder(2, 1).Should().Be(1);
            SimpleNumericAlgoritms.GcdFinder(160, 0).Should().Be(160);
            SimpleNumericAlgoritms.GcdFinder(8, 8).Should().Be(8);
            SimpleNumericAlgoritms.GcdFinder(4851, 3003).Should().Be(231);
        }

        [TestMethod]
        public void Randomize_WhenAnyElement_ShouldRandomize() {
            var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            SimpleNumericAlgoritms.RandomizeArray(arr);
            arr.Should().Contain(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            arr.Should().HaveCount(arr.Length);
        }

        [TestMethod]
        public void Randomize_WhenEmpty_ShouldDoNothing() {
            var arr = new int[0];
            SimpleNumericAlgoritms.RandomizeArray(arr);
            arr.Should().BeEmpty();
        }

        [TestMethod]
        public void ExpNumber_ShouldRaiseToPower() {
            SimpleNumericAlgoritms.ExpNumber(10, -2).Should()
                .BeApproximately(0.01, 0.001);
            SimpleNumericAlgoritms.ExpNumber(10, -3).Should()
                .BeApproximately(0.001, 0.001);
            SimpleNumericAlgoritms.ExpNumber(2, -8).Should()
                .BeApproximately(0.003906, 0.001);
            SimpleNumericAlgoritms.ExpNumber(0, 0).Should().Be(1);
            SimpleNumericAlgoritms.ExpNumber(1, 0).Should().Be(1);
            SimpleNumericAlgoritms.ExpNumber(1, 1).Should().Be(1);
            SimpleNumericAlgoritms.ExpNumber(2, 3).Should().Be(8);
            SimpleNumericAlgoritms.ExpNumber(2, 4).Should().Be(16);
            SimpleNumericAlgoritms.ExpNumber(2, 5).Should().Be(32);
            SimpleNumericAlgoritms.ExpNumber(12, 7).Should().Be(35831808);
            SimpleNumericAlgoritms.ExpNumber(7, 12).Should().Be(13841287201);
        }
    }
}
