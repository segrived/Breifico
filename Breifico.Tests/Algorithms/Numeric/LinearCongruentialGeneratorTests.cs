using System;
using System.Linq;
using Breifico.Algorithms.Numeric;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Numeric
{
    [TestClass]
    public class LinearCongruentialGeneratorTests
    {
        [TestMethod]
        public void NewInstance_WhenDefaultSeed_ShouldGenerateDifferentData() {
            int g1Value = new LinearCongruentialGenerator().Next();
            int g2Value = new LinearCongruentialGenerator().Next();
            g1Value.Should().NotBe(g2Value);
        }

        [TestMethod]
        public void Next_ShouldReturnNewRandomValue() {
            var gen = new LinearCongruentialGenerator(0xABBA);
            gen.Next().Should().Be(1081604715);
            gen.Next().Should().Be(1529113032);
            gen.Next().Should().Be(2137795169);
            gen.Next().Should().Be(503171462);
        }

        [TestMethod]
        public void NextDouble_ShouldReturnNewRandomDoubleValue() {
            var gen = new LinearCongruentialGenerator(0xABBA);
            gen.NextDouble().Should().BeApproximately(0.1, 0.50366);
            gen.NextDouble().Should().BeApproximately(0.1, 0.71204);
            gen.NextDouble().Should().BeApproximately(0.1, 0.99548);
            gen.NextDouble().Should().BeApproximately(0.1, 0.23430);
        }

        [TestMethod]
        public void Generate_ShouldGenerateRandomValues() {
            var genList = new LinearCongruentialGenerator(0xBAAB)
                .Generate().Take(9).ToList();
            genList.Should().Equal(74564872, 2057929889, 1444722374,
                                   219043463, 551692724, 1580779997,
                                   698186066, 1517278243, 173157664);
        }

        [TestMethod]
        public void GenerateInRange_ShouldGenerateRandomValuesInRange() {
            var genList = new LinearCongruentialGenerator(0xCAAC);
            genList.GenerateInRange(1, 10).Take(10).ToArray()
                .Should().Equal(4, 9, 4, 8, 3, 10, 3, 1, 5, 4);
            genList.GenerateInRange(-20, -10).Take(10).ToArray()
                .Should().Equal(-16, -13, -16, -13, -16, -9, -19, -13, -18, -9);
        }

        [TestMethod]
        public void GenerateDoubles_ShouldGenerateRandomDoubleValues() {
            var gen = new LinearCongruentialGenerator(0xCAAC)
                .GenerateDoubles().Take(5).ToArray();
            var values = new[] { 0.33688, 0.80428, 0.39373, 0.77361, 0.25803 };
            gen.Should().Equal(values, (l, r) => l.AreEqualApproximately(r, 0.01));
        }

        [TestMethod]
        public void GenerateDoubles_ShouldReturnValuesBetween0And1() {
            new LinearCongruentialGenerator(0xCAAC)
                .GenerateDoubles().Take(50)
                .Should().HaveCount(50).And
                .NotContain(d => d < 0.0).And
                .NotContain(d => d > 1.0);
        }

        [TestMethod]
        public void Next_WhenInvalidMinOrMax_ShouldThrowException() {
            var genList = new LinearCongruentialGenerator(0xCAAC);
            genList.Invoking(v => v.Next(5, 3))
                   .ShouldThrow<ArgumentException>();
            genList.Invoking(v => v.Next(0, 0))
                   .ShouldThrow<ArgumentException>();
            genList.Invoking(v => v.Next(10, 10))
                   .ShouldThrow<ArgumentException>();
            genList.Invoking(v => v.Next(0, -10))
                   .ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void Next_WhenInRange_ShouldReturnRandomValue() {
            var genList = new LinearCongruentialGenerator(0xAB45);
            genList.Next(0, 3).Should().Be(1);
            genList.Next(0, 3).Should().Be(3);
            genList.Next(0, 3).Should().Be(2);
            genList.Next(0, 3).Should().Be(2);
            genList.Next(0, 3).Should().Be(2);
            genList.Next(0, 3).Should().Be(0);
        }

        [TestMethod]
        public void NextByte_ShouldReturnRandomByte() {
            var genList = new LinearCongruentialGenerator(0xA55A);
            genList.NextByte().Should().Be(224);
            genList.NextByte().Should().Be(29);
            genList.NextByte().Should().Be(187);
            genList.NextByte().Should().Be(105);
            genList.NextByte().Should().Be(175);
        }
    }
}
