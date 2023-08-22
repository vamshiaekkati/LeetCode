using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _917_ReverseOnlyLetters
    {
        public class Solution
        {
            public string ReverseOnlyLetters(string s)
            {
                int i = 0, j = s.Length - 1;
                char[] charArray = s.ToArray();
                while (i < j)
                {
                    while (!isReversable(s[i]) && i < j)
                        i++;
                    while (!isReversable(s[j]) && i < j)
                        j--;
                    swap(charArray, i, j);
                    i++; j--;
                }

                return new string(charArray);
            }

            private void swap(char[] s, int i, int j)
            {
                char t = s[i];
                s[i] = s[j];
                s[j] = t;
            }

            private bool isReversable(char v)
            {
                if ((v >= 'a' && v <= 'z') || (v >= 'A' && v <= 'Z'))
                    return true;
                return false;
            }
        }

        private readonly Solution s;
        public _917_ReverseOnlyLetters()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = "ab-cd";
            var res = s.ReverseOnlyLetters(input);
            Assert.Equal("dc-ba", res);
        }

        [Fact]
        public void example2()
        {
            var input = "a-bC-dEf-ghIj";
            var res = s.ReverseOnlyLetters(input);
            Assert.Equal("j-Ih-gfE-dCba", res);
        }

        [Fact]
        public void example3()
        {
            var input = "Test1ng-Leet=code-Q!";
            var res = s.ReverseOnlyLetters(input);
            Assert.Equal("Qedo1ct-eeLg=ntse-T!", res);
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("ab", "ba")]
        [InlineData("abc", "cba")]
        [InlineData("-bc", "-cb")]
        [InlineData("!@#", "!@#")]
        public void test1(string input, string expect)
        {
            var res = s.ReverseOnlyLetters(input);
            Assert.Equal(expect, res);
        }
    }
}