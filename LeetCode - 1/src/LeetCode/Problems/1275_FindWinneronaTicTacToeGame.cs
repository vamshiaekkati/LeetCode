using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _1275_FindWinneronaTicTacToeGame
    {
        public class Solution
        {
            int[][] field;
            public string Tictactoe(int[][] moves)
            {
                field = new int[3][];
                for (int i = 0; i < 3; i++)
                    field[i] = new int[3];

                for (int i = 0; i < moves.Length; i++)
                {
                    var move = moves[i];
                    field[move[0]][move[1]] = i % 2 + 1;
                }

                if (CheckRows(out int winRow))
                    return GetAnswer(field[winRow][0]);
                if (CheckColumns(out int winColumn))
                    return GetAnswer(field[0][winColumn]);
                if (CheckDiagonals())
                    return GetAnswer(field[1][1]);
                if (moves.Length < 9)
                    return "Pending";
                return "Draw";
            }

            private bool CheckDiagonals()
            {
                if ((field[0][0] & field[1][1] & field[2][2]) > 0)
                    return true;
                if ((field[2][0] & field[1][1] & field[0][2]) > 0)
                    return true;
                return false;
            }

            private bool CheckColumns(out int winColumn)
            {
                winColumn = -1;
                for (int i = 0; i < 3; i++)
                    if ((field[0][i] & field[1][i] & field[2][i]) > 0)
                    {
                        winColumn = i;
                        return true;
                    }
                return false;
            }

            private bool CheckRows(out int winRow)
            {
                winRow = -1;
                for (int i = 0; i < 3; i++)
                    if ((field[i][0] & field[i][1] & field[i][2]) > 0)
                    {
                        winRow = i;
                        return true;
                    }
                return false;
            }

            private string GetAnswer(int v) => v == 1 ? "A" : "B";
        }

        private readonly Solution s;
        public _1275_FindWinneronaTicTacToeGame()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = "[[0,0],[2,0],[1,1],[2,1],[2,2]]".Make2DArray<int>();
            var res = s.Tictactoe(input);
            Assert.Equal("A", res);
        }

        [Fact]
        public void example2()
        {
            var input = "[[0,0],[1,1],[0,1],[0,2],[1,0],[2,0]]".Make2DArray<int>();
            var res = s.Tictactoe(input);
            Assert.Equal("B", res);
        }

        [Fact]
        public void example3()
        {
            var input = "[[0,0],[1,1],[2,0],[1,0],[1,2],[2,1],[0,1],[0,2],[2,2]]".Make2DArray<int>();
            var res = s.Tictactoe(input);
            Assert.Equal("Draw", res);
        }

        [Fact]
        public void example4()
        {
            var input = "[[0,0],[1,1]]".Make2DArray<int>();
            var res = s.Tictactoe(input);
            Assert.Equal("Pending", res);
        }

        [Fact]
        public void testrow()
        {
            var input = "[[0,0],[1,0],[0,1],[1,1],[0,2]]".Make2DArray<int>();
            var res = s.Tictactoe(input);
            Assert.Equal("A", res);
        }

        [Fact]
        public void testrow2()
        {
            var input = "[[0,0],[1,0],[0,1],[1,1],[2,2],[1,2]".Make2DArray<int>();
            var res = s.Tictactoe(input);
            Assert.Equal("B", res);
        }


        [Fact]
        public void testcol1()
        {
            var input = "[[0,0],[0,1],[1,0],[1,1],[2,0]".Make2DArray<int>();
            var res = s.Tictactoe(input);
            Assert.Equal("A", res);
        }

        [Fact]
        public void testcol2()
        {
            var input = "[[0,0],[0,1],[1,0],[1,1],[2,2],[2,1]".Make2DArray<int>();
            var res = s.Tictactoe(input);
            Assert.Equal("B", res);
        }
    }
}