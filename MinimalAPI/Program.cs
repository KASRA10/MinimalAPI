using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Functions;
using MinimalAPI.Middlewares;
using MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

#region MiddleWares

//? Logging MiddleWare
//? Logging (Log Data [IP, Browser,USer, RateDate, ResDate, Operation, ...]) ==> is a Middleware
//* EX: Default logger on Console
// app.Logger.LogInformation("Before");
// app.Logger.LogInformation("After");
// app.Logger.LogError(ex.message);
app.Use(
    async (context, next) =>
    {
        app.Logger.LogInformation("Before");
        await next(); //? PipLine
        app.Logger.LogInformation("After");
    }
);

//? Registration Of AuthenticationHEaderMiddleWare
//* http://localhost:5020?User-App-Key=MyApp10X&User-Auth-Token=1111-xxxx-yyyy-0000
app.UseMiddleware<AuthenticationHeaderMiddleWare>();

//? Respiration of BrowserCheckMiddleWare
app.UseMiddleware<BrowserCheckMiddleware>();

//* MiddleWare
//? Global Exception Handling (inline MiddleWare)
app.Use(
    async (context, next) =>
    {
        try
        {
            //? preLogin
            await next(); //? PipLine
            //? PostLogin
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

//? Branch Middleware === Branch Endpoint
app.Use(
    async (context, next) =>
    {
        if (context.Request.Method == "GET" && context.Request.Path == "/branch")
        {
            await context.Response.WriteAsync("Custom Content");
        }
        else
        {
            await next(context); //? PipLine
        }
    }
);

//? Registration of a ConventionBased MiddleWare
app.UseMiddleware<ConventionBased>();

#endregion

// Default (Home) EndPoint
app.MapGet(
    "/",
    () =>
        "Application Name: KasraK10 Minimal API - Basic\nList Of EndPoints:\n/error/{number}\n/browser\n/api/{companyName}/priority\n/api/{brand}\n/api/registration\n/api/registration\n/api/login\n/api/profiles/{userName}\n/api/profiles/{userName}\n/api/post/{postId}"
);

//! UnHandled Error Message ==> Using Global Exception Error Message
app.MapGet("/error/{number}", ([FromRoute] int number) => $"Your Id Is: {number}");

//? Get ==> Get Method, MapGet("Path| Pattern", Delegate| Lambda Function);
//* static Pattern | Path | Address
app.MapGet("/browser", static () => "Edge\nChrome\nFireFox\nOpera\nArc\nVivaldi\nSafari");

//? Dynamic Path | Pattern
//* FromRoute == Dynamic Section on Path | pattern
app.MapGet(
    "/api/{companyName}/priority",
    ([FromRoute] string companyName) => $"I like To Work In \"{companyName}\"."
);

//? FromQuery ===> mean with ? on path be added, data will added on path | route | pattern
//* http://localhost:5020/api/priceItems?itemName=PS5&state=Hot
//! Parameter Name = Value.....separator: &
app.MapGet(
    "/api/{brand}",
    ([FromRoute] string brand, [FromQuery] string point, [FromQuery] string alphabet) =>
        $"Parent: {brand}\nComment on: {point}\nstar value: \"{alphabet}\""
);

//* Post ==> Send Data
//? Classic HTML Form ==> FromForm
app.MapPost(
        "/api/registration",
        (
            [FromForm] string name,
            [FromForm] string surname,
            [FromForm] string email,
            [FromForm] string pass
        ) =>
            $"New User:\nName: {name}\nSurName: {surname}\nEmail: {email}\nPassWord: {pass}\nHas Been Registered Successfully"
    )
    .DisableAntiforgery();

//? From Body for POST Json ==> PayLoad
app.MapPost("/api/login", Result.HandleLogin).DisableAntiforgery();

//? PUT ==> For Updating | RePlacing
app.MapPut(
        "/api/profiles/{userName}",
        ([FromRoute] string userName, [FromBody] UserProfile data) =>
            $"{userName} Info Has Updated to {data.UserName}"
    )
    .DisableAntiforgery();

//? Delete
app.MapDelete("/api/post/{postId}", ([FromRoute] string postId) => $"\"{postId}\" Deleted");

app.Run();
