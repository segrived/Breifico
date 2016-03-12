using System;
using System.Collections.Generic;
using Breifico.DataStructures;

namespace Breifico.Algorithms.Compression.Huffman
{
    /// <summary>
    /// Бинарное дерево Хаффмана
    /// </summary>
    public class HuffmanTree
    {
        /// <summary>
        /// Нода бинарного дерева Хаффмана
        /// </summary>
        private class Node : IComparable<Node>
        {
            /// <summary>
            /// Левая нода
            /// </summary>
            public Node LeftNode { get; internal set; }

            /// <summary>
            /// Правая нода
            /// </summary>
            public Node RightNode { get; internal set; }

            /// <summary>
            /// Список байтов
            /// </summary>
            public List<byte> Bytes { get; }

            /// <summary>
            /// Частота вхождения списка байтов
            /// </summary>
            public int Frequency { get; }

            /// <summary>
            /// Создает новую ноду с массивом байт и указанной частотой вхождения
            /// </summary>
            /// <param name="nodeBytes">Массив байт</param>
            /// <param name="frequency">Частота вхождения</param>
            public Node(List<byte> nodeBytes, int frequency) {
                this.Bytes = nodeBytes;
                this.Frequency = frequency;
            }

            /// <summary>
            /// Содает новую ноду с одним байтом и указанной частотой вхождения
            /// </summary>
            /// <param name="nodeByte">Байт</param>
            /// <param name="frequency">Частота вхождения</param>
            public Node(byte nodeByte, int frequency) : 
                this(new List<byte> { nodeByte }, frequency) {}

            /// <summary>
            /// Возвращает true если текущая нода является листом
            /// </summary>
            public bool IsLeafNode => this.LeftNode == null && this.RightNode == null;

            /// <summary>
            /// Содержит первый (и единственный, в случае если нода являтся листом)
            /// байт ноды
            /// </summary>
            public byte LeafValue => this.Bytes[0];

            /// <summary>
            /// Сравнивает одну ноду с другой. Сравнение выполняется по частоте вхождения
            /// </summary>
            /// <param name="other">Другая нода</param>
            /// <returns>Значение, указывающее, каков относительный порядок сравниваемых объектов</returns>
            public int CompareTo(Node other) {
                return other.Frequency.CompareTo(this.Frequency);
            }
        }

        private readonly List<Node> _nodes;

        public static HuffmanTree Create(int[] freqData) {
            if (freqData.Length > 255) {
                throw new Exception();
            }
            var nodes = new List<Node>(255);
            for (byte i = 0; i < freqData.Length; i++) {
                if (freqData[i] == 0) {
                    continue;
                }
                nodes.Add(new Node(i, freqData[i]));
            }
            return new HuffmanTree(nodes);
        }

        private HuffmanTree(List<Node> nodes) {
            this._nodes = nodes;
            this.Build();
        }

        /// <summary>
        /// Строит дерево Хаффмана на основе доабвленных данных
        /// </summary>
        private void Build() {
            while (this._nodes.Count > 1) {
                // Сортируем ноды по частоте, самые  редкие окажутся в конце списка
                this._nodes.Sort();
               
                // Временно сохраняем значения нод
                var node1 = this._nodes[this._nodes.Count - 1];
                var node2 = this._nodes[this._nodes.Count - 2];

                // Удаляет две последние ноды
                int count = this._nodes.Count;
                this._nodes.RemoveAt(count - 1);
                this._nodes.RemoveAt(count - 2);

                // Объеденяем список байт с двух последних нод
                var newBytes = new List<byte>();
                newBytes.AddRange(node1.Bytes);
                newBytes.AddRange(node2.Bytes);

                // Создаем новую ноду и добавляем ее обратно в список
                var newNode = new Node(newBytes, node1.Frequency + node2.Frequency) {
                    LeftNode = node1,
                    RightNode = node2
                };
                this._nodes.Add(newNode);
            }
        }

        /// <summary>
        /// Возвращает битовый массив по указанному байту
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public MyBitArray GetCode(byte b) {
            var bitArray = new MyBitArray();
            var tempNode = this._nodes[0];
            while (!tempNode.IsLeafNode) {
                if (tempNode.LeftNode.Bytes.Contains(b)) {
                    tempNode = tempNode.LeftNode;
                    bitArray.Append(false);
                } else {
                    tempNode = tempNode.RightNode;
                    bitArray.Append(true);
                }
            }
            return bitArray;
        }
    }
}
