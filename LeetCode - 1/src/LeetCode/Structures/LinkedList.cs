using Xunit;

namespace LeetCode.Structures
{
    public class LinkedList
    {
        public class Node
        {
            public int Value { get; set; }
            public Node Next { get; set; }
            public Node(int value)
            {
                Value = value;
                Next = null;
            }
        }

        Node head;
        public LinkedList()
        {
            head = null;
        }


        public int Size()
        {
            if (isEmpty())
                return 0;
            int count = 1;
            var current = head;
            while (current.Next != null)
            {
                count++;
                current = current.Next;
            }

            return count;
        }

        public void Append(int value)
        {
            var node = new Node(value);
            if (isEmpty())
                head = node;
            else
            {
                var current = head;
                while (current.Next != null)
                    current = current.Next;
                current.Next = node;
            }
        }

        public void Remove(int value)
        {
            if (isEmpty())
                return;
            if (head.Value == value)
            {
                head = head.Next;
                return;
            }

            var current = head;
            while (current.Next != null)
            {
                if (current.Next.Value == value)
                {
                    current.Next = current.Next.Next;
                    return;
                }

                current = current.Next;
            }
        }

        private bool isEmpty()
        {
            return head == null;
        }
    }

    public class LinkedListTests
    {
        private readonly LinkedList ll;

        public LinkedListTests()
        {
            ll = new LinkedList();
        }

        private void ExpectedSize(int Expected)
        {
            Assert.Equal(Expected, ll.Size());
        }

        [Fact]
        public void when_created()
        {
            ExpectedSize(0);
        }

        [Fact]
        public void when_appended()
        {
            ll.Append(0);
            ExpectedSize(1);
        }

        [Fact]
        public void when_appended_3()
        {
            ll.Append(1);
            ll.Append(2);
            ll.Append(3);
            ExpectedSize(3);
        }

        [Fact]
        public void when_removed()
        {
            ll.Append(1);
            ll.Remove(1);
            ExpectedSize(0);
        }

        [Fact]
        public void when_no_removed()
        {
            ll.Append(1);
            ll.Remove(2);
            ExpectedSize(1);
        }

        [Fact]
        public void when_removed3()
        {
            ll.Append(1);
            ll.Append(2);
            ll.Remove(2);
            ExpectedSize(1);
        }


    }
}
