using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace LeetCode.Algorithms
{
    public class HeapSort
    {
        public int[] Sort(int[] a)
        {
            var count = a.Length;
            heapify(a, count);
            var end = count - 1;
            while (end > 0)
            {
                swap(ref a[end], ref a[0]);
                end--;
                siftDown(a, 0, end);
            }

            return a;
        }

        private void heapify(int[] a, int count)
        {
            var start = iParent(count);
            while (start >= 0)
                siftDown(a, start--, count - 1);
        }

        private void siftDown(int[] a, int start, int end)
        {
            var root = start;
            while (iLeftChild(root) <= end)
            {
                var child = iLeftChild(root);
                var swap = root;
                if (a[swap] < a[child])
                    swap = child;
                if (child + 1 <= end && a[swap] < a[child + 1])
                    swap = child + 1;
                if (swap == root)
                    return;
                else
                {
                    this.swap(ref a[root], ref a[swap]);
                    root = swap;
                }
            }
        }

        private int iParent(int i) => (i - 1) / 2;
        private int iLeftChild(int i) => 2 * i + 1;
        private int iRightChild(int i) => 2 * i + 2;

        private void swap(ref int v1, ref int v2)
        {
            var t = v1;
            v1 = v2;
            v2 = t;
        }
    }

    public class HeapSortTests
    {
        private readonly HeapSort heapsort;

        public class TestData : IEnumerable<object[]>
        {
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new int[] { 1 }, new int[] { 1 } };
                yield return new object[] { new int[] { 2, 1 }, new int[] { 1, 2 } };
                yield return new object[] { new int[] { 3, 2, 1 }, new int[] { 1, 2, 3 } };
                yield return new object[] { new int[] { 1, 2, 3, 3, 2, 1 }, new int[] { 1, 1, 2, 2, 3, 3 } };
                yield return new object[] { new int[] { 6, 5, 3, 1, 8, 7, 2, 4 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 } };
            }
        }

        public HeapSortTests()
        {
            heapsort = new HeapSort();
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void sort_theory(int[] array, int[] expected)
        {
            var result = heapsort.Sort(array);
            Assert.Equal(expected, result);
        }
    }
}
