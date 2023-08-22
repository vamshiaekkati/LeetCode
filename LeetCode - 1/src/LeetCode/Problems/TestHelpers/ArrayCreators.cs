using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LeetCode.Problems
{
    public static class ArrayCreators
    {
        /// <summary></summary>
        /// <param name="input">[[0,0],[2,0],[1,1],[2,1],[2,2]]</param>
        public static T[][] Make2DArray<T>(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException();

            var split = input.Split('[');
            var rows = split.Length - 2;
            T[][] res = new T[rows][];
            for (int i = 0; i < rows; i++)
            {
                var split2 = split[i + 2].Split(']').First().Split(',');
                var cols = split2.Where(s => !string.IsNullOrEmpty(s)).Count();
                res[i] = new T[cols];
                for (int j = 0; j < cols; j++)
                    res[i][j] = Parse<T>(split2[j]);
            }

            return res;
        }

        /// <summary></summary>
        /// <param name="input">[1,2,0,7,9,1]</param>
        public static T[] MakeArray<T>(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException();

            var split = input.Trim('[', ']')
                             .Split(',')
                             .Where(i => !string.IsNullOrEmpty(i))
                             .ToArray();
            var n = split.Length;
            var res = new T[n];
            for (int i = 0; i < n; i++)
                res[i] = Parse<T>(split[i].Trim());

            return res;
        }

        private static T Parse<T>(string v)
        {
            if (typeof(T) == typeof(string))
                v = v.Trim('"');

            return (T)Convert.ChangeType(v, typeof(T));
        }
    }

    public class Make2DArrayTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void when_error_input_then_throws(string input)
        {
            Assert.Throws<ArgumentException>(() => ArrayCreators.Make2DArray<int>(input));
        }

        [Fact]
        public void when_empty_array()
        {
            var result = ArrayCreators.Make2DArray<int>("[]");

            Assert.Empty(result);
        }

        [Fact]
        public void when_single_element()
        {
            var result = ArrayCreators.Make2DArray<int>("[[]]");

            Assert.Single(result);
        }

        [Fact]
        public void when_single_with_value_element()
        {
            var result = ArrayCreators.Make2DArray<int>("[[1]]");

            var arr = Assert.Single(result);
            var value = Assert.Single(arr);
            Assert.Equal(1, value);
        }

        [Fact]
        public void when_two_with_single_element()
        {
            var result = ArrayCreators.Make2DArray<int>("[[1],[2]]");

            Assert.Equal(2, result.Length);
            Assert.Equal(1, Assert.Single(result[0]));
            Assert.Equal(2, Assert.Single(result[1]));
        }

        [Fact]
        public void when_multiple_elements()
        {
            var result = ArrayCreators.Make2DArray<char>("[[a,b,c]]");

            var arr = Assert.Single(result);
            Assert.Collection(arr,
                i => Assert.Equal('a', i),
                i => Assert.Equal('b', i),
                i => Assert.Equal('c', i)
                );
        }

        [Fact]
        public void when_multiple_string_elements()
        {
            var result = ArrayCreators.Make2DArray<string>("[[foo,bar,quux]]");

            var arr = Assert.Single(result);
            Assert.Collection(arr,
                i => Assert.Equal("foo", i),
                i => Assert.Equal("bar", i),
                i => Assert.Equal("quux", i)
                );
        }

        [Fact]
        public void when_multiple_arrays()
        {
            var result = ArrayCreators.Make2DArray<int>("[[],[],[]]");

            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void when_two_arrays_different_sizes()
        {
            var result = ArrayCreators.Make2DArray<long>("[[1],[2,3]]");

            Assert.Equal(2, result.Length);

            Assert.Collection(result[0],
                i => Assert.Equal(1, i));
            Assert.Collection(result[1],
                i => Assert.Equal(2, i),
                i => Assert.Equal(3, i));
        }

        [Fact]
        public void when_string_with_quotes()
        {
            var result = ArrayCreators.Make2DArray<string>(@"[[""a"",""b"",""c""]]");

            var arr = Assert.Single(result);
            Assert.Collection(arr,
                i => Assert.Equal("a", i),
                i => Assert.Equal("b", i),
                i => Assert.Equal("c", i)
                );
        }

        [Fact]
        public void when_multiline()
        {
            var result = ArrayCreators.Make2DArray<int>(@"[[0,1,1],
                                                            [1,1,1],
                                                            [1,0,0]],");
            Assert.Collection(result,
                i => Assert.Collection(i, 
                    j => Assert.Equal(0, j),
                    j => Assert.Equal(1, j),
                    j => Assert.Equal(1, j)),
                i => Assert.Collection(i,
                    j => Assert.Equal(1, j),
                    j => Assert.Equal(1, j),
                    j => Assert.Equal(1, j)),
                i => Assert.Collection(i,
                    j => Assert.Equal(1, j),
                    j => Assert.Equal(0, j),
                    j => Assert.Equal(0, j))
                );
        }
    }

    public class MakeArrayTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void when_error_input(string input)
        {
            Assert.Throws<ArgumentException>(() => ArrayCreators.MakeArray<int>(input));
        }

        [Fact]
        public void when_empty_array()
        {
            var result = ArrayCreators.MakeArray<int>("[]");
            Assert.Empty(result);
        }

        [Fact]
        public void when_one_item()
        {
            var result = ArrayCreators.MakeArray<int>("[1]");
            Assert.Single(result, 1);
        }

        [Fact]
        public void when_mupltiple_item()
        {
            var result = ArrayCreators.MakeArray<int>("[1,-2,3]");
            Assert.Collection(result,
                i => Assert.Equal(1, i),
                i => Assert.Equal(-2, i),
                i => Assert.Equal(3, i));
        }

        [Fact]
        public void when_diff_type()
        {
            var result = ArrayCreators.MakeArray<string>("[a,be,c]");
            Assert.Collection(result,
                i => Assert.Equal("a", i),
                i => Assert.Equal("be", i),
                i => Assert.Equal("c", i));
        }

        [Fact]
        public void when_strings_with_quotes_type()
        {
            var result = ArrayCreators.MakeArray<string>(@"[""a"",""be"",""c""]");
            Assert.Collection(result,
                i => Assert.Equal("a", i),
                i => Assert.Equal("be", i),
                i => Assert.Equal("c", i));
        }

        [Fact]
        public void when_strings_with_quotes_and_whitespaces_type()
        {
            var result = ArrayCreators.MakeArray<string>(@"[""a"",""be"",""c""]");
            Assert.Collection(result,
                i => Assert.Equal("a", i),
                i => Assert.Equal("be", i),
                i => Assert.Equal("c", i));
        }
    }
}
