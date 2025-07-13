using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyExtensions.Endpoints.Contracts;

namespace MyExtensions.Endpoints
{
    public static class EndpointExtension
    {
        //? This MEthod find our Endpoints and select them and register them in DI.
        public static void AddEndpoints(this IServiceCollection services)
        {
            Assembly assembly = typeof(EndpointExtension).Assembly;
            assembly
                .GetTypes()
                .Where(type => type.IsClass && type.IsAssignableTo(typeof(IEndpoint)))
                .Select(endpointType =>
                    ServiceDescriptor.Transient(typeof(IEndpoint), endpointType)
                )
                .ToList()
                .ForEach(endpointDescriptor => services.TryAddEnumerable(endpointDescriptor));
        } // Goal is register Endpoints in DI

        //? MapEndpoint To Get IEndpointRouteBuilder as Parameter. register Endpoint ASP.NET Router
        public static void MapEndpoints(this WebApplication app)
        {
            app.Services.GetServices<IEndpoint>()
                .ToList()
                .ForEach(endpoint => endpoint.MapEndpoint(app));
        }
    }
}
