using Microsoft.Extensions.Primitives;

namespace MyExtensions.Middlewares.Contracts
{
    public interface IBrowserCheckService
    {
        public bool Validate(StringValues header);
    }
}
