using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _130_Surrounded_Regions
    {
        public class Solution
        {
            public char[][] Board;
            private Queue<(int r, int c)> q;
            private HashSet<int> visitedSet;
            private HashSet<int> notForFlip;
            private int rows;
            private int columns;

            public void Solve(char[][] board)
            {
                Board = board;
                q = new Queue<(int r, int c)>();
                visitedSet = new HashSet<int>();
                notForFlip = new HashSet<int>();
                rows = board.Length;
                columns = board[0].Length;
                for (int c = 0; c < columns; c++)
                {
                    if (board[0][c] == 'O')
                        dfs(0, c);
                    if (board[rows - 1][c] == 'O')
                        dfs(rows - 1, c);
                }

                for (int r = 0; r < rows; r++)
                {
                    if (board[r][0] == 'O')
                        dfs(r, 0);
                    if (board[r][columns - 1] == 'O')
                        dfs(r, columns - 1);
                }

                for (int r = 1; r < rows - 1; r++)
                    for (int c = 1; c < columns - 1; c++)
                        if (board[r][c] == 'O' && !notForFlip.Contains(absoluteNum(r, c)))
                            board[r][c] = 'X';
            }

            private void dfs(int r, int c)
            {
                if (r < 0 || r >= rows || c < 0 || c >= columns)
                    return;
                if (visited(r, c))
                    return;
                addToVisited(r, c);

                if (Board[r][c] == 'X')
                    return;

                notForFlip.Add(absoluteNum(r, c));

                dfs(r - 1, c);
                dfs(r + 1, c);
                dfs(r, c - 1);
                dfs(r, c + 1);
            }

            private void addToVisited(int r, int c)
            {
                visitedSet.Add(absoluteNum(r, c));
            }

            private bool visited(int r, int c)
            {
                return visitedSet.Contains(absoluteNum(r, c));
            }

            private int absoluteNum(int r, int c)
            {
                return columns * r + c;
            }
        }

        private readonly Solution s;
        public _130_Surrounded_Regions()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = "[[X,X,X,X],[X,O,O,X],[X,X,O,X],[X,O,X,X]]"
                .Make2DArray<char>();

            s.Solve(input);

            Assert.Collection(s.Board,
                r => Assert.Collection(r, X, X, X, X),
                r => Assert.Collection(r, X, X, X, X),
                r => Assert.Collection(r, X, X, X, X),
                r => Assert.Collection(r, X, O, X, X));
        }

        private static Action<char> X => c => Assert.Equal('X', c);
        private static Action<char> O => c => Assert.Equal('O', c);

        [Fact]
        public void example2()
        {
            var input = "[[X]]".Make2DArray<char>();

            s.Solve(input);

            Assert.Collection(s.Board, 
                r => Assert.Collection(r, X));
        }

        [Fact]
        public void test1()
        {
            var input = "[[O]]".Make2DArray<char>();

            s.Solve(input);

            Assert.Collection(s.Board,
                r => Assert.Collection(r, O));
        }
    }
}