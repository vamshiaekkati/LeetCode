using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _929_Unique_Email_Addresses
    {
        public class Solution
        {
            public int NumUniqueEmails(string[] emails)
            {
                HashSet<string> result = new HashSet<string>();
                foreach (var email in emails)
                {
                    var split = email.Split('@');
                    var left = split[0].Split('+')[0].Replace(".", "");
                    result.Add($"{left}@{split[1]}");
                }

                return result.Count;
            }
        }

        private readonly Solution s;
        public _929_Unique_Email_Addresses()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = @"[""test.email+alex@leetcode.com"",""test.e.mail+bob.cathy@leetcode.com"",""testemail+david@lee.tcode.com""]"
                .MakeArray<string>();

            var res = s.NumUniqueEmails(input);

            Assert.Equal(2, res);
        }

        [Fact]
        public void example2()
        {
            var input = @"[""a@leetcode.com"",""b@leetcode.com"",""c@leetcode.com""]"
                .MakeArray<string>();

            var res = s.NumUniqueEmails(input);

            Assert.Equal(3, res);
        }


        [Fact]
        public void test1()
        {
            var input = @"[""abc+a@leetcode.com"",""a.bc+a@leetcode.com"",""a.b.c+a@leetcode.com""]"
                .MakeArray<string>();

            var res = s.NumUniqueEmails(input);

            Assert.Equal(1, res);
        }

        [Fact]
        public void test2()
        {
            var input = @"[""a@a""]"
                .MakeArray<string>();

            var res = s.NumUniqueEmails(input);

            Assert.Equal(1, res);
        }


        [Fact]
        public void fail1()
        {
            var input = @"[""test.email+alex@leetcode.com"",""test.email.leet+alex@code.com""]"
                .MakeArray<string>();

            var res = s.NumUniqueEmails(input);

            Assert.Equal(2, res);
        }
    }
}