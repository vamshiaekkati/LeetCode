using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _54_SpiralMatrix
    {
        public class Solution
        {
            List<int> res;
            int[][] matrix;
            int rows;
            int cols;

            public IList<int> SpiralOrder(int[][] matrix)
            {
                res = new List<int>();
                this.matrix = matrix;
                rows = matrix.Length;
                cols = matrix[0].Length;
                int t = 0;
                int b = matrix.Length - 1;
                int l = 0;
                int r = matrix[0].Length - 1;

                while (t <= b && l <= r)
                {
                    if (t <= b)
                        AddClockwise(t, t, l, r);
                    t++;
                    if (l <= r)
                        AddClockwise(t, b, r, r);
                    r--;
                    if (t <= b)
                        AddCounterclockwise(b, b, r, l);
                    b--;
                    if (l <= r)
                        AddCounterclockwise(b, t, l, l);
                    l++;
                }

                return res;
            }

            private void AddClockwise(int rStart, int rEnd, int cStart, int cEnd)
            {
                if (rEnd >= rows || cEnd >= cols)
                    return;
                for (int row = rStart; row <= rEnd; row++)
                    for (int col = cStart; col <= cEnd; col++)
                        res.Add(matrix[row][col]);
            }

            private void AddCounterclockwise(int rStart, int rEnd, int cStart, int cEnd)
            {
                if (rEnd >= rows || cEnd >= cols)
                    return;

                for (int row = rStart; row >= rEnd; row--)
                    for (int col = cStart; col >= cEnd; col--)
                        res.Add(matrix[row][col]);
            }
        }

        private readonly Solution s;
        public _54_SpiralMatrix()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = makeMatrix(3, 3);
            var res = s.SpiralOrder(input);
            Assert.Equal(new[] { 1, 2, 3, 6, 9, 8, 7, 4, 5 }, res);
        }

        private int[][] makeMatrix(int n, int m)
        {
            int num = 1;
            int[][] res = new int[n][];
            for (int i = 0; i < n; i++)
            {
                res[i] = new int[m];
                for (int j = 0; j < m; j++)
                    res[i][j] = num++;
            }

            return res;
        }

        [Fact]
        public void example2()
        {
            var input = makeMatrix(3, 4);
            var res = s.SpiralOrder(input);
            Assert.Equal(new[] { 1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7 }, res);
        }

        [Fact]
        public void test1()
        {
            var input = makeMatrix(1, 1);
            var res = s.SpiralOrder(input);
            Assert.Equal(new[] { 1 }, res);
        }

        [Fact]
        public void test2()
        {
            var input = makeMatrix(1, 3);
            var res = s.SpiralOrder(input);
            Assert.Equal(new[] { 1, 2, 3 }, res);
        }

        [Fact]
        public void test3()
        {
            var input = makeMatrix(3, 1);
            var res = s.SpiralOrder(input);
            Assert.Equal(new[] { 1, 2, 3 }, res);
        }

        [Fact]
        public void test4()
        {
            var input = makeMatrix(3, 2);
            var res = s.SpiralOrder(input);
            Assert.Equal(new[] { 1, 2, 4, 6, 5, 3 }, res);
        }


        [Fact]
        public void test5()
        {
            var input = makeMatrix(1, 2);
            var res = s.SpiralOrder(input);
            Assert.Equal(new[] { 1, 2 }, res);
        }

        [Fact]
        public void test6()
        {
            var input = makeMatrix(2, 1);
            var res = s.SpiralOrder(input);
            Assert.Equal(new[] { 1, 2 }, res);
        }

        [Fact]
        public void test7()
        {
            var input = makeMatrix(99, 99);
            var res = s.SpiralOrder(input);
            Assert.Equal(99 * 99, res.Count);
        }
    }
}