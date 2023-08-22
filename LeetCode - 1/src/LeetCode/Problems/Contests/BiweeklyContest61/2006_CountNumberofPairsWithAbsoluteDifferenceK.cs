using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems.BiweeklyContest61
{
    public class LeetCodeProblem1
    {
        public class Solution
        {
            public int CountKDifference(int[] nums, int k)
            {
                int answer = 0;
                for (int i = 0; i < nums.Length - 1; i++)
                {
                    for (int j = i + 1; j < nums.Length; j++)
                    {
                        if (Math.Abs(nums[i] - nums[j]) == k)
                            answer++;
                    }
                }
                return answer;
            }
        }

        private readonly Solution s;
        public LeetCodeProblem1()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = new int[] { 1, 2, 2, 1 };
            var res = s.CountKDifference(input, 1);
            Assert.Equal(4, res);
        }

        [Fact]
        public void example2()
        {
            var input = new int[] { 1, 3 };
            var res = s.CountKDifference(input, 3);
            Assert.Equal(0, res);
        }

        [Fact]
        public void example3()
        {
            var input = new int[] { 3, 2, 1, 5, 4 };
            var res = s.CountKDifference(input, 2);
            Assert.Equal(3, res);
        }


        [Fact]
        public void test1()
        {
            var input = new int[] { 1 };
            var res = s.CountKDifference(input, 2);
            Assert.Equal(0, res);
        }
    }
}