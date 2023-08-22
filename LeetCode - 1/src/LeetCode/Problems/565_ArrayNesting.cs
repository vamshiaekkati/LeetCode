using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeetCode.Problems._21_09_Week1
{
    public class _565_ArrayNesting
    {
        public class Solution
        {
            public int ArrayNesting(int[] nums)
            {
                var visited = new HashSet<int>();

                int path;
                int maxPath = 0;
                foreach (var num in nums)
                {
                    path = 0;
                    if (visited.Contains(num))
                        continue;

                    visited.Add(num);
                    path++;
                    int nextNum = nums[num];
                    while (!visited.Contains(nextNum))
                    {
                        visited.Add(nextNum);
                        path++;
                        nextNum = nums[nextNum];
                    }

                    maxPath = path > maxPath ? path : maxPath;
                }

                return maxPath;
            }
        }

        private Solution s;
        public _565_ArrayNesting()
        {
            s = new Solution();
        }

        [Theory]
        [InlineData(new int[] { 5, 4, 0, 3, 1, 6, 2 }, 4)]
        [InlineData(new int[] { 0, 1, 2 }, 1)]
        [InlineData(new int[] { 0 }, 1)]
        [InlineData(new int[] { 1, 2, 3, 0 }, 4)]
        [InlineData(new int[] { 2, 0, 3, 1 }, 4)]
        public void when2(int[] arr, int expect)
        {
            var res = s.ArrayNesting(arr);

            Assert.Equal(expect, res);
        }
    }
}
