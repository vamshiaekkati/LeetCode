using Xunit;

namespace LeetCode.Problems
{
    public class _2_AddTwoNumbers
    {
        private readonly Solution s;

        public _2_AddTwoNumbers()
        {
            s = new Solution();
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

        public class Solution
        {
            public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
            {
                var sum = GetSum(l1, l2, 0);
                var sumMod10 = sum % 10;
                var carried = sum / 10;
                var l3 = new ListNode(sumMod10);
                var result = l3;
                while (l1.next != null)
                    PopulateL3(ref l1, ref l2, ref carried, ref l3);

                if (l2 != null)
                    while (l2.next != null)
                        PopulateL3(ref l1, ref l2, ref carried, ref l3);

                if (carried > 0)
                    l3.next = new ListNode(carried);

                return result;
            }

            private static void PopulateL3(ref ListNode l1, ref ListNode l2, ref int carried, ref ListNode l3)
            {
                if (l1 != null)
                    l1 = l1.next;
                if (l2 != null)
                    l2 = l2.next;
                int sum = GetSum(l1, l2, carried);
                int sumMod10 = sum % 10;
                carried = sum / 10;

                l3.next = new ListNode(sumMod10);
                l3 = l3.next;
            }

            private static int GetSum(ListNode l1, ListNode l2, int sumDiv10)
            {
                var v1 = l1 == null ? 0 : l1.val;
                var v2 = l2 == null ? 0 : l2.val;
                var sum = v1 + v2 + sumDiv10;
                return sum;
            }
        }

        [Fact]
        public void create_list()
        {
            var list = CreateList(1, 2);

            CheckList(list, 1, 2);
        }

        [Fact]
        public void example_1()
        {
            var l1 = CreateList(2, 4, 3);
            var l2 = CreateList(5, 6, 4);

            var res = s.AddTwoNumbers(l1, l2);

            CheckList(res, 7, 0, 8);
        }

        [Fact]
        public void example_1_reverse()
        {
            var l1 = CreateList(5, 6, 4);
            var l2 = CreateList(2, 4, 3);

            var res = s.AddTwoNumbers(l1, l2);

            CheckList(res, 7, 0, 8);
        }



        [Fact]
        public void example_2()
        {
            var l1 = CreateList(0);
            var l2 = CreateList(0);

            var res = s.AddTwoNumbers(l1, l2);

            CheckList(res, 0);
        }

        [Fact]
        public void example_3()
        {
            var l1 = CreateList(9, 9, 9, 9, 9, 9, 9);
            var l2 = CreateList(9, 9, 9, 9);

            var res = s.AddTwoNumbers(l1, l2);

            CheckList(res, 8, 9, 9, 9, 0, 0, 0, 1);
        }

        [Fact]
        public void example_3_reverse()
        {
            var l1 = CreateList(9, 9, 9, 9);
            var l2 = CreateList(9, 9, 9, 9, 9, 9, 9);

            var res = s.AddTwoNumbers(l1, l2);

            CheckList(res, 8, 9, 9, 9, 0, 0, 0, 1);
        }

        [Fact]
        public void test_1()
        {
            var l1 = CreateList(0, 1);
            var l2 = CreateList(1);

            var res = s.AddTwoNumbers(l1, l2);

            CheckList(res, 1, 1);
        }

        [Fact]
        public void test_1_reverse()
        {
            var l1 = CreateList(1);
            var l2 = CreateList(0, 1);

            var res = s.AddTwoNumbers(l1, l2);

            CheckList(res, 1, 1);
        }

        [Fact]
        public void test_2()
        {
            var l1 = CreateList(9);
            var l2 = CreateList(1);

            var res = s.AddTwoNumbers(l1, l2);

            CheckList(res, 0, 1);
        }


        private ListNode CreateList(params int[] nums)
        {
            ListNode l = null;
            for (var i = nums.Length - 1; i >= 0; i--)
                if (l is null)
                    l = new ListNode(nums[i], null);
                else
                    l = new ListNode(nums[i], l);
            return l;
        }

        private static void CheckList(ListNode node, params int[] nums)
        {
            foreach (var num in nums)
            {
                Assert.Equal(num, node.val);
                node = node.next;
            }
            Assert.Null(node);
        }
    }
}
