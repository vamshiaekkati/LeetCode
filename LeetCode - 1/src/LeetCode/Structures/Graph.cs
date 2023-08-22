using System;
using System.Collections.Generic;
using Xunit;

namespace LeetCode.Structures
{
    public class Graph
    {
        private readonly Dictionary<int, Node> nodes;

        public int Count => nodes.Count;

        private class Node
        {
            public int Id { get; private set; }
            public List<Node> Connections { get; set; }
            public Node(int id)
            {
                Id = id;
                Connections = new List<Node>();
            }
        }

        public Graph()
        {
            nodes = new Dictionary<int, Node>();
        }

        internal void AddNode(int v)
        {
            nodes.Add(v, new Node(v));
        }

        internal void AddPath(int source, int dest)
        {
            var s = nodes[source];
            var d = nodes[dest];
            s.Connections.Add(d);
            d.Connections.Add(s);
        }

        internal bool HasPathDFS(int source, int dest)
        {
            var visited = new HashSet<int>();
            return DFS(nodes[source], dest, visited);
        }

        private bool DFS(Node node, int dest, HashSet<int> visited)
        {
            if (node.Id == dest)
                return true;
            if (visited.Contains(node.Id))
                return false;
            visited.Add(node.Id);
            foreach (var connect in node.Connections)
                if (DFS(connect, dest, visited))
                    return true;
            return false;
        }

        internal bool HasPathBFS(int source, int dest)
        {
            var queue = new Queue<int>();
            var visited = new HashSet<int>();
            queue.Enqueue(source);
            while (queue.Count > 0)
            {
                var node = nodes[queue.Dequeue()];
                if (node.Id == dest)
                    return true;
                if (visited.Contains(node.Id))
                    continue;
                visited.Add(node.Id);
                foreach (var item in node.Connections)
                    queue.Enqueue(item.Id);
            }
            return false;
        }
    }

    public class GraphTests
    {
        private readonly Graph tree;

        public GraphTests()
        {
            tree = new Graph();
        }
        [Fact]
        public void addNode()
        {
            tree.AddNode(3);
            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void addPath()
        {
            tree.AddNode(3);
            tree.AddNode(4);
            tree.AddPath(3, 4);
        }

        [Fact]
        public void havePath()
        {
            tree.AddNode(3);
            tree.AddNode(4);
            tree.AddPath(3, 4);
            hasPath(3,4);
        }

        private void hasPath(int s, int d)
        {
            Assert.True(tree.HasPathDFS(s, d));
            Assert.True(tree.HasPathBFS(s, d));
        }

        private void noPath(int s, int d)
        {
            Assert.False(tree.HasPathDFS(s, d));
            Assert.False(tree.HasPathBFS(s, d));
        }

        [Fact]
        public void noPathFact()
        {
            tree.AddNode(3);
            tree.AddNode(4);
            noPath(3, 4);
        }

        [Fact]
        public void different_trees()
        {
            tree.AddNode(1);
            tree.AddNode(2);
            tree.AddNode(3);
            tree.AddNode(4);
            tree.AddPath(1, 2);
            tree.AddPath(2, 3);
            tree.AddPath(3, 4);
            tree.AddPath(4, 2);

            tree.AddNode(5);
            tree.AddNode(6);
            tree.AddNode(7);
            tree.AddNode(8);
            tree.AddNode(9);
            tree.AddPath(5, 6);
            tree.AddPath(6, 7);
            tree.AddPath(7, 8);
            tree.AddPath(8, 9);
            tree.AddPath(9, 6);

            hasPath(1, 4);
            hasPath(5, 9);
            hasPath(6, 7);
            noPath(1, 5);
            noPath(9, 4);
            noPath(4, 5);
        }
    }
}
