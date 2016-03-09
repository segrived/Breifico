using Breifico.Algorithms.Crypto;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Crypto
{
    [TestClass]
    public class XorChiperTests
    {
        [TestMethod]
        public void NewInstance_WithByteArray_ShouldAssignKey() {
            var xor = new XorChiper(new byte[] {0x00, 0x55, 0xAB});
            xor.Key.Length.Should().Be(3);
            xor.Key[0].Should().Be(0x00);
            xor.Key[1].Should().Be(0x55);
            xor.Key[2].Should().Be(0xAB);
        }

        [TestMethod]
        public void NewInstance_WithString_ShouldGenerateAndAssignKey() {
            const string pass = "passphrase";
            var xor = new XorChiper(pass);
            xor.Key.Length.Should().Be(pass.Length * 2); // UTF16
        }

        [TestMethod]
        public void NewInstance_WithLength_ShouldCreateRandomKeyAndAssignKey() {
            var xor = new XorChiper(255);
            xor.Key.Length.Should().Be(255);
        }

        [TestMethod]
        public void Process_ShouldEncodeAndDecodeText() {
            var key = new byte[] {0xDA, 0xAD};
            var input = new byte[] { 0x10, 0x11, 0x12, 0x13 };

            var xor = new XorChiper(key);
            var result = xor.Process(input);
            result.Should().NotEqual(input);
            result.Length.Should().Be(4);
            xor.Process(result).Should().Equal(input);
        }
    }
}
