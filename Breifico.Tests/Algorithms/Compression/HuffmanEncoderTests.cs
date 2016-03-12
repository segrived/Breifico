using System.IO;
using Breifico.Algorithms.Compression.Huffman;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breifico.Tests.Algorithms.Compression
{
    [TestClass]
    public class HuffmanEncoderTests
    {
        [TestMethod]
        public void HuffmanEncoder_Test() {
            var bytes = File.ReadAllBytes(@"D:\My Files\TGPL_WorkFiles\url_adresses.txt");
            var x = new HuffmanEncoder(bytes);
            var encoded = x.Encode();
        }
    }
    }
