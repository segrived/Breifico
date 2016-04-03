using System;
using System.Collections.Generic;
using System.IO;

namespace Breifico.Algorithms.Compression.Huffman
{
    /// <summary>
    /// Строит дерево Хаффмана на основе исходных данных
    /// </summary>
    public sealed class HuffmanTreeBuilder
    {
        private readonly int[] _freqTable = new int[256];

        /// <summary>
        /// Добавляет байт в список частот
        /// </summary>
        /// <param name="b">Исходный байт</param>
        public void Append(byte b) {
            this._freqTable[b] += 1;
        }

        /// <summary>
        /// Строит дерефо Хаффмана и возвращает его
        /// </summary>
        /// <returns>Дерево Хаффмана</returns>
        public HuffmanTree ToTree() {
            if (this._freqTable.Length > 256) {
                throw new Exception();
            }
            var nodes = new List<HuffmanTree.Node>(256);
            for (int i = 0; i < this._freqTable.Length; i++) {
                if (this._freqTable[i] == 0) {
                    continue;
                }
                nodes.Add(new HuffmanTree.Node((byte)i, this._freqTable[i]));
            }
            return new HuffmanTree(nodes);
        }

        /// <summary>
        /// Строит дерево Хаффмана по содержимому файла
        /// </summary>
        /// <param name="fileName">Имя исходного файла</param>
        /// <returns>Дерево Хаффмана</returns>
        public static HuffmanTree FromFile(string fileName) {
            using (var stream = File.OpenRead(fileName)) {
                return FromStream(stream);
            }
        }

        /// <summary>
        /// Строит дерево Хаффмана по байтовому массиву
        /// </summary>
        /// <param name="input">Исходный байтовый массив</param>
        /// <returns>Дерево Хаффмана</returns>
        public static HuffmanTree FromByteArray(byte[] input) {
            var treeBuilder = new HuffmanTreeBuilder();
            foreach (byte b in input) {
                treeBuilder.Append(b);
            }
            return treeBuilder.ToTree();
        }

        /// <summary>
        /// Строит дерефо Хаффана из потока данных
        /// </summary>
        /// <param name="s">Исходный поток</param>
        /// <returns>Дерево Хаффмана</returns>
        public static HuffmanTree FromStream(Stream s) {
            var treeBuilder = new HuffmanTreeBuilder();
            int readByte;
            while ((readByte = s.ReadByte()) != -1) {
                treeBuilder.Append((byte)readByte);
            }
            return treeBuilder.ToTree();
        }
    }
}
