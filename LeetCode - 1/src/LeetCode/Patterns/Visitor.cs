using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Patterns.Visitor
{
    class Client
    {
        void Main()
        {
            var visitor = new Visitor();
            var o1 = new Object1();
            var o2 = new Object2();
            o1.Accept(visitor);
            o2.Accept(visitor);
        }
    }

    class Object1
    {
        public void Accept(Visitor v)
        {
            v.VisitObject1(this);
        }
    }

    class Object2
    {
        public void Accept(Visitor v)
        {
            v.VisitObject2(this);
        }
    }

    class Visitor
    {
        internal void VisitObject1(Object1 obj)
        {
            Console.WriteLine($"visited {obj.GetType().Name}");
        }

        internal void VisitObject2(Object2 obj)
        {
            Console.WriteLine($"visited {obj.GetType().Name}");
        }
    }
}
