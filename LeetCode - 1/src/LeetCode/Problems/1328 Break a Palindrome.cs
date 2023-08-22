using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LeetCode.Problems
{
    public class _1328_Break_a_Palindrome
    {
        public class Solution
        {
            public string BreakPalindrome(string palindrome)
            {
                if (palindrome.Length == 1)
                    return "";

                int notAs = 0;
                int firstNotAIndex = -1;
                for (int i = 0; i < palindrome.Length; i++)
                    if (palindrome[i] != 'a')
                    {
                        notAs++;
                        if (firstNotAIndex == -1)
                            firstNotAIndex = i;
                        if (notAs > 1)
                            break;
                    }

                if (notAs > 1)
                    return ReplaceAtTo(palindrome, firstNotAIndex, 'a');
                else
                    return ReplaceAtTo(palindrome, palindrome.Length-1, 'b');
            }

            private string ReplaceAtTo(string palindrome, int at, char to)
            {
                var sb = new StringBuilder(palindrome);
                sb[at] = to;
                return sb.ToString();
            }
        }

        private readonly Solution s;
        public _1328_Break_a_Palindrome()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = "abccba";
            var res = s.BreakPalindrome(input);
            Assert.Equal("aaccba", res);
        }

        [Fact]
        public void example2()
        {
            var input = "a";
            var res = s.BreakPalindrome(input);
            Assert.Equal("", res);
        }

        [Fact]
        public void example3()
        {
            var input = "aa";
            var res = s.BreakPalindrome(input);
            Assert.Equal("ab", res);
        }

        [Fact]
        public void example4()
        {
            var input = "aba";
            var res = s.BreakPalindrome(input);
            Assert.Equal("abb", res);
        }

        [Fact]
        public void test1()
        {
            var input = "zaz";
            var res = s.BreakPalindrome(input);
            Assert.Equal("aaz", res);
        }

        [Fact]
        public void test2()
        {
            var input = "aza";
            var res = s.BreakPalindrome(input);
            Assert.Equal("azb", res);
        }
    }
}