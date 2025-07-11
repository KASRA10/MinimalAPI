using Microsoft.Extensions.Primitives;

namespace ConstructorInjection.Middlewares.Contracts
{
    public interface IBrowserCheckService
    {
        //? Privilege OWASP Principle ==> StringValues header
        //?interface === contract
        public bool Validate(StringValues header);
    }
}
