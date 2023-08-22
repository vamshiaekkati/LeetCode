using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeetCode.Structures
{
    public class Heap
    {
        List<int> items;
        public Heap()
        {
            items = new List<int>();
        }

        internal void Insert(int v)
        {
            items.Add(v);
            heapifyUp();
        }

        private bool HasParent(int i) => i > 0;
        private int GetParentIndex(int i) => (i - 1) / 2;
        private int GetParent(int i) => items[GetParentIndex(i)];
        private bool HasLeftChild(int i) => GetLeftChildIndex(i) < items.Count;
        private int GetLeftChildIndex(int i) => i * 2 + 1;
        private int GetLeftChild(int i) => items[GetLeftChildIndex(i)];

        private bool HasRightChild(int i) => (GetRightChildIndex(i)) < items.Count;
        private int GetRightChildIndex(int i) => i * 2 + 2;
        private int GetRightChild(int i) => items[GetRightChildIndex(i)];

        private void heapifyUp()
        {
            var index = items.Count - 1;
            var item = items[index];
            while (HasParent(index))
            {
                if (GetParent(index) > item)
                    swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }

        private void swap(int i1, int i2)
        {
            var t = items[i1];
            items[i1] = items[i2];
            items[i2] = t;
        }

        internal int Min()
        {
            return items[0];
        }

        internal void Remove()
        {
            var index = items.Count - 1;
            swap(0, index);
            items.RemoveAt(index);
            heapifyDown();
        }

        private void heapifyDown()
        {
            var index = 0;
            while (HasLeftChild(index))
            {
                var minIndex = -1;
                if (GetLeftChild(index) < items[index])
                    minIndex = GetLeftChildIndex(index);
                if (HasRightChild(index))
                    if (GetRightChild(index) < items[minIndex])
                        minIndex = GetRightChildIndex(index);
                if (minIndex == -1)
                    break;
                swap(minIndex, index);
                index = minIndex;
            }
        }
    }

    public class HeapTests
    {
        private Heap heap;

        public HeapTests()
        {
            heap = new Heap();
        }
        [Fact]
        public void Insert()
        {
            heap.Insert(3);
            Assert.Equal(3, heap.Min());
        }

        [Fact]
        public void Insert2()
        {
            heap.Insert(3);
            heap.Insert(2);
            Assert.Equal(2, heap.Min());
        }

        [Fact]
        public void Remove()
        {
            heap.Insert(3);
            heap.Insert(2);
            heap.Remove();
            Assert.Equal(3, heap.Min());
        }

        [Fact]
        public void Remove2()
        {
            heap.Insert(3);
            heap.Insert(2);
            heap.Insert(1);
            heap.Remove();
            heap.Remove();
            Assert.Equal(3, heap.Min());
        }

        [Fact]
        public void Remove3()
        {
            heap.Insert(10);
            heap.Insert(15);
            heap.Insert(20);
            heap.Insert(17);
            heap.Remove();
            Assert.Equal(15, heap.Min());
        }

        [Fact]
        public void Remove4()
        {
            heap.Insert(10);
            heap.Insert(20);
            heap.Insert(15);
            heap.Insert(17);
            heap.Remove();
            Assert.Equal(15, heap.Min());
        }

        [Fact]
        public void Insert3()
        {
            heap.Insert(10);
            heap.Insert(15);
            heap.Insert(20);
            heap.Insert(17);
            heap.Insert(8);
            Assert.Equal(8, heap.Min());
        }
    }

}
