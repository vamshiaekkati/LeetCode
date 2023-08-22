using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Patterns.Facade
{
    class Client
    {
        void Main()
        {
            var c1 = new Component1();
            var c2 = new Component2();
            var facade = new Facade(c1, c2);
            facade.DoWork();
        }
    }

    class Component1
    {
        public void DoThis()
        {
            Console.WriteLine("Component 1 work");
        }
    }
    class Component2
    {
        public void DoThat()
        {
            Console.WriteLine("Component 2 work");
        }
    }

    class Facade
    {
        private readonly Component1 component1;
        private readonly Component2 component2;

        public Facade(Component1 component1, Component2 component2)
        {
            this.component1 = component1;
            this.component2 = component2;
        }

        public void DoWork()
        {
            component1.DoThis();
            component2.DoThat();
        }
    }
}
