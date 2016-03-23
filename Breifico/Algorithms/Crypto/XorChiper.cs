using System.Linq;
using System.Text;
using Breifico.Algorithms.Numeric;

namespace Breifico.Algorithms.Crypto
{
    /// <summary>
    /// XOR-шифрование
    /// </summary>
    public class XorChiper
    {
        /// <summary>
        /// Ключ шифрования
        /// </summary>
        public byte[] Key { get; }


        /// <summary>
        /// Создает новый экзепляр <see cref="XorChiper"/> используя
        /// указанный массив байт в качестве ключа
        /// </summary>
        /// <param name="key">Массив байт, используемый в качестве ключа</param>
        public XorChiper(byte[] key) {
            this.Key = key;
        }

        /// <summary>
        /// Создает новый экзепляр <see cref="XorChiper"/> используя
        /// указанную строку в качестве ключа
        /// </summary>
        /// <param name="key">Строка, используемая в качестве ключа</param>
        public XorChiper(string key) {
            this.Key = Encoding.Unicode.GetBytes(key);
        }

        /// <summary>
        /// Создает новый экземпляр <see cref="XorChiper"/> с указанной
        /// длиной ключа
        /// </summary>
        /// <param name="keyLength">Длина ключа</param>
        public XorChiper(int keyLength) {
            var rnd = new LinearCongruentialGenerator();
            var bytes = rnd.GenerateBytes().Take(keyLength).ToArray();
            this.Key = bytes;
        }

        /// <summary>
        /// Шифрует указанный байтовый массив
        /// </summary>
        /// <param name="input">Исходный байтовый массив</param>
        /// <returns>Шифрованный байтовый массив</returns>
        public byte[] Process(byte[] input) {
            var output = new byte[input.Length];
            for (int i = 0; i < input.Length; i++) {
                output[i] = (byte)(input[i] ^ this.Key[i % this.Key.Length]);
            }
            return output;
        }
    }
}
