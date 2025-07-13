namespace MyExtensions.Endpoints
{
    public class CustomerEndpoint
    {
        public void MapCustomer(IEndpointRouteBuilder builder)
        {
            var customerGroup = builder.MapGroup("customers");
            customerGroup.MapGet("/c1", () => "customer r1");
            customerGroup.MapGet("/c2", () => "customer r2");
            customerGroup.MapGet("/c3", () => "customer r3");
        }
    }
}
