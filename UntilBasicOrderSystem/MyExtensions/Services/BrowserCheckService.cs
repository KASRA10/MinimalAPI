using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using MyExtensions.Middlewares.Contracts;
using MyExtensions.Services.Options;

namespace MyExtensions.Services
{
    public class BrowserCheckService : IBrowserCheckService
    {
        private readonly BrowserCheckOptions _browserOption;

        public BrowserCheckService(IOptions<BrowserCheckOptions> browserOption)
        {
            _browserOption = browserOption.Value;
        }

        public bool Validate(StringValues header)
        {
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
