using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeetCode.Algorithms
{
    class MergeSort
    {
        internal int[] Sort(int[] vs)
        {
            return mergeSort(vs);
        }

        private int[] mergeSort(int[] vs)
        {
            if (vs.Length <= 1)
                return vs;
            var arr1 = copy(vs, 0, vs.Length / 2);
            var arr2 = copy(vs, vs.Length / 2, vs.Length);
            var sort1 = mergeSort(arr1);
            var sort2 = mergeSort(arr2);
            var sort3 = new int[vs.Length];
            int j = 0;
            int k = 0;
            for (int i = 0; i < sort3.Length; i++)
            {
                int value1 = int.MaxValue;
                int value2 = int.MaxValue;
                if (j < sort1.Length)
                    value1 = sort1[j];
                if (k < sort2.Length)
                    value2 = sort2[k];
                if (value1 < value2)
                {
                    sort3[i] = value1;
                    j++;
                }
                else
                {
                    sort3[i] = value2;
                    k++;
                }
            }
            return sort3;
        }

        private int[] copy(int[] vs, int start, int end)
        {
            var newArr = new int[end - start];
            int j = 0;
            for (int i = start; i < end; i++)
                newArr[j++] = vs[i];
            return newArr;
        }
    }

    public class MergeSortTests
    {
        private readonly MergeSort ms;

        public MergeSortTests()
        {
            ms = new MergeSort();
        }


        [Fact]
        public void sort1()
        {
            var res = ms.Sort(new int[] { 1, 3, 2 });
            Assert.Equal(new int[] { 1, 2, 3 }, res);
        }

        [Fact]
        public void sort2()
        {
            var res = ms.Sort(new int[] { 15, 3, 2, 1, 5, 9, 7, 8, 6 });
            Assert.Equal(new int[] { 1, 2, 3, 5, 6, 7, 8, 9, 15 }, res);
        }
    }
}
