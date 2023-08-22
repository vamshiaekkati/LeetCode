using System.Collections.Generic;
using Xunit;

namespace LeetCode.Structures
{
    public class Trie
    {
        public Node Root { get; private set; }

        public class Node
        {
            public Dictionary<char, Node> Children;
            public bool CompleteWord;
            public Node Parent;

            public Node(Node parent)
            {
                Children = new Dictionary<char, Node>();
                Parent = parent;
            }
        }

        public Trie()
        {
            Root = new Node(null);
        }

        public void Insert(string word)
        {
            var current = Root;
            foreach (var c in word)
            {
                if (!current.Children.ContainsKey(c))
                {
                    var node = new Node(current);
                    current.Children.Add(c, node);
                    current = node;
                }
                else
                    current = current.Children[c];
            }
            current.CompleteWord = true;
        }

        public bool Contains(string word)
        {
            var node = Find(word);
            return node == null ? false : node.CompleteWord;
        }

        public void Delete(string word)
        {
            Delete(Root, word, 0);
        }

        private bool Delete(Node node, string word, int depth)
        {
            if (node == null)
                return true;

            if (word.Length == depth)
            {
                node.CompleteWord = false;
                return node.Children.Values.Count == 0;
            }

            var c = word[depth];
            if (node.Children.ContainsKey(c))
            {
                if (Delete(node.Children[c], word, depth + 1))
                    node.Children.Remove(c);
            }

            return node.Children.Values.Count == 0 && node.CompleteWord == false;
        }

        private Node Find(string word)
        {
            var current = Root;
            foreach (var c in word)
            {
                if (current.Children.ContainsKey(c))
                    current = current.Children[c];
                else
                    return null;
            }
            return current;
        }
    }
    public class TrieTests
    {
        private readonly Trie trie;

        public TrieTests()
        {
            trie = new Trie();

        }

        [Fact]
        public void when_empty_then_assert()
        {
            Assert.False(trie.Contains("empty"));
            trie.Delete("empty");
        }

        [Fact]
        public void insert_then_contains()
        {
            trie.Insert("test");
            Assert.True(trie.Contains("test"));
        }

        [Fact]
        public void insert_then_contains2()
        {
            trie.Insert("Car");
            trie.Insert("Done");
            trie.Insert("Try");
            trie.Insert("Cat");
            trie.Insert("Trie");
            trie.Insert("Do");
            Assert.False(trie.Contains("Ca"));
            Assert.False(trie.Contains("Zebra"));
            Assert.True(trie.Contains("Try"));
            Assert.False(trie.Contains("Card"));
        }

        [Fact]
        public void when_delete_then()
        {
            trie.Insert("Car");
            trie.Insert("Done");
            trie.Insert("Try");
            trie.Insert("Cat");
            trie.Insert("Trie");
            trie.Insert("Tri");
            trie.Insert("Do");
            trie.Delete("Do");
            trie.Delete("Trie");
            trie.Delete("Ca");
            trie.Delete("Qweqwe");

            Assert.False(trie.Contains("Do"));
            Assert.False(trie.Contains("Trie"));
            Assert.True(trie.Contains("Try"));
            Assert.True(trie.Contains("Done"));
            Assert.True(trie.Contains("Tri"));
        }
    }
}
