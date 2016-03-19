using System.Collections.Generic;
using Breifico.DataStructures;

namespace Breifico.Algorithms.Compression.Huffman
{
    public class HuffmanCompressedData
    {
        public byte[] OutputBytes { get; }
        public int FreeBits { get; }
        public HuffmanTree.Node DecodeTree { get; }

        public HuffmanCompressedData(byte[] outputBytes, int freeBits, HuffmanTree.Node decodeTree) {
            this.OutputBytes = outputBytes;
            this.FreeBits = freeBits;
            this.DecodeTree = decodeTree;
        }
    }

    /// <summary>
    /// Кодирует исходные данные алгоритмом Хаффмана
    /// </summary>
    public class HuffmanEncoder
    {
        private readonly byte[] _inputData;

        public HuffmanEncoder(byte[] inputData) {
            this._inputData = inputData;
        }

        private readonly Dictionary<byte, MyBitArray> _cache = 
            new Dictionary<byte, MyBitArray>();

        public HuffmanCompressedData EncodeTableTree() {
            var outputBuffer = new MyBitArray();
            var tree = HuffmanTreeBuilder.FromByteArray(this._inputData);

            foreach (byte b in this._inputData) {
                if (!this._cache.ContainsKey(b)) {
                    this._cache[b] = tree.GetCode(b);
                }
                outputBuffer.Append(this._cache[b]);
            }

            return new HuffmanCompressedData(outputBuffer.ToByteArray(), 
                outputBuffer.FreeBits, tree.Root);
        }

        public static MyBitArray EncodeTableTree(HuffmanTree.Node rootNode) {
            return EncodeTableTree(rootNode, new MyBitArray());
        }

        private static MyBitArray EncodeTableTree(HuffmanTree.Node n, MyBitArray a) {
            if (n.IsLeafNode) {
                a.Append(true);
                a.Append(n.LeafValue);
            } else {
                a.Append(false);
                EncodeTableTree(n.LeftNode, a);
                EncodeTableTree(n.RightNode, a);
            }
            return a;
        }
    }
}
