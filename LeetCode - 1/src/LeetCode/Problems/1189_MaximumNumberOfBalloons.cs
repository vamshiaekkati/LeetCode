using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _1189_MaximumNumberOfBalloons
    {
        public class Solution
        {
            public int MaxNumberOfBalloons(string text)
            {
                string one = "ban";
                string two = "lo";
                int[] charCounts = new int['z' - 'a' + 1];
                foreach (char c in text)
                    charCounts[c - 'a']++;

                int answer = int.MaxValue;
                foreach (char c in one)
                    answer = Math.Min(answer, charCounts[c - 'a']);
                foreach (char c in two)
                    answer = Math.Min(answer, charCounts[c - 'a'] / 2);
                return answer;
            }
        }

        private readonly Solution s;
        public _1189_MaximumNumberOfBalloons()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = "nlaebolko";

            var res = s.MaxNumberOfBalloons(input);

            Assert.Equal(1, res);
        }

        [Fact]
        public void example2()
        {
            var input = "loonbalxballpoon";

            var res = s.MaxNumberOfBalloons(input);

            Assert.Equal(2, res);
        }

        [Fact]
        public void example3()
        {
            var input = "leetcode";

            var res = s.MaxNumberOfBalloons(input);

            Assert.Equal(0, res);
        }

        [Fact]
        public void test1()
        {
            var input = "abcdefghijklmnopqrstuvwxyz";

            var res = s.MaxNumberOfBalloons(input);

            Assert.Equal(0, res);
        }

        [Fact]
        public void test2()
        {
            var input = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz";

            var res = s.MaxNumberOfBalloons(input);

            Assert.Equal(1, res);
        }
    }
}