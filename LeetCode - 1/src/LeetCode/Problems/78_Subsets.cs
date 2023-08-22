using System.Collections.Generic;
using Xunit;

namespace LeetCode.Problems
{
    public class _78_Subsets
    {
        public IList<IList<int>> Solve(int[] nums)
        {
            var result = new List<IList<int>>();
            result.Add(new List<int> { });
            for (int i = 0; i < (1 << nums.Length) - 1; i++)
            {
                var bitIndexes = toBitIndexes(i + 1);
                var subset = new List<int>();
                foreach (var index in bitIndexes)
                    subset.Add(nums[index]);
                result.Add(subset);
            }
            return result;
        }

        private List<int> toBitIndexes(int bitmask)
        {
            var subset = new List<int>();
            int i = 0;
            while (bitmask > 0)
            {
                if ((bitmask & 1) > 0)
                    subset.Add(i);
                bitmask >>= 1;
                i++;
            }
            return subset;
        }
    }

    public class SubsetsTests
    {
        private readonly _78_Subsets s;

        public SubsetsTests()
        {
            s = new _78_Subsets();
        }

        [Fact]
        public void when()
        {
            var nums = new[] { 1, 2, 3 };

            IList<IList<int>> result = s.Solve(nums);

            Assert.Equal(8, result.Count);
            Assert.Collection(result,
                i => Assert.Empty(i),
                i => Assert.Equal(new[] { 1 }, i),
                i => Assert.Equal(new[] { 2 }, i),
                i => Assert.Equal(new[] { 1, 2 }, i),
                i => Assert.Equal(new[] { 3 }, i),
                i => Assert.Equal(new[] { 1, 3 }, i),
                i => Assert.Equal(new[] { 2, 3 }, i),
                i => Assert.Equal(new[] { 1, 2, 3 }, i)
                );
        }


        [Fact]
        public void when2()
        {
            var nums = new[] { 0 };

            IList<IList<int>> result = s.Solve(nums);

            Assert.Equal(new int[][] { new int[] { }, new[] { 0 } }, result);
        }
    }
}
