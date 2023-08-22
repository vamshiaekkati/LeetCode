using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _446_ArithmeticSlices2Subsequence
    {
        public class Solution
        {
            public int NumberOfArithmeticSlices(int[] nums)
            {
                long answer = 0;
                List<Dictionary<long, int>> diffs = new List<Dictionary<long, int>>();
                for (int j = 0; j < nums.Length; j++)
                {
                    int num2 = nums[j];
                    diffs.Add(new Dictionary<long, int>());
                    for (int i = 0; i < j; i++)
                    {
                        int num1 = nums[i];
                        long diff = (long)num2 - num1;
                        int curr = diffs[i].ContainsKey(diff) ? diffs[i][diff] : 0;
                        int prev = diffs[j].ContainsKey(diff) ? diffs[j][diff] : 0;
                        diffs[j][diff] = prev + 1 + curr;
                        answer += curr;
                    }
                }

                return (int)answer;
            }
        }

        private readonly Solution s;
        public _446_ArithmeticSlices2Subsequence()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = new int[] { 2, 4, 6, 8, 10 };

            var res = s.NumberOfArithmeticSlices(input);

            Assert.Equal(7, res);
        }

        [Fact]
        public void example2()
        {
            var input = new int[] { 7, 7, 7, 7, 7 };

            var res = s.NumberOfArithmeticSlices(input);

            Assert.Equal(16, res);
        }

        [Fact]
        public void test1()
        {
            var input = new int[] { 2, 4, 6, 2, 4, 6 };

            var res = s.NumberOfArithmeticSlices(input);

            Assert.Equal(4, res);
        }

        [Fact]
        public void test2()
        {
            var input = new int[] { 2, 4, 6, 2, 4, 6, 8 };

            var res = s.NumberOfArithmeticSlices(input);

            Assert.Equal(11, res);
        }

        [Fact]
        public void test3()
        {
            var input = Enumerable.Range(1, 1000).ToArray();

            var res = s.NumberOfArithmeticSlices(input);

            Assert.Equal(2781846, res);
        }

        [Fact]
        public void test4()
        {
            var pow = (1 << 31);
            var input = new int[] { -pow, -pow + 1, -pow + 2, pow - 1 };

            var res = s.NumberOfArithmeticSlices(input);

            Assert.Equal(1, res);
        }
    }
}