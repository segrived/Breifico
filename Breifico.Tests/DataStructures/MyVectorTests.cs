using Breifico.DataStructures;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.DataStructures
{
    [TestClass]
    public class MyVectorTests
    {
        [TestMethod]
        public void Sum_Tests() {
            new MyVector().GetSum().Should().Be(0);
            new MyVector(12).GetSum().Should()
                .BeApproximately(12.0, 0.01);
            new MyVector(4, 36, 45, 50, 75).GetSum().Should()
                .BeApproximately(210, 0.01);
        }

        [TestMethod]
        public void ArithmeticMean_Tests() {
            new MyVector().GetArithmeticMean().Should()
                .Be(double.NaN);
            new MyVector(12).GetArithmeticMean().Should()
                .BeApproximately(12.0, 0.01);
            new MyVector(4, 36, 45, 50, 75).GetArithmeticMean().Should()
                .BeApproximately(42, 0.01);
        }

        [TestMethod]
        public void GeometricMean_Tests() {
            new MyVector().GetGeometricMean().Should()
                .Be(double.NaN);
            new MyVector(12).GetGeometricMean().Should()
                .BeApproximately(12.0, 0.01);
            new MyVector(4, 36, 45, 50, 75).GetGeometricMean().Should()
                .BeApproximately(30, 0.01);
        }

        [TestMethod]
        public void HarmonicMean_Tests() {
            new MyVector().GetHarmonicMean().Should()
                .Be(double.NaN);
            new MyVector(12).GetHarmonicMean().Should()
                .BeApproximately(12.0, 0.01);
            new MyVector(4, 36, 45, 50, 75).GetHarmonicMean().Should()
                .BeApproximately(15, 0.01);
        }

        [TestMethod]
        public void AddOperator_WhenSingle_Test() {
            var v = new MyVector(1, 2, 7, 11) + 2;
            v.Should().Equal(3, 4, 9, 13);

            (new MyVector() + 1).Should().BeEmpty();
        }

        [TestMethod]
        public void SubOperator_WhenSingle_Test() {
            var v = new MyVector(1, 2, 7, 11) - 2;
            v.Should().Equal(-1, 0, 5, 9);

            (new MyVector() - 1).Should().BeEmpty();
        }

        [TestMethod]
        public void MulOperator_WhenSingle_Test() {
            var v = new MyVector(1, 2, 7, 11) * 2;
            v.Should().Equal(2, 4, 14, 22);

            (new MyVector() * 2).Should().BeEmpty();
        }

        [TestMethod]
        public void DivOperator_WhenSingle_Test() {
            var v = new MyVector(1, 2, 7, 11) / 2.0;
            v[0].AreEqualApproximately(0.5, 0.001).Should().BeTrue();
            v[1].AreEqualApproximately(1.0, 0.001).Should().BeTrue();
            v[2].AreEqualApproximately(3.5, 0.001).Should().BeTrue();
            v[3].AreEqualApproximately(5.5, 0.001).Should().BeTrue();

            (new MyVector() / 2.0).Should().BeEmpty();
        }
    }
}
