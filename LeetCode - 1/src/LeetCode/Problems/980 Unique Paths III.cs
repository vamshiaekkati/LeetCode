using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _980_Unique_Paths_III
    {
        public class Solution
        {
            private int startRow;
            private int startColumn;
            private int emptySquares;
            private int[][] grid;
            private int rows;
            private int cols;

            public int UniquePathsIII(int[][] grid)
            {
                emptySquares = 0;
                this.grid = grid;
                rows = grid.Length;
                cols = grid[0].Length;
                for (int r = 0; r < rows; r++)
                    for (int c = 0; c < cols; c++)
                    {
                        var v = grid[r][c];
                        if (v == 1)
                        {
                            startRow = r;
                            startColumn = c;
                        }
                        if (v == 0)
                            emptySquares++;
                    }
                return dfs(startRow, startColumn, emptySquares);
            }

            private int dfs(int r, int c, int emptySquares)
            {
                if (r < 0 || r >= rows || c < 0 || c >= cols)
                    return 0;

                if (grid[r][c] == -1)
                    return 0;
                if (grid[r][c] == 2)
                    return emptySquares < 0 ? 1 : 0;

                grid[r][c] = -1;
                emptySquares--;
                int ans = 
                    dfs(r + 1, c, emptySquares) +
                    dfs(r - 1, c, emptySquares) +
                    dfs(r, c + 1, emptySquares) +
                    dfs(r, c - 1, emptySquares);

                grid[r][c] = 0;
                emptySquares++;
                return ans;
            }
        }

        private readonly Solution s;
        public _980_Unique_Paths_III()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = "[[1,0,0,0],[0,0,0,0],[0,0,2,-1]]".Make2DArray<int>();

            var res = s.UniquePathsIII(input);

            Assert.Equal(2, res);
        }

        [Fact]
        public void example2()
        {
            var input = "[[1,0,0,0],[0,0,0,0],[0,0,0,2]]".Make2DArray<int>();

            var res = s.UniquePathsIII(input);

            Assert.Equal(4, res);
        }

        [Fact]
        public void example3()
        {
            var input = "[[0,1],[2,0]]".Make2DArray<int>();

            var res = s.UniquePathsIII(input);

            Assert.Equal(0, res);
        }
    }
}