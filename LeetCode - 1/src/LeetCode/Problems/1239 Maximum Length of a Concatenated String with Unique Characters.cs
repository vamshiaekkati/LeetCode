using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _1239_Maximum_Length_of_a_Concatenated_String_with_Unique_Characters
    {
        public class Solution
        {
            int[] masks;
            IList<string> arr;
            public int MaxLength(IList<string> arr)
            {
                masks = new int[arr.Count];
                this.arr = arr;
                for (int i = 0; i < arr.Count; i++)
                    masks[i] = GetMask(arr[i]);

                int answer = 0;
                for (int i = 0; i < arr.Count; i++)
                    answer = Math.Max(answer, Concat(0, i, 0));

                return answer;
            }

            private int GetAnswer(int mask)
            {
                int res = 0;
                while (mask > 0)
                {
                    var t = mask - 1;
                    res++;
                    mask &= t;
                }
                return res;
            }

            private int Concat(int mask, int i, int max)
            {
                if (!IsIntersects(mask, masks[i]))
                    mask += masks[i];

                for (int j = i + 1; j < arr.Count; j++)
                    max = Math.Max(max, Concat(mask, j, max));

                return Math.Max(max, GetAnswer(mask));
            }

            private bool IsIntersects(int mask, int v)
            {
                return (mask & v) > 0;
            }

            private int GetMask(string v)
            {
                int res = 0;
                foreach (char c in v)
                {
                    var charValue = 1 << c - 'a';
                    if ((res & charValue) > 0)
                        return 0;
                    res |= 1 << c - 'a';
                }
                return res;
            }
        }

        private readonly Solution s;
        public _1239_Maximum_Length_of_a_Concatenated_String_with_Unique_Characters()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = @"[""un"", ""iq"", ""ue""]".MakeArray<string>();
            var res = s.MaxLength(input.ToList());
            Assert.Equal(4, res);
        }

        [Fact]
        public void example2()
        {
            var input = @"[""cha"", ""r"", ""act"", ""ers""]".MakeArray<string>();
            var res = s.MaxLength(input.ToList());
            Assert.Equal(6, res);
        }

        [Fact]
        public void example3()
        {
            var input = @"[""abcdefghijklmnopqrstuvwxyz""]".MakeArray<string>();
            var res = s.MaxLength(input.ToList());
            Assert.Equal(26, res);
        }

        [Fact]
        public void test1()
        {
            var input = "[a,b,c,ab,az]".MakeArray<string>();
            var res = s.MaxLength(input.ToList());
            Assert.Equal(4, res);
        }

        [Fact]
        public void fail1()
        {
            var input = @"[""yy"",""bkhwmpbiisbldzknpm""]".MakeArray<string>();
            var res = s.MaxLength(input.ToList());
            Assert.Equal(0, res);
        }

        [Fact]
        public void test2()
        {
            var input = "[b,aa,bb,cc,dd,a]".MakeArray<string>();
            var res = s.MaxLength(input.ToList());
            Assert.Equal(2, res);
        }
    }
}