using System;
using System.Collections.Generic;
using Xunit;

namespace LeetCode.Structures
{
    public class BinarySearchTree
    {
        public enum Order
        {
            PreOrder,
            InOrder,
            PostOrder,
        }

        public class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Data { get; set; }

            public Node(int value)
            {
                Data = value;
            }
        }

        public Node Root { get; private set; }
        public int Count { get; private set; }

        internal void Add(int v)
        {
            var node = new Node(v);
            if (IsEmpty())
            {
                Root = node;
                Count++;
                return;
            }

            var current = Root;
            while (current != null)
            {
                if (v == current.Data)
                    throw new InvalidOperationException();
                if (v < current.Data)
                {
                    if (current.Left == null)
                    {
                        current.Left = node;
                        Count++;
                        return;
                    }

                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = node;
                        Count++;
                        return;
                    }

                    current = current.Right;
                }
            }
        }

        internal bool Search(int value)
        {
            var current = Root;
            while (current != null)
            {
                if (current.Data == value)
                    return true;

                if (value < current.Data)
                    current = current.Left;
                else
                    current = current.Right;
            }

            return false;
        }

        private bool IsEmpty()
        {
            return Root == null;
        }

        internal IEnumerable<int> DFS(Order order = Order.PostOrder)
        {
            return DFS(Root, order);
        }

        private IEnumerable<int> DFS(Node node, Order order)
        {
            if (order == Order.PreOrder)
                yield return node.Data;

            if (node.Left != null)
                foreach (var item in DFS(node.Left, order))
                    yield return item;

            if (order == Order.InOrder)
                yield return node.Data;

            if (node.Right != null)
                foreach (var item in DFS(node.Right, order))
                    yield return item;

            if (order == Order.PostOrder)
                yield return node.Data;
        }

        internal IEnumerable<int> BFS()
        {
            var q = new Queue<Node>();
            q.Enqueue(Root);
            while (q.Count > 0)
                foreach (var item in BFS(q))
                    yield return item.Data;
        }

        private IEnumerable<Node> BFS(Queue<Node> q)
        {
            var node = q.Dequeue();
            yield return node;
            if (node.Left != null)
                q.Enqueue(node.Left);
            if (node.Right != null)
                q.Enqueue(node.Right);
        }
    }

    public class BinarySearchTreeTests
    {
        private readonly BinarySearchTree bt;

        public BinarySearchTreeTests()
        {
            bt = new BinarySearchTree();
        }

        [Fact]
        public void empty_when_create()
        {
            Assert.Equal(0, bt.Count);
            Assert.False(bt.Search(0));
        }

        [Fact]
        public void when_add()
        {
            bt.Add(5);
            Assert.Equal(1, bt.Count);
        }

        [Fact]
        public void when_add2()
        {
            bt.Add(2);
            bt.Add(1);
            bt.Add(3);
            Assert.Equal(2, bt.Root.Data);
            Assert.Equal(1, bt.Root.Left.Data);
            Assert.Equal(3, bt.Root.Right.Data);
            Assert.Equal(3, bt.Count);
        }

        [Fact]
        public void when_search()
        {
            bt.Add(5);
            Assert.True(bt.Search(5));
        }

        [Fact]
        public void when_search2()
        {
            bt.Add(5);
            Assert.False(bt.Search(4));
        }

        [Fact]
        public void when_search3()
        {
            bt.Add(2);
            bt.Add(1);
            bt.Add(3);
            Assert.True(bt.Search(3));
            Assert.True(bt.Search(1));
            Assert.True(bt.Search(2));
        }

        [Fact]
        public void when_DFS_inOrder()
        {
            bt.Add(6);
            bt.Add(2);
            bt.Add(7);
            bt.Add(1);
            bt.Add(4);
            bt.Add(9);
            bt.Add(3);
            bt.Add(5);
            bt.Add(8);

            Assert.Equal(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, bt.DFS(BinarySearchTree.Order.InOrder));
        }

        [Fact]
        public void when_DFS_preOrder()
        {
            bt.Add(6);
            bt.Add(2);
            bt.Add(7);
            bt.Add(1);
            bt.Add(4);
            bt.Add(9);
            bt.Add(3);
            bt.Add(5);
            bt.Add(8);

            Assert.Equal(new int[] { 6, 2, 1, 4, 3, 5, 7, 9, 8 }, bt.DFS(BinarySearchTree.Order.PreOrder));
        }

        [Fact]
        public void when_DFS_postOrder()
        {
            bt.Add(6);
            bt.Add(2);
            bt.Add(7);
            bt.Add(1);
            bt.Add(4);
            bt.Add(9);
            bt.Add(3);
            bt.Add(5);
            bt.Add(8);

            Assert.Equal(new int[] { 1, 3, 5, 4, 2, 8, 9, 7, 6 }, bt.DFS(BinarySearchTree.Order.PostOrder));
        }

        [Fact]
        public void when_BFS()
        {
            bt.Add(6);
            bt.Add(2);
            bt.Add(7);
            bt.Add(1);
            bt.Add(4);
            bt.Add(9);
            bt.Add(3);
            bt.Add(5);
            bt.Add(8);

            Assert.Equal(new int[] { 6, 2, 7, 1, 4, 9, 3, 5, 8 }, bt.BFS());
        }
    }
}
