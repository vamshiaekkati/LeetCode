using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace TestProject1
{
    public class _31_NextPermutation
    {
        private readonly Solution solution;

        public class Solution
        {
            public string Solver(int[] nums)
            {
                return $"[{String.Join(',', Solve(nums))}]";
            }

            public IEnumerable<int> Solve(int[] nums)
            {
                int j = -1;
                for (int i = nums.Length - 1; i > 0; i--)
                    if (nums[i - 1] < nums[i])
                    {
                        j = i - 1;
                        for (int k = j + 1; k < nums.Length; k++)
                            if (nums[j] < nums[k])
                                i = k;
                        swap(nums, i, j);
                        break;
                    }
                Reverse(nums, j + 1);
                return nums;
            }

            public void Reverse(int[] nums, int start = 0)
            {
                int end = nums.Length - 1;
                while (start < end)
                    swap(nums, start++, end--);
            }

            private void swap(int[] nums, int k, int v)
            {
                var temp = nums[k];
                nums[k] = nums[v];
                nums[v] = temp;
            }
        }

        public _31_NextPermutation()
        {
            solution = new Solution();
        }

        public class Data : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new int[] { 1 }, "[1]" };
                yield return new object[] { new int[] { 1, 2, 3 }, "[1,3,2]" };
                yield return new object[] { new int[] { 1, 3, 2 }, "[2,1,3]" };
                yield return new object[] { new int[] { 2, 1, 3 }, "[2,3,1]" };
                yield return new object[] { new int[] { 2, 3, 1 }, "[3,1,2]" };
                yield return new object[] { new int[] { 3, 1, 2 }, "[3,2,1]" };
                yield return new object[] { new int[] { 3, 2, 1 }, "[1,2,3]" };
                yield return new object[] { new int[] { 2, 1 }, "[1,2]" };
                yield return new object[] { new int[] { 4, 3, 2, 1 }, "[1,2,3,4]" };
                yield return new object[] { new int[] { 1, 1, 5, 6 }, "[1,1,6,5]" };
                yield return new object[] { new int[] { 1, 1, 6, 5 }, "[1,5,1,6]" };
                yield return new object[] { new int[] { 1, 1, 6, 5, 4 }, "[1,4,1,5,6]" };
                yield return new object[] { new int[] { 1, 1, 6, 5, 4, 1 }, "[1,4,1,1,5,6]" };
                yield return new object[] { new int[] { 1, 1, 5 }, "[1,5,1]" };
                yield return new object[] { new int[] { 4, 2, 0, 2, 3, 2, 0 }, "[4,2,0,3,0,2,2]" };
                yield return new object[] { new int[] { 2, 3, 1, 3, 3 }, "[2,3,3,1,3]" };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(Data))]
        public void Theory(int[] input, string output)
        {
            var result = solution.Solver(input);
            Assert.Equal(output, result);
        }

        [Fact]
        public void Solver()
        {
            var result = solution.Solver(new int[] { 1, 2, 3 });
            Assert.Equal("[1,3,2]", result);
        }

        [Fact]
        public void reverse()
        {
            var arr = new int[] { 4, 2, 7, 5 };
            solution.Reverse(arr, 1);

            var exp = new int[] { 4, 5, 7, 2 };
            Assert.Equal(exp, arr);
        }

        [Fact]
        public void reverse3()
        {
            var arr = new int[] { 4, 2 };
            solution.Reverse(arr);

            var exp = new int[] { 2, 4 };
            Assert.Equal(exp, arr);
        }

        [Fact]
        public void reverse4()
        {
            var arr = new int[] { 1, 2, 3 };
            solution.Reverse(arr);

            var exp = new int[] { 3, 2, 1 };
            Assert.Equal(exp, arr);
        }

        [Fact]
        public void reverse5()
        {
            var arr = new int[] { 1, 2, 3 };
            solution.Reverse(arr, 1);

            var exp = new int[] { 1, 3, 2 };
            Assert.Equal(exp, arr);
        }
    }
}
