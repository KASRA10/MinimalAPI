using MyExtensions.Endpoints;
using MyExtensions.Endpoints.Contracts;
using MyExtensions.Middlewares.Contracts;
using Scalar.AspNetCore;

namespace MyExtensions.Middlewares
{
    public static class MiddlewareExtension
    {
        public static void UseMiddlewares(this WebApplication app)
        {
            app.Logger.LogInformation("Start Program - Hi :))");
            app.Use(
                async (context, next) =>
                {
                    app.Logger.LogInformation("Before");
                    await next();
                    app.Logger.LogInformation("After");
                }
            );

            app.UseBrowserCheck2(app.Environment);

            app.Use(
                async (context, next) =>
                {
                    try
                    {
                        await next();
                    }
                    catch (Exception e)
                    {
                        app.Logger.LogError(e.Message);
                        await context.Response.WriteAsync(
                            e.Message + "\nGlobal Custom Error Message By Developer"
                        );
                    }
                }
            );

            app.Use(
                async (context, next) =>
                {
                    if (context.Request.Method == "GET" && context.Request.Path == "/branch")
                    {
                        await context.Response.WriteAsync("Custom Content");
                    }
                    else
                    {
                        await next(context);
                    }
                }
            );

            //? Inline Endpoint
            app.MapGet("/", () => "Ordering System");
            app.MapGet(
                "/error",
                () =>
                {
                    throw new Exception("Custom Message");
                }
            );

            //? Static Endpoint
            app.MapProfile();

            //? creating Instance Endpoint
            new CustomerEndpoint().MapCustomer(app);

            //* Iterable Code ==> DRY ==> Not Good, Should Improve it
            // //? Interface - Contract
            // IEndpoint orderEndpoint = new OrderEndpoint();
            // orderEndpoint.MapEndpoint(app);

            app.MapEndpoints();

            app.MapOpenApi();
            app.MapScalarApiReference();
        }
    }
}
