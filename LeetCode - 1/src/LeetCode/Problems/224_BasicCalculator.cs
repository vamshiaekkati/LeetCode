using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _224_BasicCalculator
    {
        public abstract class Expression
        {
            public abstract int Calc();
        }
        public class ParenthesisExpression : Expression
        {
            public List<Expression> Childs;
            public ParenthesisExpression()
            {
                Childs = new List<Expression>();
            }

            public override int Calc()
            {
                int res = 0;
                foreach (var exp in Childs)
                    res += exp.Calc();
                return res;
            }
        }
        public class NumberExpression : Expression
        {
            public int Num;
            public NumberExpression(int num)
            {
                Num = num;
            }

            public override int Calc() => Num;
        }

        public class UnaryOperationExpression : Expression
        {
            public Op Op;
            public Expression Exp;

            public UnaryOperationExpression(Op op, Expression ex)
            {
                Op = op;
                Exp = ex;
            }

            public override int Calc()
            {
                if (Op == Op.Minus)
                    return -1 * Exp.Calc();
                return 0;
            }
        }

        public class BinaryOperationExpression : Expression
        {
            private readonly Expression lExp;
            public Op Op;
            private readonly Expression rExp;

            public BinaryOperationExpression(Expression lExp, Op op, Expression rExp)
            {
                this.lExp = lExp;
                Op = op;
                this.rExp = rExp;
            }

            public override int Calc()
            {
                int l = lExp.Calc();
                int r = rExp.Calc();
                if (Op == Op.Plus)
                    return l + r;
                if (Op == Op.Minus)
                    return l - r;
                return 0;
            }
        }

        public enum Op { Plus, Minus }

        public class Solution
        {
            public int Calculate(string s)
            {
                int answer = 0;
                int i = 0;
                var root = ParseExpression(s, ref i);
                foreach (var exp in root)
                    answer += exp.Calc();
                return answer;
            }

            private IEnumerable<Expression> ParseExpression(string s, ref int pos)
            {
                var expressions = new List<Expression>();
                while (pos < s.Length)
                {
                    var c = s[pos];
                    if (c == ' ')
                        pos++;
                    if (c == ')')
                    {
                        pos++;
                        return expressions;
                    }
                    if (c == '(')
                        expressions.Add(ParseParenthesisExpression(s, ref pos));
                    if (char.IsDigit(c))
                        expressions.Add(ParseNumberExpression(s, ref pos));
                    if (IsOp(c))
                        AddOpExpression(s, ref pos, expressions);
                }

                return expressions;
            }

            private void AddOpExpression(string s, ref int pos, List<Expression> expressions)
            {
                var op = ParseOp(s, ref pos);
                var rExp = ParseOneExpression(s, ref pos);
                if (expressions.Any())
                {
                    var last = expressions.Last();
                    expressions.RemoveAt(expressions.Count - 1);
                    expressions.Add(new BinaryOperationExpression(last, op, rExp));
                }
                else
                    expressions.Add(new UnaryOperationExpression(op, rExp));
            }

            private Expression ParseOneExpression(string s, ref int pos)
            {
                while (pos < s.Length)
                {
                    var c = s[pos];
                    if (c == ' ')
                        pos++;
                    if (c == '(')
                        return ParseParenthesisExpression(s, ref pos);
                    if (char.IsDigit(c))
                        return ParseNumberExpression(s, ref pos);
                }
                throw new Exception("oopsy parse one expression");
            }

            private ParenthesisExpression ParseParenthesisExpression(string s, ref int pos)
            {
                pos++;
                var exp = new ParenthesisExpression();
                exp.Childs.AddRange(ParseExpression(s, ref pos));
                return exp;
            }

            private NumberExpression ParseNumberExpression(string s, ref int pos)
            {
                return new NumberExpression(ParseInt(s, ref pos));
            }

            private Op ParseOp(string s, ref int i)
            {
                switch (s[i++])
                {
                    case '+':
                        return Op.Plus;
                    case '-':
                        return Op.Minus;
                }
                throw new Exception("oopsy parse op");
            }

            private bool IsOp(char c) => c == '+' || c == '-';

            public int ParseInt(string s, ref int startPos)
            {
                var pos = startPos;
                while (pos < s.Length && char.IsDigit(s[pos]))
                    pos++;

                var length = pos - startPos;
                var result = int.Parse(s.Substring(startPos, length));
                startPos += length;
                return result;
            }
        }

        private readonly Solution s;
        public _224_BasicCalculator()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var input = "1 + 1";

            var res = s.Calculate(input);

            Assert.Equal(2, res);
        }

        [Fact]
        public void example2()
        {
            var input = " 2-1 + 2 ";

            var res = s.Calculate(input);

            Assert.Equal(3, res);
        }

        [Fact]
        public void example3()
        {
            var input = "(1+(4+5+2)-3)+(6+8)";

            var res = s.Calculate(input);

            Assert.Equal(23, res);
        }

        [Fact]
        public void test0()
        {
            var input = "(0+1)+(2+3)";

            var res = s.Calculate(input);

            Assert.Equal(6, res);
        }


        [Fact]
        public void test1()
        {
            var input = "1";

            var res = s.Calculate(input);

            Assert.Equal(1, res);
        }

        [Fact]
        public void test11()
        {
            var input = "               - (- 1)                       ";

            var res = s.Calculate(input);

            Assert.Equal(1, res);
        }


        [Fact]
        public void test2()
        {
            var input = "-1";

            var res = s.Calculate(input);

            Assert.Equal(-1, res);
        }

        [Fact]
        public void test3()
        {
            var input = "1-(-1)";

            var res = s.Calculate(input);

            Assert.Equal(2, res);
        }

        [Fact]
        public void test4()
        {
            var input = "(1+((1+1)+1))";

            var res = s.Calculate(input);

            Assert.Equal(4, res);
        }

        [Theory]
        [InlineData("3", 0, 3, 1)]
        [InlineData("12345", 0, 12345, 5)]
        [InlineData("12+45", 3, 45, 5)]
        [InlineData("12+45", 0, 12, 2)]
        [InlineData("(99)", 1, 99, 3)]
        public void ParseIntTests(string input, int startPos, int expect, int endPos)
        {
            var result = s.ParseInt(input, ref startPos);

            Assert.Equal(expect, result);
            Assert.Equal(endPos, startPos);
        }
    }
}