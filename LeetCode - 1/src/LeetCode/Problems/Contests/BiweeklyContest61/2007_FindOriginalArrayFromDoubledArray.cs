using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems.BiweeklyContest61
{
    public class LeetCodeProblem2
    {
        public class Solution
        {
            public int[] FindOriginalArray(int[] changed)
            {
                var answer = new List<int>();
                if (changed.Length % 2 != 0)
                    return answer.ToArray();

                int length = changed.Length;
                Array.Sort(changed);
                var dict = new Dictionary<int, int>();
                var dictAnswer = new Dictionary<int, int>();
                foreach (var item in changed)
                {
                    if (dict.ContainsKey(item))
                        dict[item]++;
                    else
                        dict[item] = item;
                }

                for (int i = length - 1; i >= 0; i--)
                {
                    int bignum = changed[i];
                    if (dictAnswer.ContainsKey(bignum) && dictAnswer[bignum] > 0)
                    {
                        dictAnswer[bignum]--;
                        continue;
                    }

                    if (bignum % 2 > 0)
                        return new int[] { };

                    if (dict.ContainsKey(bignum / 2))
                    {
                        int halfbig = bignum / 2;
                        if (!dictAnswer.ContainsKey(halfbig))
                            dictAnswer[halfbig] = 0;
                        dictAnswer[halfbig] += 1;
                        answer.Add(halfbig);
                    }
                    else
                        return new int[] { };
                }

                return answer.ToArray();
            }
        }

        private readonly Solution s;
        public LeetCodeProblem2()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = new int[] { 1, 3, 4, 2, 6, 8 };
            var res = s.FindOriginalArray(input);
            Assert.Equal(3, res.Length);
            Assert.Contains(1, res);
            Assert.Contains(3, res);
            Assert.Contains(4, res);
        }

        [Fact]
        public void example2()
        {
            var input = new int[] { 6, 3, 0, 1 };
            var res = s.FindOriginalArray(input);
            Assert.Empty(res);
        }

        [Fact]
        public void example3()
        {
            var input = new int[] { 1 };
            var res = s.FindOriginalArray(input);
            Assert.Empty(res);
        }

        [Fact]
        public void test1()
        {
            var input = new int[] { 1, 2, 4, 8 };
            var res = s.FindOriginalArray(input);
            Assert.Equal(2, res.Length);
            Assert.Contains(1, res);
            Assert.Contains(4, res);
        }

        [Fact]
        public void test2()
        {
            var input = new int[] { 4, 4, 2, 2 };
            var res = s.FindOriginalArray(input);
            Assert.Equal(2, res.Length);
            Assert.Contains(2, res);
            Assert.Contains(2, res);
        }
    }
}