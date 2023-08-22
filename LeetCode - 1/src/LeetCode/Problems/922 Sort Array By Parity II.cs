using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _922_Sort_Array_By_Parity_II
    {
        public class Solution
        {
            public int[] SortArrayByParityII(int[] nums)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        if (nums[i] % 2 == 1)
                            for (int j = i + 1; j < nums.Length; j++)
                                if (nums[j] % 2 == 0)
                                {
                                    Swap(nums, i, j);
                                    break;
                                }
                    }
                    else
                    {
                        if (nums[i] % 2 == 0)
                            for (int j = i + 1; j < nums.Length; j++)
                                if (nums[j] % 2 == 1)
                                {
                                    Swap(nums, i, j);
                                    break;
                                }
                    }
                }

                return nums;
            }

            private void Swap(int[] nums, int i, int j)
            {
                int t = nums[i];
                nums[i] = nums[j];
                nums[j] = t;
            }
        }

        private readonly Solution s;
        public _922_Sort_Array_By_Parity_II()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = "[4,2,5,7]".MakeArray<int>();

            var res = s.SortArrayByParityII(input);

            Assert.Collection(res,
                i => Assert.Equal(4, i),
                i => Assert.Equal(5, i),
                i => Assert.Equal(2, i),
                i => Assert.Equal(7, i));
        }

        [Fact]
        public void example2()
        {
            var input = "[2,3]".MakeArray<int>();

            var res = s.SortArrayByParityII(input);

            Assert.Collection(res,
                i => Assert.Equal(2, i),
                i => Assert.Equal(3, i));
        }

        [Fact]
        public void example3()
        {
            var input = "[3,3,2,2]".MakeArray<int>();

            var res = s.SortArrayByParityII(input);

            Assert.Collection(res,
                i => Assert.Equal(2, i),
                i => Assert.Equal(3, i),
                i => Assert.Equal(2, i),
                i => Assert.Equal(3, i)
                );
        }
    }
}