using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Patterns.Adapter
{
    class Client
    {
        void Main()
        {
            var clientData = "some data";
            Service service = new Service();
            // service.ServiceMethod() //cannot use;
            IClientInterface clientInterface = new Adapter(service);
            var serviceMethodResult = clientInterface.Method(clientData);
        }
    }

    interface IClientInterface
    {
        int Method(string data);
    }

    class Adapter : IClientInterface
    {
        private readonly Service service;

        public Adapter(Service service)
        {
            this.service = service;
        }

        public int Method(string data)
        {
            var specialData = convertToSpecialData(data);
            return service.ServiceMethod(specialData);
        }

        private int convertToSpecialData(string data)
        {
            return data.Length;
        }
    }

    public class Service
    {
        internal int ServiceMethod(int specialData)
        {
            return specialData;
        }
    }
}
