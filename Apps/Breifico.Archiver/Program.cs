using System;
using System.IO;
using Breifico.Algorithms.Compression.Huffman;
using Breifico.Algorithms.Compression.RLE;
using Breifico.IO;
using CommandLine;

namespace Breifico.Archiver
{
    internal class Program
    {
        private static void Main(string[] args) {
            Parser.Default.ParseArguments<Options>(args).WithParsed(CompressFile);
        }

        private static void CompressFile(Options opt) {
            try {
                byte[] fileContent = File.ReadAllBytes(opt.InputFile);
                var rleEncoded = new RleEncoder(fileContent);
                fileContent = rleEncoded.Encode();
                var bytes = new HuffmanEncoder(fileContent);
                var encodedMessage = bytes.EncodeTableTree();

                var tableTree = HuffmanEncoder.EncodeTableTree(encodedMessage.DecodeTree);

                var writer = new StreamBinaryWriter(opt.OutputFile);
                writer.WriteBitArray(tableTree);
                writer.WriteInt32(encodedMessage.OutputBytes.Length);
                writer.WriteByteArtray(encodedMessage.OutputBytes);
                writer.WriteByte((byte)encodedMessage.FreeBits);
                writer.Dispose();

                var reader = new StreamBinaryReader(opt.OutputFile);
                var bitArray = HuffmanDecoder.DecodeTableTree(reader.ReadBitArray());
                var bytesCount = reader.ReadInt32();
                var myBytes = reader.ReadBytes(bytesCount);
                var freeBits = (int)reader.ReadByte();
                reader.Dispose();

                var decoder = new HuffmanDecoder(new HuffmanCompressedData(myBytes, freeBits, bitArray));
                var x = decoder.Decode();

                var rleDecoder = new RleDecoder(x);
                var zz = rleDecoder.Decode();
                File.WriteAllBytes("zzz", zz);

            } catch (Exception ex) {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}