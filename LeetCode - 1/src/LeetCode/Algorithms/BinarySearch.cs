using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeetCode.Algorithms
{
    public class BinarySearch
    {
        internal int Find(int[] array, int value)
        {
            return find(array, 0, array.Length - 1, value);
        }

        private int find(int[] array, int left, int right, int value)
        {
            if (left > right)
                return -1;
            int middle = left + (right - left) / 2;
            if (array[middle] == value)
                return middle;
            if (array[middle] > value)
                return find(array, left, middle - 1, value);
            else
                return find(array, middle + 1, right, value);
        }
    }

    public class BinarySearchTests
    {
        private readonly BinarySearch bs;

        public BinarySearchTests()
        {
            bs = new BinarySearch();
        }

        [Fact]
        public void find()
        {
            var res = bs.Find(new[] { 1, 3, 4, 5, 13, 20, 25, 40, 42, 44, 53 }, 13);
            Assert.Equal(4, res);
        }

        [Fact]
        public void no_find()
        {
            var res = bs.Find(new[] { 1, 3, 4, 5, 13, 20, 25, 40, 42, 44, 53 }, 14);
            Assert.Equal(-1, res);
        }
    }
}
