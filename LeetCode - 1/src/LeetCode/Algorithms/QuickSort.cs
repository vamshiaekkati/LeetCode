using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeetCode.Algorithms
{
    class QuickSort
    {
        internal int[] Sort(int[] array)
        {
            quickSort(array, 0, array.Length - 1);
            return array;
        }

        private void quickSort(int[] array, int left, int right)
        {
            if (left >= right)
                return;
            int middle = findMiddle(array, left, right);
            quickSort(array, left, middle - 1);
            quickSort(array, middle, right);
        }

        private int findMiddle(int[] array, int left, int right)
        {
            var pivot = array[(left + right) / 2];
            while (left <= right)
            {
                while (array[left] < pivot)
                    left++;
                while (array[right] > pivot)
                    right--;
                if (left <= right)
                    swap(array, left++, right--);
            }
            return left;
        }

        private void swap(int[] array, int left, int right)
        {
            var t = array[left];
            array[left] = array[right];
            array[right] = t;
        }
    }

    public class QuickSortTests
    {
        private readonly QuickSort qs;

        public QuickSortTests()
        {
            qs = new QuickSort();
        }

        [Fact]
        public void sort()
        {
            var res = qs.Sort(new int[] { 15, 3, 2, 1, 5, 9, 7, 8, 6 });
            Assert.Equal(new int[] { 1, 2, 3, 5, 6, 7, 8, 9, 15 }, res);
        }
    }
}
