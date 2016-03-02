using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Breifico.DataStructures
{
    [DebuggerDisplay("MyBinarySearchTree<T>: {Count} element(s)")]
    public class MyBinarySearchTree<T> : IEnumerable<T>, ICollection where T : IComparable<T>
    {
        private enum NodePosition { Left, Right }

        [DebuggerDisplay("{Value}")]
        public class Node<TR> where TR : IComparable<TR>
        {
            public Node<TR> Left { get; set; }
            public Node<TR> Right { get; set; }

            public TR Value { get; set; }

            public Node(TR value) {
                this.Value = value;
            }

            public bool IsLeaf => this.Left == null && this.Right == null;

            public bool HasOnlyOneChild =>
                (this.Left != null && this.Right == null) 
             || (this.Right != null && this.Left == null);
            
            public override string ToString() {
                return $"{this.Value}";
            }
        }

        private object _syncRoot;

        private Node<T> _rootNode;

        public int Count { get; private set; }

        public void Add(T item) {
            if (this._rootNode == null) {
                this._rootNode = new Node<T>(item);
                this.Count += 1;
                return;
            }
            var currentNode = this._rootNode;
            while (true) {
                int compareResult = item.CompareTo(currentNode.Value);
                // do not add duplicates
                if (compareResult == 0) {
                    return;
                }
                if (compareResult < 0) {
                    if (currentNode.Left == null) {
                        currentNode.Left = new Node<T>(item);
                        break;
                    }
                    currentNode = currentNode.Left;
                } else {
                    if (currentNode.Right == null) {
                        currentNode.Right = new Node<T>(item);
                        break;
                    }
                    currentNode = currentNode.Right;
                }
            }
            this.Count += 1;
        }

        public bool Contains(T item) {
            var node = this.FindNode(item);
            return node != null;
        }

        class FoundNode<T> where T: IComparable<T>
        {
            public Node<T> Node { get; }
            public Node<T> Parent { get; }
            public NodePosition Position { get; }

            public FoundNode(Node<T> node, Node<T> parent, NodePosition position) {
                this.Node = node;
                this.Parent = parent;
                this.Position = position;
            }

            public Node<T> GetNodeByPos() {
                return this.Position == NodePosition.Left 
                    ? this.Parent.Left 
                    : this.Parent.Right;
            }

            public void ReplaceNode(Node<T> newNode) {
                if (this.Position == NodePosition.Left) {
                    this.Parent.Left = newNode;
                } else {
                    this.Parent.Right = newNode;
                }
            }

            public void ReplaceNodeValue(T newValue) {
                if (this.Position == NodePosition.Left) {
                    this.Parent.Left.Value = newValue;
                } else {
                    this.Parent.Right.Value = newValue;
                }
            }
        }

        private FoundNode<T> FindNode(Node<T> item) {
            return this.FindNode(item.Value);
        }

        private FoundNode<T> FindNode(T item) {
            var tempNode = this._rootNode;
            Node<T> parent = null;
            var nodePosition = NodePosition.Left;

            while (tempNode != null) {
                int compResult = item.CompareTo(tempNode.Value);
                if (compResult == 0) {
                    return new FoundNode<T>(tempNode, parent, nodePosition);
                }
                if (compResult < 0) {
                    parent = tempNode;
                    nodePosition = NodePosition.Left;
                    tempNode = tempNode.Left;
                } else {
                    parent = tempNode;
                    nodePosition = NodePosition.Right;
                    tempNode = tempNode.Right;
                }
            }
            return null;
        } 

        public void Remove(T item) {
            var node = this.FindNode(item);
            if (node == null) {
                return;
            }
            if (node.Node.IsLeaf) {
                // if root without any childs
                if (node.Parent == null) {
                    this._rootNode = null;
                } else {
                    node.ReplaceNode(null);
                }
            } else if (node.Node.HasOnlyOneChild) {
                var newNode = node.Node.Left ?? node.Node.Right;
                // if root with only one child
                if (node.Parent == null) {
                    this._rootNode = newNode;
                } else {
                    node.ReplaceNode(newNode);
                }
            } else {
                var x = node.Node.Right;
                var parent = node.Node;
                while (x != null) {
                    if (x.Left == null) {
                        var nodeValue = x.Value;
                        this.Remove(parent.Left.Value);
                        node.ReplaceNodeValue(nodeValue);
                        return;
                    }
                    parent = x;
                    x = x.Left;
                }
            }
        }

        private Node<T> FindMinimumNode(Node<T> start) {
            var tempNode = start;
            while (tempNode != null) {
                if (tempNode.Left == null) {
                    return tempNode;
                }
                tempNode = tempNode.Left;
            }
            return null;
        }

        private Node<T> FindMaximumNode(Node<T> start) {
            var tempNode = start;
            while (tempNode != null) {
                if (tempNode.Right == null) {
                    return tempNode;
                }
                tempNode = tempNode.Right;
            }
            return null;
        }

        public T FindMinimum(T def = default(T)) {
            var node = this.FindMinimumNode(this._rootNode);
            return node == null ? def : node.Value;
        }

        public T FindMaximum(T def = default(T)) {
            var node = this.FindMaximumNode(this._rootNode);
            return node == null ? def : node.Value;
        }

        public void AddRange(IEnumerable<T> items) {
            foreach (var item in items) {
                this.Add(item);
            }
        }

        #region IEnumerable<T> implementation
        public IEnumerator<T> GetEnumerator() {
            var stack = new MyStack<Node<T>>();
            var node = this._rootNode;
            while (stack.Count != 0 || node != null) {
                if (node != null) {
                    stack.Push(node);
                    node = node.Left;
                } else {
                    node = stack.Pop();
                    yield return node.Value;
                    node = node.Right;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        #endregion

        #region ICollection implementation
        public void CopyTo(Array array, int index) {
            throw new NotImplementedException();
        }

        public object SyncRoot {
            get
            {
                if (this._syncRoot == null) {
                    System.Threading.Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
                }
                return this._syncRoot;
            }
        }

        public bool IsSynchronized { get; } = false;
        #endregion
    }
}
