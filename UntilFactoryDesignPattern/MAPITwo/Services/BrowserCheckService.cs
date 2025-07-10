using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAPITwo.Middlewares.Contracts;

namespace MAPITwo.Services
{
    public class BrowserCheckService : IBrowserCheckService
    {
        public bool Validate(HttpContext context)
        {
            bool result = context.Request.Headers["User-Agent"].Any(hv => hv.Contains("Chrome"));

            return !result;
        }
    }
}
