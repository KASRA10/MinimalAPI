using MAPITwo.Middlewares;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Logger.LogInformation("Start Program - Hi :))");
app.Use(
    async (context, next) =>
    {
        app.Logger.LogInformation("Before");
        await next();
        app.Logger.LogInformation("After");
    }
);

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

app.UseMiddleware<AuthenticationHeaderMiddleWare>();

app.UseMiddleware<BrowserCheckMiddleware>();

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

app.UseMiddleware<ConventionBased>();

app.MapGet("/", () => "Home Page By Default");

app.MapGet("/api/{city}/new", ([FromRoute] string city) => $"this is your city: {city}".ToUpper());

app.MapGet(
        "/api/register",
        ([FromForm] string userName, [FromForm] string userPass) =>
            $"New User:\nusername: {userName}\npass: {userPass}\nis registered"
    )
    .DisableAntiforgery();

app.MapGet(
        "/api/{car}",
        ([FromRoute] string car, [FromBody] Car carNumber) =>
            $"User Wanted: {car}, number: {carNumber}"
    )
    .DisableAntiforgery();

app.MapPost("/api/address", ([FromQuery] string location) => $"user Neighborhood is: {location}")
    .DisableAntiforgery();

app.Run();

public class Car
{
    public string? carNumber { get; set; }
}
