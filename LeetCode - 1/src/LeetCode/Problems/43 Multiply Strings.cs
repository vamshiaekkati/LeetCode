using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _43_Multiply_Strings
    {
        public class Solution
        {
            int[] arr;
            public string Multiply(string num1, string num2)
            {
                arr = new int[num1.Length + num2.Length];
                for (int i = num1.Length - 1, ni = 0; i >= 0; i--, ni++)
                    for (int j = num2.Length - 1, nj = 0; j >= 0; j--, nj++)
                        Mult(num1[i] - '0', ni, num2[j] - '0', nj);

                int skip = -1;
                for (int i = arr.Length - 1; i >= 0; i--)
                    if (arr[i] != 0)
                    {
                        skip = arr.Length - 1 - i;
                        break;
                    }

                if (skip == -1)
                    return "0";

                return string.Join("", arr.Reverse().Skip(skip));
            }

            private void Mult(int num1, int ni, int num2, int nj)
            {
                var t = num1 * num2;
                int i = ni + nj;
                while (t > 0)
                {
                    t += arr[i];
                    arr[i] = t % 10;
                    t /= 10;
                    i++;
                }
            }
        }

        private readonly Solution s;
        public _43_Multiply_Strings()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var num1 = "2";
            var num2 = "3";

            var res = s.Multiply(num1, num2);

            Assert.Equal("6", res);
        }

        [Fact]
        public void example2()
        {
            var num1 = "123";
            var num2 = "456";

            var res = s.Multiply(num1, num2);

            Assert.Equal("56088", res);
        }

        [Fact]
        public void test1()
        {
            var res = s.Multiply("1", "1");

            Assert.Equal("1", res);
        }

        [Fact]
        public void test2()
        {
            var res = s.Multiply("99", "99");

            Assert.Equal("9801", res);
        }

        [Fact]
        public void test3()
        {
            var res = s.Multiply("1000", "1000");
            Assert.Equal("1000000", res);
        }
    }
}