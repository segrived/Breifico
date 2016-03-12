using System.IO;
using Breifico.Algorithms.Compression.Huffman;

namespace Breifico.Archiver
{
    internal class Program
    {
        private static void Main(string[] args) {
            string fn = @"D:\Projects\Visual Studio\Breifico\Breifico.Archiver\bin\Debug\Breifico.Archiver.vshost.exe.manifest";
            byte[] fileContent = File.ReadAllBytes(fn);
            var x = new HuffmanEncoder(fileContent);
            var encoded = x.Encode();
            File.WriteAllBytes("enc_hello", encoded.OutputBytes);
            var decoder = new HuffmanDecoder(encoded);
            var decoded = decoder.Decode();
            File.WriteAllBytes("hello", decoded);
        }
    }
}