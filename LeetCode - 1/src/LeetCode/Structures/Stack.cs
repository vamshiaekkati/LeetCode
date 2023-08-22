using Xunit;

namespace LeetCode.Structures
{
    class Stack
    {
        private class Node
        {
            public Node(int data)
            {
                Data = data;
                Next = null;
            }

            public int Data { get; set; }
            public Node Next { get; set; }
        }

        private Node top;
        public int Count { get; private set; }
        public Stack()
        {
        }

        private bool isEmpty() => top == null;

        internal void Push(int v)
        {
            Node node = new Node(v);
            if (isEmpty())
                top = node;
            else
            {
                node.Next = top;
                top = node;
            }
            Count++;
        }

        internal int Pop()
        {
            if (isEmpty())
                return -1;
            var v = top.Data;
            top = top.Next;
            Count--;
            return v;
        }

        internal int Peek() => top.Data;
    }

    public class StackTests
    {
        private readonly Stack stack;

        public StackTests()
        {
            stack = new Stack();
        }

        [Fact]
        public void when()
        {
            AssertStackSize(0);
        }

        [Fact]
        public void when_insert()
        {
            stack.Push(3);
            Assert.Equal(3, stack.Peek());
            AssertStackSize(1);
        }

        [Fact]
        public void when_push_pop()
        {
            stack.Push(1);
            var result = stack.Pop();
            Assert.Equal(1, result);
            AssertStackSize(0);
        }

        [Fact]
        public void when_push_XY_pop_YX()
        {
            stack.Push(2);
            stack.Push(3);
            Assert.Equal(3, stack.Pop());
            Assert.Equal(2, stack.Pop());
        }

        private void AssertStackSize(int size)
        {
            Assert.Equal(size, stack.Count);
        }
    }
}
