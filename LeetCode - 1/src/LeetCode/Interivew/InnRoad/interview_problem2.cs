using System.Text;
using Xunit;

namespace LeetCode.Interview.InnRoad
{
    // input:    aaaabbcccddddeffgaa
    // compress: a4b2c3d4e1f2g1a2
    public class Problem2
    {
        public class Solution
        {
            public string Compress(string str)
            {
                char c = '\0';
                int size = 0;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < str.Length; i++)
                {
                    if (c == '\0')
                    {
                        c = str[i];
                        size = 1;
                        continue;
                    }

                    if (c == str[i])
                        size++;
                    else
                    {
                        sb.Append($"{c}{size}");
                        c = str[i];
                        size = 1;
                    }
                }
                sb.Append($"{c}{size}");
                if (sb.Length < str.Length)
                    return sb.ToString();
                return str;
            }
        }


        private readonly Solution s;

        public Problem2()
        {
            s = new Solution();
        }

        [Fact]
        public void test1()
        {
            var str = "aaaabbcccddddeffgaa";

            string result = s.Compress(str);

            Assert.Equal("a4b2c3d4e1f2g1a2", result);
        }

        [Fact]
        public void test2()
        {
            var str = "abcd";

            string result = s.Compress(str);

            Assert.Equal("abcd", result);
        }



        [Fact]
        public void test3()
        {
            var str = "a";

            string result = s.Compress(str);

            Assert.Equal("a", result);
        }

        [Fact]
        public void test4()
        {
            var str = "aa";

            string result = s.Compress(str);

            Assert.Equal("aa", result);
        }



        [Fact]
        public void test5()
        {
            var str = "aaa";

            string result = s.Compress(str);

            Assert.Equal("a3", result);
        }
    }
}
