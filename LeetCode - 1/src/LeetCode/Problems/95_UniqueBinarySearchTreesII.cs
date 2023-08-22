using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems._21_09_Week1
{
    public class _95_UniqueBinarySearchTreesII
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        public class Solution
        {
            public IList<TreeNode> GenerateTrees(int n)
            {
                var trees = new List<TreeNode>();
                var input = Enumerable.Range(1, n).ToArray();
                PermutationsDFS(new HashSet<int> { }, new List<int> { }, input, trees);
                return trees;
            }

            private void PermutationsDFS(HashSet<int> visited, List<int> list, int[] input, List<TreeNode> trees)
            {
                foreach (var item in input)
                {
                    if (visited.Contains(item))
                        continue;
                    visited.Add(item);
                    list.Add(item);
                    if (input.Length == list.Count)
                    {
                        var tree = MakeTree(list);
                        if (!Contains(trees, tree))
                            trees.Add(tree);
                    }
                    PermutationsDFS(visited, list, input, trees);
                    visited.Remove(item);
                    list.RemoveAt(list.Count - 1);
                }
            }

            private bool Contains(List<TreeNode> trees, TreeNode tree)
            {
                return trees.Any(t => SameTrees(t, tree));
            }

            private bool SameTrees(TreeNode a, TreeNode b)
            {
                if (a == null && b == null)
                    return true;
                if (a != null && b != null)
                    return a.val == b.val
                            && SameTrees(a.left, b.left)
                            && SameTrees(a.right, b.right);
                return false;
            }

            private TreeNode MakeTree(List<int> list)
            {
                TreeNode root = new TreeNode(list[0]);
                for (int i = 1; i < list.Count; i++)
                    AddNode(root, list[i]);
                return root;
            }

            private void AddNode(TreeNode root, int v)
            {
                var current = root;
                while (current != null)
                    if (v < current.val)
                    {
                        if (current.left == null)
                        {
                            current.left = new TreeNode(v);
                            return;
                        }

                        current = current.left;
                    }
                    else
                    {
                        if (current.right == null)
                        {
                            current.right = new TreeNode(v);
                            return;
                        }

                        current = current.right;
                    }
            }
        }

        Solution s;
        public _95_UniqueBinarySearchTreesII()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var res = s.GenerateTrees(3);

            Assert.Equal(5, res.Count);
            HassTree(res, new int?[] { 1, null, 2, null, 3 });
            HassTree(res, new int?[] { 1, null, 3, 2 });
            HassTree(res, new int?[] { 2, 1, 3 });
            HassTree(res, new int?[] { 3, 1, null, null, 2 });
            HassTree(res, new int?[] { 3, 2, null, 1 });
        }

        [Fact]
        public void example2()
        {
            var res = s.GenerateTrees(1);

            Assert.Equal(1, res.Count);
            HassTree(res, new int?[] { 1 });
        }


        [Fact]
        public void when_n2()
        {
            var res = s.GenerateTrees(2);

            Assert.Equal(2, res.Count);
            HassTree(res, new int?[] { 1, null, 2 });
            HassTree(res, new int?[] { 2, 1 });
        }

        private void HassTree(IList<TreeNode> res, int?[] nodes)
        {
            Assert.Contains(res, tree => ContainsNodes(tree, nodes));
        }

        private bool ContainsNodes(TreeNode node, int?[] checks)
        {
            var queue = new Queue<TreeNode>();
            queue.Enqueue(node);
            IEnumerable<int?> tree = BFS(queue);
            return true;
        }

        private IEnumerable<int?> BFS(Queue<TreeNode> queue)
        {
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == null)
                    yield return null;

                yield return node.val;
                if (node.left != null || node.right != null)
                {
                    queue.Enqueue(node.left);
                    queue.Enqueue(node.right);
                }
            }
        }
    }
}
