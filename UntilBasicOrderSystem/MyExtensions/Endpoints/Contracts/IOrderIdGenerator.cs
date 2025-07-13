using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExtensions.Endpoints.Contracts
{
    public interface IOrderIdGenerator
    {
        int New();
    }
}
