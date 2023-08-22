using System.Collections.Generic;
using Xunit;

namespace LeetCode.Problems
{
    public class _3_LongestSubstringWithoutRepeatingCharacters
    {
        private readonly Solution s;

        public _3_LongestSubstringWithoutRepeatingCharacters()
        {
            s = new Solution();
        }

        public class Solution
        {
            public int LengthOfLongestSubstring(string s)
            {
                if (string.IsNullOrEmpty(s))
                    return 0;

                int result = 1;
                var charsInUse = new HashSet<char>();
                var charsQueue = new Queue<char>();
                for (int i = 0; i < s.Length; i++)
                {
                    charsQueue.Enqueue(s[i]);
                    if (!charsInUse.Contains(s[i]))
                        charsInUse.Add(s[i]);
                    else
                    {
                        var c = charsQueue.Dequeue();
                        while (c != s[i])
                        {
                            charsInUse.Remove(c);
                            c = charsQueue.Dequeue();
                        }
                    }

                    result = charsInUse.Count > result ? charsInUse.Count : result;
                }

                return result;
            }
        }


        [Theory]
        [InlineData("abcabcbb", 3)]
        [InlineData("bbbbb", 1)]
        [InlineData("pwwkew", 3)]
        [InlineData("", 0)]
        [InlineData(" ", 1)]
        [InlineData("a b", 3)]
        [InlineData("a b av", 4)]
        [InlineData("abc1e", 5)]
        public void when(string input, int output)
        {
            var result = s.LengthOfLongestSubstring(input);

            Assert.Equal(output, result);
        }

    }
}
