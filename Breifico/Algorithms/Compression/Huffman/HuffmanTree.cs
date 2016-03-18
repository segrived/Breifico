using System;
using System.Collections.Generic;
using System.IO;
using Breifico.DataStructures;
using Breifico.IO;

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
        public class Node : IComparable<Node>
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

            public Node() {
                this.Bytes = new List<byte>() { 0x00 };
            }

            public Node(Node left, Node right) {
                this.LeftNode = left;
                this.RightNode = right;
                this.Bytes = new List<byte>() {0x00};
            }

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

        public Node Root { get; private set; }

        public HuffmanTree(List<Node> nodes) {
            this.Build(nodes);
        }

        public MyBitArray Table { get; private set; }

        /// <summary>
        /// Строит дерево Хаффмана на основе доабвленных данных
        /// </summary>
        private void Build(List<Node> nodes) {
            while (nodes.Count > 1) {
                // Сортируем ноды по частоте, самые  редкие окажутся в конце списка
                nodes.Sort();
               
                // Временно сохраняем значения нод
                var node1 = nodes[nodes.Count - 1];
                var node2 = nodes[nodes.Count - 2];

                // Удаляет две последние ноды
                int count = nodes.Count;
                nodes.RemoveAt(count - 1);
                nodes.RemoveAt(count - 2);

                // Объеденяем список байт с двух последних нод
                var newBytes = new List<byte>();
                newBytes.AddRange(node1.Bytes);
                newBytes.AddRange(node2.Bytes);

                // Создаем новую ноду и добавляем ее обратно в список
                var newNode = new Node(newBytes, node1.Frequency + node2.Frequency) {
                    LeftNode = node1,
                    RightNode = node2
                };
                nodes.Add(newNode);
            }
            this.Root = nodes[0];
        }

        /// <summary>
        /// Возвращает битовый массив по указанному байту
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public MyBitArray GetCode(byte b) {
            var bitArray = new MyBitArray();
            var tempNode = this.Root;
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
