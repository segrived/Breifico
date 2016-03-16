using System.IO;
using Breifico.Algorithms.Compression.Huffman;
using Breifico.Algorithms.Compression.RLE;

namespace Breifico.Archiver
{
    internal class Program
    {
        private static void Main(string[] args) {
            //string fn = @"D:\My Files\Unit\my_big.bmp";
            //byte[] fileContent = File.ReadAllBytes(fn);
            //var rle = new RleEncoder();
            //var rleEnc = rle.Encode(fileContent);
            //var x = new HuffmanEncoder(rleEnc);
            //var encoded = x.Encode();
            //var decoder = new HuffmanDecoder(encoded).Decode();
            //var output = rle.Decode(decoder);

            //File.WriteAllBytes("enc_hello", encoded.OutputBytes);
            //var decoder = new HuffmanDecoder(encoded);
            //var decoded = decoder.Decode();
            //File.WriteAllBytes("hello", decoded);
        }
    }
}