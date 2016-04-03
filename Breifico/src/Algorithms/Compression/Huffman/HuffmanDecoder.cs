using Breifico.DataStructures;
using Breifico.IO;

namespace Breifico.Algorithms.Compression.Huffman
{
    public sealed class HuffmanDecoder
    {
        private readonly HuffmanCompressedData _data;

        public HuffmanDecoder(HuffmanCompressedData data) {
            this._data = data;
        }

        public byte[] Decode() {
            var output = new MyList<byte>();

            int lastIndex = this._data.OutputBytes.Length * 8 - this._data.FreeBits;

            var tempNode = this._data.DecodeTree;

            for (int i = 0; i < this._data.OutputBytes.Length; i++) {
                int startBit = i * 8;
                for (int j = 0; j < 8; j++) {
                    if (tempNode.IsLeafNode) {
                        output.Add(tempNode.LeafValue);
                        tempNode = this._data.DecodeTree;
                    }
                    if (startBit + j == lastIndex) {
                        break;
                    }
                    bool isSetByte = (this._data.OutputBytes[i] & (1 << 7 - j)) != 0;
                    tempNode = isSetByte ? tempNode.RightNode : tempNode.LeftNode;
                }
            }
            return output.ToArray();
        }

        public static HuffmanTree.Node DecodeTableTree(MyBitArray r) {
            var reader = new BitArrayReader(r);
            return DecodeTableTree(reader);
        }

        private static HuffmanTree.Node DecodeTableTree(BitArrayReader r) {
            if (r.ReadBit()) {
                return new HuffmanTree.Node(r.ReadByte(), 0);
            }
            var leftNode = DecodeTableTree(r);
            var rightNode = DecodeTableTree(r);
            return new HuffmanTree.Node(leftNode, rightNode);
        }
    }
}
