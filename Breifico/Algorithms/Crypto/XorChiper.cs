using System.Linq;
using System.Text;
using Breifico.Algorithms.Numeric;

namespace Breifico.Algorithms.Crypto
{
    public class XorChiper
    {
        public byte[] Key { get; private set; }

        public XorChiper(byte[] key) {
            this.Key = key;
        }

        public XorChiper(string key) {
            this.Key = Encoding.Unicode.GetBytes(key);
        }

        public XorChiper(int keyLength) {
            var rnd = new LinearCongruentialGenerator();
            var bytes = rnd.GenerateBytes().Take(keyLength).ToArray();
            this.Key = bytes;
        }

        public byte[] Process(byte[] input) {
            var output = new byte[input.Length];
            for (int i = 0; i < input.Length; i++) {
                output[i] = (byte)(input[i] ^ this.Key[i % this.Key.Length]);
            }
            return output;
        }
    }
}
