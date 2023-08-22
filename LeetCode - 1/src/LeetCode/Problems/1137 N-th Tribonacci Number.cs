using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _1137_N_th_Tribonacci_Number
    {
        public class Solution
        {
            public int Tribonacci(int n)
            {
                int[] tribs = new int[38];
                tribs[0] = 0;
                tribs[1] = 1;
                tribs[2] = 1;

                for (int i = 3; i <= n; i++)
                    tribs[i] = tribs[i - 1] + tribs[i - 2] + tribs[i - 3];

                return tribs[n];
            }
        }

        private readonly Solution s;
        public _1137_N_th_Tribonacci_Number()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = 4;

            var res = s.Tribonacci(input);

            Assert.Equal(4, res);
        }

        [Fact]
        public void example2()
        {
            var input = 25;

            var res = s.Tribonacci(input);

            Assert.Equal(1389537, res);
        }

        [Fact]
        public void test1()
        {
            var input = 0;
            var res = s.Tribonacci(input);
            Assert.Equal(0, res);
        }

        [Fact]
        public void test2()
        {
            var input = 1;
            var res = s.Tribonacci(input);
            Assert.Equal(1, res);
        }

        [Fact]
        public void test3()
        {
            var input = 2;
            var res = s.Tribonacci(input);
            Assert.Equal(1, res);
        }

        [Fact]
        public void test4()
        {
            var input = 37;
            var res = s.Tribonacci(input);
            Assert.True(res > 0);
        }
    }
}