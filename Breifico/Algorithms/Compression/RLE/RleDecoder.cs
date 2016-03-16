using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Breifico.DataStructures;

namespace Breifico.Algorithms.Compression.RLE
{
    public class RleDecoder
    {
        private byte[] _input;

        public RleDecoder(byte[] input) {
            this._input = input;
        }

        public byte[] Decode() {
            MyList<byte> output = new MyList<byte>();
            for (int i = 0; i < this._input.Length;) {
                byte b = this._input[i];
                if (this._input[i] != 0x00) {
                    byte byteCode = this._input[i + 1];
                    for (int j = 0; j < this._input[i]; j++) {
                        output.Add(byteCode);
                    }
                    i += 2;
                } else {
                    byte count = this._input[i + 1];
                    var arr = new byte[count];
                    Array.Copy(this._input, i + 2, arr, 0, count);
                    output.AddRange(arr);
                    i += 2 + count;
                }
            }
            return output.ToArray();
        }
    }
}
