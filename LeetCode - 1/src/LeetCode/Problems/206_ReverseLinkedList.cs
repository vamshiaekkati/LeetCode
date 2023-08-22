using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _206_ReverseLinkedList
    {
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

        public class Solution
        {
            public ListNode ReverseList(ListNode head)
            {
                if (head == null)
                    return null;

                if (head.next == null)
                    return head;

                ListNode last;
                var current = head;
                while (current.next != null)
                    current = current.next;
                last = current;
                current = head;
                while (head.next != null)
                {
                    if (current.next.next == null)
                    {
                        var next = current.next;
                        next.next = current;
                        current.next = null;
                        current = head;
                    }
                    else current = current.next;
                }

                return last;
            }
        }

        public class Solution_Recursion
        {
            public ListNode ReverseList(ListNode head)
            {
                if (head == null)
                    return null;

                var reversed = Reverse(head);
                reversed.next = null;
                return last;
            }

            ListNode last;
            private ListNode Reverse(ListNode current)
            {
                var next = current.next;
                if (next == null)
                {
                    last = current;
                    return current;
                }

                var node = Reverse(next);
                node.next = current;
                return current;
            }
        }

        private readonly Solution s;
        public _206_ReverseLinkedList()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = new int[] { 1, 2, 3, 4, 5 };

            var res = s.ReverseList(ToLinkedList(input));

            Assert.Collection(ToArray(res),
                i => Assert.Equal(5, i),
                i => Assert.Equal(4, i),
                i => Assert.Equal(3, i),
                i => Assert.Equal(2, i),
                i => Assert.Equal(1, i)
                );
        }

        private ListNode ToLinkedList(int[] array)
        {
            var first = new ListNode(array[0]);
            var current = first;
            for (int i = 1; i < array.Length; i++)
            {
                current.next = new ListNode(array[i]);
                current = current.next;
            }
            return first;
        }

        private IEnumerable<int> ToArray(ListNode list)
        {
            var current = list;
            while (current != null)
            {
                yield return current.val;
                current = current.next;
            }
        }

        [Fact]
        public void example2()
        {
            var input = new int[] { 1, 2 };

            var res = s.ReverseList(ToLinkedList(input));

            Assert.Collection(ToArray(res),
                i => Assert.Equal(2, i),
                i => Assert.Equal(1, i)
                );
        }

        [Fact]
        public void example3()
        {
            var res = s.ReverseList(null);

            Assert.Null(res);
        }

        [Fact]
        public void test1()
        {
            var input = new int[] { 1 };

            var res = s.ReverseList(ToLinkedList(input));

            Assert.Collection(ToArray(res),
                i => Assert.Equal(1, i)
                );
        }
    }
}