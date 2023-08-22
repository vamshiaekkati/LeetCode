using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeetCode.Problems
{
    class _46_Permutations
    {
        public IList<IList<int>> Solve(int[] input)
        {
            IList<IList<int>> res = new List<IList<int>>();
            DFS(new HashSet<int> { }, new List<int> { }, input, res);
            return res;
        }

        private void DFS(HashSet<int> visited, List<int> list, int[] input, IList<IList<int>> res)
        {
            foreach (var item in input)
            {
                if (visited.Contains(item))
                    continue;
                visited.Add(item);
                list.Add(item);
                if (input.Length == list.Count)
                    res.Add(new List<int>(list));
                DFS(visited, list, input, res);
                visited.Remove(item);
                list.RemoveAt(list.Count - 1);
            }
        }
    }

    public class PermutationsTests
    {
        private readonly _46_Permutations solver;

        public PermutationsTests()
        {
            solver = new _46_Permutations();
        }

        [Fact]
        public void when()
        {
            var input = new int[] { 1, 2, 3 };

            IList<IList<int>> result = solver.Solve(input);

            Assert.Equal(new[] { new[] { 1, 2, 3 }, new[] { 1, 3, 2 }, new[] { 2, 1, 3 }, new[] { 2, 3, 1 }, new[] { 3, 1, 2 }, new[] { 3, 2, 1 } }, result);
        }

        [Fact]
        public void when2()
        {
            var input = new int[] { 0, 1 };

            IList<IList<int>> result = solver.Solve(input);

            Assert.Equal(new[] { new[] { 0, 1 }, new[] { 1, 0 } }, result);
        }

        [Fact]
        public void when3()
        {
            var input = new int[] { 1 };

            IList<IList<int>> result = solver.Solve(input);

            Assert.Equal(new[] { new[] { 1 } }, result);
        }
    }
}
