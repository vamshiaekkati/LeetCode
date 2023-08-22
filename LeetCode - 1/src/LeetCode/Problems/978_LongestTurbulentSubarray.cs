using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _978_LongestTurbulentSubarray
    {
        public class Solution
        {
            public int MaxTurbulenceSize(int[] arr)
            {
                int counter1 = 1, counter2 = 1;
                int max1 = 1, max2 = 1;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (Case1(i, arr))
                        counter1++;
                    else
                    {
                        max1 = Math.Max(max1, counter1);
                        counter1 = 1;
                    }

                    if (Case2(i, arr))
                        counter2++;
                    else
                    {
                        max2 = Math.Max(max2, counter2);
                        counter2 = 1;
                    }
                }

                max1 = Math.Max(max1, counter1);
                max2 = Math.Max(max2, counter2);
                return Math.Max(max1, max2);
            }

            private bool Case1(int i, int[] arr)
            {
                if (i % 2 == 0)
                {
                    if (IsLT(i, arr))
                        return true;
                }
                else
                {
                    if (IsGT(i, arr))
                        return true;
                }
                return false;
            }

            private bool Case2(int i, int[] arr)
            {
                if (i % 2 == 0)
                {
                    if (IsGT(i, arr))
                        return true;
                }
                else
                {
                    if (IsLT(i, arr))
                        return true;
                }
                return false;
            }


            private bool IsLT(int i, int[] arr)
            {
                if (arr[i] < arr[i + 1])
                    return true;
                return false;
            }

            private bool IsGT(int i, int[] arr)
            {
                if (arr[i] > arr[i + 1])
                    return true;
                return false;
            }
        }

        private readonly Solution s;
        public _978_LongestTurbulentSubarray()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = new int[] { 9, 4, 2, 10, 7, 8, 8, 1, 9 };
            var res = s.MaxTurbulenceSize(input);
            Assert.Equal(5, res);
        }

        [Fact]
        public void example2()
        {
            var input = new int[] { 4, 8, 12, 16 };
            var res = s.MaxTurbulenceSize(input);
            Assert.Equal(2, res);
        }

        [Fact]
        public void example3()
        {
            var input = new int[] { 100 };
            var res = s.MaxTurbulenceSize(input);
            Assert.Equal(1, res);
        }

        [Fact]
        public void test1()
        {
            var input = new int[] { 100, 99 };
            var res = s.MaxTurbulenceSize(input);
            Assert.Equal(2, res);
        }

        [Fact]
        public void test2()
        {
            var input = new int[] { 99, 100 };
            var res = s.MaxTurbulenceSize(input);
            Assert.Equal(2, res);
        }

        [Fact]
        public void test3()
        {
            var input = new int[] { 1, 0, 1 };
            var res = s.MaxTurbulenceSize(input);
            Assert.Equal(3, res);
        }
    }
}