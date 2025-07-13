using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExtensions.Endpoints.Contracts;

namespace MyExtensions.Services
{
    public class OrderIdGenerator : IOrderIdGenerator
    {
        public int New() => new Random().Next(1, 1000);
    }
}
