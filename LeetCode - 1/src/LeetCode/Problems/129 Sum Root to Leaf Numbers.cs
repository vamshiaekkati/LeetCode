using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LeetCode.Problems
{
    public class _129_Sum_Root_to_Leaf_Numbers
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
            private StringBuilder sb;
            private int answer;

            public int SumNumbers(TreeNode root)
            {
                sb = new StringBuilder();
                answer = 0;
                dfs(root);
                return answer;
            }

            private void dfs(TreeNode node)
            {
                if (node == null)
                    return;

                sb.Append(node.val);

                if(node.left == null && node.right == null)
                {
                    answer += int.Parse(sb.ToString());
                    sb.Remove(sb.Length - 1, 1);
                    return;
                }

                dfs(node.left);
                dfs(node.right);

                sb.Remove(sb.Length - 1, 1);
            }
        }

        private readonly Solution s;
        public _129_Sum_Root_to_Leaf_Numbers()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = new TreeNode(1, new TreeNode(2), new TreeNode(3));

            var res = s.SumNumbers(input);

            Assert.Equal(25, res);
        }

        [Fact]
        public void example2()
        {
            var input = new TreeNode(4, 
                new TreeNode(9, 
                    new TreeNode(5), 
                    new TreeNode(1)), 
                new TreeNode(0));

            var res = s.SumNumbers(input);

            Assert.Equal(1026, res);
        }

        [Fact]
        public void test1()
        {
            var input = new TreeNode(0);
            var res = s.SumNumbers(input);
            Assert.Equal(0, res);
        }

        [Fact]
        public void test2()
        {
            var input = new TreeNode(0,
                new TreeNode(0),
                new TreeNode(1));
            var res = s.SumNumbers(input);
            Assert.Equal(1, res);
        }
    }
}