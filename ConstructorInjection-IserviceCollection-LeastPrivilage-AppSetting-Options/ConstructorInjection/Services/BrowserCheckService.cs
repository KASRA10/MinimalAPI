using ConstructorInjection.Middlewares.Contracts;
using ConstructorInjection.Services.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace ConstructorInjection.Services
{
    public class BrowserCheckService : IBrowserCheckService
    {
        // private readonly IConfiguration _configuration; // because we want to use IConfiguration to get values from appsettings.json in Constructor injection

        private readonly BrowserCheckOptions _browserOption;

        // UseExtensions IConfiguration by using Constructor injection
        public BrowserCheckService(IOptions<BrowserCheckOptions> browserOption)
        {
            // //? UseExtensions IConfiguration by using Constructor injection
            // //? Get Configuration Value from appsettings.json
            // //? For example: "browser": "Firefox"
            //* if use constructor injection
            //_configuration = configuration;

            //?inject Settings by using IOptions interface
            _browserOption = browserOption.Value;
        }

        //? Privilege OWASP Principle
        public bool Validate(StringValues header)
        {
            // string? browser = _configuration.GetValue<string>("Browser");

            string? browser = _browserOption.Browser_Name;

            if (string.IsNullOrEmpty(browser))
            {
                return false;
            }

            bool result = header.Any(hv => hv.Contains(browser));

            return !result;
        }
    }
}
