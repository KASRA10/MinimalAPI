using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAPITwo.Middlewares.Contracts;
using MAPITwo.Services;

namespace MAPITwo
{
    public class ServiceFactory
    {
        private ServiceFactory() { }

        public static IBrowserCheckService CreateCheckService() => new BrowserCheckService();

        public static IAuthenticationHeaderService CreateAthService() =>
            new AuthenticationHeaderService();
    }
}
