using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExtensions.Endpoints.Contracts;

namespace MyExtensions.Endpoints
{
    public class BasketEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder builder)
        {
            RouteGroupBuilder basketGroup = builder.MapGroup("basket");
            basketGroup.MapGet("b1", () => "basket1");
            basketGroup.MapGet("b2", () => "basket2");
            basketGroup.MapGet("b3", () => "basket3");
        }
    }
}
