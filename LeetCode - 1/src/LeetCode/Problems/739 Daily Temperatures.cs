using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _739_Daily_Temperatures
    {
        public class Solution
        {
            public int[] DailyTemperatures(int[] temperatures)
            {
                int[] answer = new int[temperatures.Length];

                var s = new Stack<(int i, int temp)>();
                for (int i = 0; i < temperatures.Length; i++)
                {
                    var temp = temperatures[i];
                    while (s.Count > 0 && temp > s.Peek().temp)
                    {
                        var item = s.Pop();
                        answer[item.i] = i - item.i;
                    }

                    s.Push((i, temp));
                }

                return answer;
            }
        }

        private readonly Solution s;
        public _739_Daily_Temperatures()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = "[73,74,75,71,69,72,76,73]".MakeArray<int>();

            var res = s.DailyTemperatures(input);

            Assert.Collection(res,
                i => Assert.Equal(1, i),
                i => Assert.Equal(1, i),
                i => Assert.Equal(4, i),
                i => Assert.Equal(2, i),
                i => Assert.Equal(1, i),
                i => Assert.Equal(1, i),
                i => Assert.Equal(0, i),
                i => Assert.Equal(0, i));
        }

        [Fact]
        public void example2()
        {
            var input = "[30,40,50,60]".MakeArray<int>();

            var res = s.DailyTemperatures(input);

            Assert.Collection(res,
                i => Assert.Equal(1, i),
                i => Assert.Equal(1, i),
                i => Assert.Equal(1, i),
                i => Assert.Equal(0, i));
        }

        [Fact]
        public void example3()
        {
            var input = "[30,60,90]".MakeArray<int>();

            var res = s.DailyTemperatures(input);

            Assert.Collection(res,
                i => Assert.Equal(1, i),
                i => Assert.Equal(1, i),
                i => Assert.Equal(0, i));
        }

        [Fact]
        public void test1()
        {
            var input = "[30]".MakeArray<int>();

            var res = s.DailyTemperatures(input);

            Assert.Collection(res, i => Assert.Equal(0, i));
        }
    }
}