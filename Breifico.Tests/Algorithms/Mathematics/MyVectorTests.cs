using Breifico.Mathematics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Mathematics
{
    [TestClass]
    public class MyVectorTests
    {
        [TestMethod]
        public void Sum_Tests() {
            new MyVector().Sum().Should().Be(0);
            new MyVector(12).Sum().Should()
                .BeApproximately(12.0, 0.01);
            new MyVector(4, 36, 45, 50, 75).Sum().Should()
                .BeApproximately(210, 0.01);
        }

        [TestMethod]
        public void ArithmeticMean_Tests() {
            new MyVector().ArithmeticMean().Should()
                .Be(double.NaN);
            new MyVector(12).ArithmeticMean().Should()
                .BeApproximately(12.0, 0.01);
            new MyVector(4, 36, 45, 50, 75).ArithmeticMean().Should()
                .BeApproximately(42, 0.01);
        }

        [TestMethod]
        public void GeometricMean_Tests() {
            new MyVector().GeometricMean().Should()
                .Be(double.NaN);
            new MyVector(12).GeometricMean().Should()
                .BeApproximately(12.0, 0.01);
            new MyVector(4, 36, 45, 50, 75).GeometricMean().Should()
                .BeApproximately(30, 0.01);
        }

        [TestMethod]
        public void HarmonicMean_Tests() {
            new MyVector().HarmonicMean().Should()
                .Be(double.NaN);
            new MyVector(12).HarmonicMean().Should()
                .BeApproximately(12.0, 0.01);
            new MyVector(4, 36, 45, 50, 75).HarmonicMean().Should()
                .BeApproximately(15, 0.01);
        }
    }
}
