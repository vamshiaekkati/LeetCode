using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _764_LargestPlusSign
    {
        public class Solution
        {
            public int OrderOfLargestPlusSign(int n, int[][] mines)
            {
                bool[,] matrix = new bool[n, n];
                for (int i = 0; i < mines.Length; i++)
                    matrix[mines[i][1], mines[i][0]] = true;

                int[,] left = new int[n, n], right = new int[n, n],
                    up = new int[n, n], down = new int[n, n];
                for (int r = 0; r < n; r++)
                    for (int c = 0; c < n; c++)
                        if (!matrix[r, c])
                        {
                            left[r, c] = SafeGet(left, r, c - 1) + 1;
                            up[r, c] = SafeGet(up, r - 1, c) + 1;
                        }


                for (int r = n - 1; r >= 0; r--)
                    for (int c = n - 1; c >= 0; c--)
                        if (!matrix[r, c])
                        {
                            right[r, c] = SafeGet(right, r, c + 1) + 1;
                            down[r, c] = SafeGet(down, r + 1, c) + 1;
                        }

                int answer = 0;
                for (int r = 0; r < n; r++)
                    for (int c = 0; c < n; c++)
                        answer = Math.Max(
                            Math.Min(
                                Math.Min(left[r, c], right[r, c]),
                                Math.Min(up[r, c], down[r, c])),
                            answer);

                return answer;
            }
            private int SafeGet(int[,] left, int r, int c)
            {
                int n = left.GetLength(0);
                if (r < 0 || r >= n)
                    return 0;
                if (c < 0 || c >= n)
                    return 0;
                return left[r, c];
            }
        }

        private readonly Solution s;
        public _764_LargestPlusSign()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var n = 5;
            var mines = new List<int[]>();
            AddMine(mines, 4, 2);

            var res = s.OrderOfLargestPlusSign(n, mines.ToArray());

            Assert.Equal(2, res);
        }

        private static void AddMine(List<int[]> mines, int x, int y)
        {
            mines.Add(new int[] { x, y });
        }

        [Fact]
        public void example2()
        {
            var n = 1;
            var mines = new List<int[]>();
            AddMine(mines, 0, 0);

            var res = s.OrderOfLargestPlusSign(n, mines.ToArray());

            Assert.Equal(0, res);
        }

        [Fact]
        public void test1()
        {
            var n = 5;
            var mines = new List<int[]>();
            AddMine(mines, 0, 0);

            var res = s.OrderOfLargestPlusSign(n, mines.ToArray());

            Assert.Equal(3, res);
        }

        [Fact]
        public void test2()
        {
            var n = 500;
            var mines = new List<int[]>();
            AddMine(mines, 0, 0);

            var res = s.OrderOfLargestPlusSign(n, mines.ToArray());

            Assert.Equal(250, res);
        }

        [Fact]
        public void fail1()
        {
            var n = 2;
            var mines = new List<int[]>();
            AddMine(mines, 0, 0);
            AddMine(mines, 0, 1);
            AddMine(mines, 1, 0);

            var res = s.OrderOfLargestPlusSign(n, mines.ToArray());

            Assert.Equal(1, res);
        }
    }
}