using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAPITwo.Middlewares.Contracts;

namespace MAPITwo.Services
{
    public class AuthenticationHeaderService : IAuthenticationHeaderService
    {
        public bool ValidAppKey(HttpContext context)
        {
            bool result = context
                .Request.Headers["MyAppKey"]
                .Any(static hv => hv.Contains("111-222-333"));

            return !result;
        }
    }
}
