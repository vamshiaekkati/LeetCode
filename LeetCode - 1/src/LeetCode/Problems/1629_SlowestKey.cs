using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _1629_SlowestKey
    {
        public class Solution
        {
            public char SlowestKey(int[] releaseTimes, string keysPressed)
            {
                char answer = keysPressed[0];
                int maxTime = releaseTimes[0];

                for (int i = 1; i < releaseTimes.Length; i++)
                {
                    var time = releaseTimes[i] - releaseTimes[i - 1];
                    if (time < maxTime)
                        continue;
                    var key = keysPressed[i];
                    if (time == maxTime)
                    {
                        if (key > answer)
                            answer = key;
                        continue;
                    }

                    maxTime = time;
                    answer = key;
                }
                return answer;
            }
        }

        private readonly Solution s;
        public _1629_SlowestKey()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var releaseTimes = new int[] { 9, 29, 49, 50 };
            var res = s.SlowestKey(releaseTimes, "cbcd");
            Assert.Equal('c', res);
        }

        [Fact]
        public void example2()
        {
            var releaseTimes = new int[] { 12,23,36,46,62 };
            var res = s.SlowestKey(releaseTimes, "spuda");
            Assert.Equal('a', res);
        }

        [Fact]
        public void test1()
        {
            var releaseTimes = new int[] { (int)1e9-1, (int)1e9 };
            var res = s.SlowestKey(releaseTimes, "aa");
            Assert.Equal('a', res);
        }

        [Fact]
        public void test2()
        {
            var releaseTimes = new int[] { 1,3,4,5,6,7,8,10 };
            var res = s.SlowestKey(releaseTimes, "bbcdhfgh");
            Assert.Equal('h', res);
        }
    }
}