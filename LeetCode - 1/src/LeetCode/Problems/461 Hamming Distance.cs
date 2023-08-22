using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _461_Hamming_Distance
    {
        public class Solution
        {
            public int HammingDistance(int x, int y)
            {
                if (y > x)
                {
                    int t = x;
                    x = y;
                    y = t;
                }

                int answer = 0, x1, y1;
                while (x > 0)
                {
                    x1 = x & 1;
                    y1 = y & 1;
                    if (x1 != y1)
                        answer++;
                    x >>= 1;
                    y >>= 1;
                }
                return answer;
            }
        }

        private readonly Solution s;
        public _461_Hamming_Distance()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var res = s.HammingDistance(1, 4);

            Assert.Equal(2, res);
        }

        [Fact]
        public void example2()
        {
            var res = s.HammingDistance(3, 1);
            Assert.Equal(1, res);
        }

        [Fact]
        public void test1()
        {
            var res = s.HammingDistance(4, 9);
            Assert.Equal(3, res);
        }

        [Fact]
        public void test2()
        {
            var res = s.HammingDistance(0, 15);
            Assert.Equal(4, res);
        }
    }
}