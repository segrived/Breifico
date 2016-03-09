using System.Linq;
using Breifico.DataStructures;

namespace Breifico.Algorithms.Compression.Huffman
{
    public class Decoder
    {
        private readonly HuffmanCompressedData _data;

        public Decoder(HuffmanCompressedData data) {
            this._data = data;
        }

        public byte[] Decode() {
            var bitArray = new MyBitArray();

            MyList<byte> output = new MyList<byte>();

            int lastIndex = this._data.OutputBytes.Length * 8 - this._data.FreeBits;

            for (int i = 0; i < this._data.OutputBytes.Length; i++) {
                int startBit = i * 8;
                for (int j = 0; j < 8; j++) {
                    if (startBit + j == lastIndex) {
                        break;
                    }
                    bool isSetByte = (this._data.OutputBytes[i] & (1 << 7 - j)) != 0;
                    bitArray.Append(isSetByte);
                    byte resultByte;
                    if (!this._data.DecodeTable.TryGetValue(bitArray, out resultByte)) {
                        continue;
                    }
                    output.Add(resultByte);
                    bitArray.Clear();
                }
            }
            return output.ToArray();
        }
    }
}
