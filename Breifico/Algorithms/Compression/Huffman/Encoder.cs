using System.Collections.Generic;
using System.Linq;
using Breifico.DataStructures;

namespace Breifico.Algorithms.Compression.Huffman
{
    public class HuffmanCompressedData
    {
        public byte[] OutputBytes { get; }
        public int FreeBits { get; }
        public Dictionary<MyBitArray, byte> DecodeTable { get; }

        public HuffmanCompressedData(byte[] outputBytes, int freeBits, Dictionary<MyBitArray, byte> decodeTable) {
            this.OutputBytes = outputBytes;
            this.FreeBits = freeBits;
            this.DecodeTable = decodeTable;
        }
    }

    public class Encoder
    {
        private readonly byte[] _inputData;

        public Encoder(byte[] inputData) {
            this._inputData = inputData;
        }

        private int[] ComputeFrequencies() {
            var nodes = new int[255];

            foreach (byte b in this._inputData) {
                nodes[b] += 1;
            }
            return nodes;
        }

        private readonly Dictionary<byte, MyBitArray> _cache = 
            new Dictionary<byte, MyBitArray>(); 

        public HuffmanCompressedData Encode() {
            var outputBuffer = new MyBitArray();
            var freq = this.ComputeFrequencies();
            var tree = Tree.Create(freq);

            foreach (byte b in this._inputData) {
                if (!this._cache.ContainsKey(b)) {
                    this._cache[b] = tree.GetCode(b);
                }
                outputBuffer.Append(this._cache[b]);
            }

            var decodeTable = this._cache.ToDictionary(x => x.Value, x => x.Key);

            return new HuffmanCompressedData(outputBuffer.ToByteArray(), 
                outputBuffer.FreeBits, decodeTable);
        }
    }
}
