using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _350_IntersectionofTwoArraysII
    {
        public class Solution
        {
            public int[] Intersect(int[] nums1, int[] nums2)
            {
                var x = new int[1001];
                for (int i = 0; i < nums1.Length; i++)
                    x[nums1[i]]++;

                var result = new List<int>();
                for (int i = 0; i < nums2.Length; i++)
                    if (x[nums2[i]] > 0)
                    {
                        x[nums2[i]]--;
                        result.Add(nums2[i]);
                    }

                return result.ToArray();
            }
        }

        private readonly Solution s;
        public _350_IntersectionofTwoArraysII()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var arr1 = new int[] { 1, 2, 2, 1 };
            var arr2 = new int[] { 2, 2 };

            var res = s.Intersect(arr1, arr2);

            Assert.Collection(res,
                i => Assert.Equal(2, i),
                i => Assert.Equal(2, i)
                );
        }

        [Fact]
        public void example2()
        {
            var arr1 = new int[] { 4, 9, 5 };
            var arr2 = new int[] { 9, 4, 9, 8, 4 };

            var res = s.Intersect(arr1, arr2);

            Assert.Collection(res,
                i => Assert.Contains(4, res),
                i => Assert.Contains(9, res)
                );
        }

        [Fact]
        public void example3()
        {
            var arr1 = new int[] { 1 };
            var arr2 = new int[] { 2 };

            var res = s.Intersect(arr1, arr2);

            Assert.Empty(res);
        }

        [Fact]
        public void example4()
        {
            var arr1 = new int[] { 1000, 0 };
            var arr2 = new int[] { 0, 1000 };

            var res = s.Intersect(arr1, arr2);

            Assert.Contains(1000, res);
            Assert.Contains(0, res);
        }
    }
}