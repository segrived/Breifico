using System.IO;

namespace Breifico.Algorithms.Compression.Huffman
{
    public class TreeBuilder
    {
        private int[] _freqTable = new int[255];

        public void Append(byte b) {
            this._freqTable[b] += 1;
        }

        public Tree ToTree() {
            var tree = Tree.Create(this._freqTable);
            return tree;
        }

        public static Tree FromFile(string fileName) {
            using (var stream = File.OpenRead(fileName)) {
                return FromStream(stream);
            }
        }

        public static Tree FromByteArray(byte[] input) {
            var treeBuilder = new TreeBuilder();
            foreach (byte b in input) {
                treeBuilder.Append(b);
            }
            return treeBuilder.ToTree();
        }

        public static Tree FromStream(Stream s) {
            var treeBuilder = new TreeBuilder();
            int readByte;
            while ((readByte = s.ReadByte()) != -1) {
                treeBuilder.Append((byte)readByte);
            }
            return treeBuilder.ToTree();
        }
    }
}
