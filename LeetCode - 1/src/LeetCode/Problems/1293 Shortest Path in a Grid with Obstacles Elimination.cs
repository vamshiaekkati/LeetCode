using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _1293_Shortest_Path_in_a_Grid_with_Obstacles_Elimination
    {
        public class Solution
        {
            private int[][] grid;
            private int rows;
            private int cols;
            private int[,,] moveCount;

            public int ShortestPath(int[][] grid, int k)
            {
                this.grid = grid;
                rows = grid.Length;
                cols = grid[0].Length;
                moveCount = new int[rows, cols, k + 1];

                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < cols; j++)
                        for (int z = 0; z <= k; z++)
                            moveCount[i, j, z] = int.MaxValue;

                queue.Enqueue((0, 0, k, 0));
                while (queue.Count > 0)
                {
                    var move = queue.Dequeue();
                    BFS(move.row, move.col, move.k, move.moves);
                }

                int min = int.MaxValue;
                for (int i = 0; i <= k; i++)
                    min = Math.Min(min, moveCount[rows - 1, cols - 1, i]);
                return min == int.MaxValue ? -1 : min;
            }

            private Queue<(int row, int col, int k, int moves)> queue = new Queue<(int, int, int, int)>();

            private void BFS(int row, int col, int k, int moves)
            {
                if (moveCount[row, col, k] <= moves)
                    return;

                moveCount[row, col, k] = moves;
                MoveUp(row, col, k, moves);
                MoveDown(row, col, k, moves);
                MoveLeft(row, col, k, moves);
                MoveRight(row, col, k, moves);
            }

            private void MoveUp(int row, int col, int k, int moves)
            {
                if (row <= 0)
                    return;
                Move(row - 1, col, k, moves + 1);
            }

            private void MoveDown(int row, int col, int k, int moves)
            {
                if (row >= rows - 1)
                    return;
                Move(row + 1, col, k, moves + 1);
            }

            private void MoveLeft(int row, int col, int k, int moves)
            {
                if (col <= 0)
                    return;

                Move(row, col - 1, k, moves + 1);
            }

            private void MoveRight(int row, int col, int k, int moves)
            {
                if (col >= cols - 1)
                    return;

                Move(row, col + 1, k, moves + 1);
            }

            private void Move(int row, int col, int k, int moves)
            {
                if (IsWall(row, col))
                {
                    if (k > 0)
                        EnqueueMove(row, col, k - 1, moves);
                }
                else
                    EnqueueMove(row, col, k, moves);
            }

            private bool IsWall(int row, int col) => grid[row][col] == 1;

            private void EnqueueMove(int row, int col, int k, int moves)
            {
                queue.Enqueue((row, col, k, moves));
            }
        }

        private readonly Solution s;
        public _1293_Shortest_Path_in_a_Grid_with_Obstacles_Elimination()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = @"[[0,0,0],
                           [1,1,0],
                           [0,0,0],
                           [0,1,1],
                           [0,0,0]]".Make2DArray<int>();

            var res = s.ShortestPath(input, 1);

            Assert.Equal(6, res);
        }

        [Fact]
        public void example2()
        {
            var input = @"[[0,1,1],
                           [1,1,1],
                           [1,0,0]]".Make2DArray<int>();

            var res = s.ShortestPath(input, 1);

            Assert.Equal(-1, res);
        }

        [Fact]
        public void test1()
        {
            var input = @"[[0]]".Make2DArray<int>();

            var res = s.ShortestPath(input, 1);

            Assert.Equal(0, res);
        }

        [Fact]
        public void test2()
        {
            var input = @"[[0, 1, 1, 0]]".Make2DArray<int>();

            var res = s.ShortestPath(input, 1);

            Assert.Equal(-1, res);
        }

        [Fact]
        public void test3()
        {
            var input = @"[[0, 1, 1, 0, 0, 0],
                           [0, 1, 1, 0, 1, 0],
                           [0, 1, 1, 0, 1, 0],
                           [0, 1, 0, 0, 1, 0]]".Make2DArray<int>();

            var res = s.ShortestPath(input, 1);

            Assert.Equal(14, res);
        }


        [Fact]
        public void fail1()
        {
            var input = @"[[0,0],
                           [1,0],
                           [1,0],
                           [1,0],
                           [1,0],
                           [1,0],
                           [0,0],
                           [0,1],
                           [0,1],
                           [0,1],
                           [0,0],
                           [1,0],
                           [1,0],
                           [0,0]]".Make2DArray<int>();

            var res = s.ShortestPath(input, 4);

            Assert.Equal(14, res);
        }


        [Fact]
        public void test4()
        {
            var input = @"[[0,0],
                           [1,0],
                           [1,0],
                           [0,0],
                           [0,1],
                           [0,0]]".Make2DArray<int>();

            var res = s.ShortestPath(input, 1);

            Assert.Equal(6, res);
        }

        [Fact]
        public void test5()
        {
            var input = @"[[0,1,1,0,0,0],
                           [0,0,0,0,1,0]]".Make2DArray<int>();

            var res = s.ShortestPath(input, 1);

            Assert.Equal(6, res);
        }

        [Fact]
        public void fail2()
        {
            var input = @"[[0,1,0,0,0,1,0,0],
                           [0,1,0,1,0,1,0,1],
                           [0,0,0,1,0,0,1,0]]".Make2DArray<int>();

            var res = s.ShortestPath(input, 1);

            Assert.Equal(13, res);
        }

        [Fact]
        public void test6()
        {
            var input = @"[[0,1,0,0,0,0],
                           [0,1,0,1,0,1],
                           [0,0,0,1,1,0]]".Make2DArray<int>();

            var res = s.ShortestPath(input, 1);
            Assert.True(res > 0);
            //Assert.Equal(13, res);
        }
    }
}