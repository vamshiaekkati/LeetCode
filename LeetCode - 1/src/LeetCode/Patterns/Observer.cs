using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Patterns.Observer
{
    class Client
    {
        void Main()
        {
            var obs1 = new Observer1();
            var obs2 = new Observer2();

            var publisher = new Publisher();
            publisher.Subscribe(obs1);
            publisher.Subscribe(obs2);
            publisher.ChangeSome();
            publisher.Unsubscribe(obs2);
            publisher.ChangeSome();
        }
    }

    interface IObserver
    {
        void Notify();
    }

    class Publisher
    {
        private readonly HashSet<IObserver> subscribers;

        public Publisher()
        {
            subscribers = new HashSet<IObserver>();
        }

        public void Subscribe(IObserver observer)
        {
            subscribers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            subscribers.Remove(observer);
        }

        public void ChangeSome()
        {
            this.Notify();
        }

        private void Notify()
        {
            foreach (var sub in subscribers)
                sub.Notify();
        }
    }

    class Observer1 : IObserver
    {
        public void Notify()
        {
            Console.WriteLine($"{this.GetType().Name} got notify");
        }
    }

    class Observer2 : IObserver
    {
        public void Notify()
        {
            Console.WriteLine($"{this.GetType().Name} got notify");
        }
    }
}
