using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Interview.InnRoad
{
    public class Problem1
    {
        public class Solution
        {
            internal IEnumerable<Tuple<int, int>> Solve(int[] array1, int[] array2)
            {
                var array2List = array2.ToList();
                array2List.Sort();
                foreach (var item in array1)
                {
                    var index = array2List.BinarySearch(-item);
                    if (index >= 0)
                        yield return new Tuple<int, int>(item, array2List[index]);
                }
            }
        }

        private readonly Solution s;

        // 2 arrays on integers 
        // any length, no sorting
        // find a subset of pairs
        // sum of pair is 0 (one element from array 1, one from array 2)
        public Problem1()
        {
            s = new Solution();
        }

        [Fact]
        public void test1()
        {
            var array1 = new int[] { 1 };
            var array2 = new int[] { -1 };

            var result = s.Solve(array1, array2);

            Assert.Collection(result,
                r => Assert.Equal(r, new Tuple<int, int>(1, -1)));
        }

        [Fact]
        public void test2()
        {
            var array1 = new int[] { 1, 2, 3, 2 };
            var array2 = new int[] { -1, -2 };

            var result = s.Solve(array1, array2);
            Assert.Collection(result,
                r => Assert.Equal(r, new Tuple<int, int>(1, -1)),
                r => Assert.Equal(r, new Tuple<int, int>(2, -2)),
                r => Assert.Equal(r, new Tuple<int, int>(2, -2)));
        }
    }
}
