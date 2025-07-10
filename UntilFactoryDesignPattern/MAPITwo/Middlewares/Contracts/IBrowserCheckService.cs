using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAPITwo.Middlewares.Contracts
{
    public interface IBrowserCheckService
    {
        public bool Validate(HttpContext context);
    }
}
