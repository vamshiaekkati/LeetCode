using Xunit;

namespace LeetCode.Structures
{
    public class Queue
    {
        private class Node
        {
            public Node(int v)
            {
                Data = v;
            }

            public int Data { get; set; }
            public Node Next { get; set; }
        }

        Node tail;
        Node head;

        internal bool IsEmpty() => tail is null && head is null;

        internal void Add(int v)
        {
            var node = new Node(v);
            if (IsEmpty())
                head = node;
            else
                tail.Next = node;
            tail = node;
        }

        internal int Peek()
        {
            if (IsEmpty())
                return -1;
            return head.Data;
        }

        internal int Remove()
        {
            if (IsEmpty())
                return -1;

            var data = head.Data;
            head = head.Next;
            if (head == null)
                tail = null;

            return data;
        }
    }

    public class QueueTests
    {
        private Queue q;

        public QueueTests()
        {
            q = new Queue();
        }

        [Fact]
        public void IsEmpty()
        {
            Assert.True(q.IsEmpty());
        }

        [Fact]
        public void Add()
        {
            q.Add(4);
            Assert.False(q.IsEmpty());
            Assert.Equal(4, q.Peek());
        }

        [Fact]
        public void Add2()
        {
            q.Add(1);
            q.Add(2);
            Assert.Equal(1, q.Peek());
        }

        [Fact]
        public void Remove()
        {
            q.Add(1);
            Assert.Equal(1, q.Remove());
            Assert.True(q.IsEmpty());
        }

        [Fact]
        public void when_add_XY_remove_XY()
        {
            q.Add(1);
            q.Add(2);
            Assert.Equal(1, q.Remove());
            Assert.Equal(2, q.Remove());
        }
    }
}
