using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _725_Split_Linked_List_in_Parts
    {
        public class Solution
        {
            public ListNode[] SplitListToParts(ListNode head, int k)
            {
                var result = new ListNode[k];

                var node = head;
                int nodes = 0;
                while (node != null)
                {
                    node = node.next;
                    nodes++;
                }

                int nodesMin = nodes / k;
                int countNodesMinPlus1 = nodes % k;
                var next = head;
                for (int i = 0; i < countNodesMinPlus1; i++)
                {
                    var add = next;
                    for (int j = 0; j < nodesMin; j++)
                        head = head.next;
                    next = head.next;
                    head.next = null;
                    result[i] = add;
                    head = next;
                }

                for (int i = countNodesMinPlus1; i < k; i++)
                {
                    var add = next;
                    var skipper = next;
                    for (int j = 0; j < nodesMin - 1; j++)
                        skipper = skipper.next;
                    if (skipper != null)
                    {
                        next = skipper.next;
                        skipper.next = null;
                    }
                    result[i] = add;
                }

                return result;
            }
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        private readonly Solution s;
        public _725_Split_Linked_List_in_Parts()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = "[1,2,3]".MakeArray<int>();

            var res = s.SplitListToParts(ToLL(input), 5);

            Assert.Collection(res,
                i => IsLL(i, 1),
                i => IsLL(i, 2),
                i => IsLL(i, 3),
                i => IsLL(i),
                i => IsLL(i));
        }

        private void IsLL(ListNode l, params int[] arr)
        {
            var node = l;
            for (int i = 0; i < arr.Length; i++)
            {
                Assert.NotNull(node);
                Assert.Equal(arr[i], node.val);
                node = node.next;
            }
            Assert.Null(node);
        }

        private ListNode ToLL(int[] input)
        {
            if (input.Length == 0)
                return null;
            var head = new ListNode(input[0]);
            var node = head;
            for (int i = 1; i < input.Length; i++)
            {
                node.next = new ListNode(input[i]);
                node = node.next;
            }
            return head;
        }

        [Fact]
        public void example2()
        {
            var input = "[1,2,3,4,5,6,7,8,9,10]".MakeArray<int>();

            var res = s.SplitListToParts(ToLL(input), 3);

            Assert.Collection(res,
                i => IsLL(i, 1, 2, 3, 4),
                i => IsLL(i, 5, 6, 7),
                i => IsLL(i, 8, 9, 10));
        }

        [Fact]
        public void test1()
        {
            var input = "[]".MakeArray<int>();

            var res = s.SplitListToParts(ToLL(input), 3);

            Assert.Collection(res, i => IsLL(i), i => IsLL(i), i => IsLL(i));
        }

        [Fact]
        public void fail1()
        {
            var input = "[0,1,2]".MakeArray<int>();
            var res = s.SplitListToParts(ToLL(input), 3);

            Assert.Collection(res, 
                i => IsLL(i, 0),
                i => IsLL(i, 1),
                i => IsLL(i, 2));
        }
    }
}