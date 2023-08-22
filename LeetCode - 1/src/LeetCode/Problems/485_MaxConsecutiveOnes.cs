using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _485_MaxConsecutiveOnes
    {
        public class Solution
        {
            public int FindMaxConsecutiveOnes(int[] nums)
            {
                int answer = 0;
                int counter = 0;
                foreach (var item in nums)
                    if (item == 0)
                    {
                        answer = Math.Max(answer, counter);
                        counter = 0;
                    }
                    else
                        counter++;

                answer = Math.Max(answer, counter);
                return answer;
            }
        }

        private readonly Solution s;
        public _485_MaxConsecutiveOnes()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = "[1,1,0,1,1,1]".MakeArray<int>();
            var res = s.FindMaxConsecutiveOnes(input);
            Assert.Equal(3, res);
        }

        [Fact]
        public void example2()
        {
            var input = "[1,0,1,1,0,1]".MakeArray<int>();
            var res = s.FindMaxConsecutiveOnes(input);
            Assert.Equal(2, res);
        }

        [Fact]
        public void test1()
        {
            var input = "[1]".MakeArray<int>();
            var res = s.FindMaxConsecutiveOnes(input);
            Assert.Equal(1, res);
        }

        [Fact]
        public void test2()
        {
            var input = "[0]".MakeArray<int>();
            var res = s.FindMaxConsecutiveOnes(input);
            Assert.Equal(0, res);
        }

        [Fact]
        public void test3()
        {
            var input = "[1,0,1,0,1,0,1]".MakeArray<int>();
            var res = s.FindMaxConsecutiveOnes(input);
            Assert.Equal(1, res);
        }
    }
}