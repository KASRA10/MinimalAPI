# Developers Documentation - KasraK10 Basic API

**Point**: To Run HTTPS: `dotnet watch --lunch-profile=https`

Default ENdPoint ==> Home ==> "/"

Get ==> Get Method, MapGet("Path| Pattern", Delegate| Lambda Function);

**Point:** On browser, all requests type are by default get.

## Delegate

Create a method signature. Inputs are signature

**_Delegate method ==> Process Handler_**

**_Pattern == path EX: /api/webhook_**

## Sections

For patterns or path.

\* Sections /…/…/ ==> three sections

We hae 2 types of section (static|dynamic):

**Static** = "/profiles/kasra

**Dynamic** = /profiles/{userName}" **==>** Delegate should access to that dynamic section

## static Pattern | Path | Address

`app.MapGet("/browsers", static () => "Edge\nChrome\nFireFox\nOpera\nArc\nVivaldi\nSafari");`

OutPut:

```text
Edge
Chrome
FireFox
Opera
Arc
Vivaldi
Safari
```

## Dynamic Pattern | Path | Address

**FromRoute == Dynamic Section on Path | pattern** ==> it adds on path.

`app.MapGet(
    "/api/{companyName}/priority",
    ([FromRoute] string companyName) => $"I like To Work In \"{companyName}\".")
`

EX:

**Endpoint:** <http://localhost:5020/api/Microsoft/priority>

OutPut:

`I like To Work In "Microsoft".`

**FromQuery** ===> mean with ? on path be added, data will added on path | route | pattern

**_? Parameter Name = Value.....separator: &_**

EX:

`app.MapGet(
"/api/{brand}",
([FromRoute] string brand, [FromQuery] string point, [FromQuery] string alphabet) =>
$"Parent: {brand}\nComment on: {point}\nstar value: \"{alphabet}\"");`

<http://localhost:5020/api/Nikee?point=Perfect&alphabet=A>

## Post

**Post** ==> Send Data

**Classic HTML Form** ==> FromForm

`app.MapPost(
"/api/registration",
(
[FromForm] string name,
[FromForm] string surname,
[FromForm] string email,
[FromForm] string pass
) =>
$"New User:\nName: {name}\nSurName: {surname}\nEmail: {email}\nPassWord: {pass}\nHas Been Registered Successfully"
)
.DisableAntiforgery();`

OutPut:

````text
New User:
Name: Kasra
SurName: Hosseini
Email: Example@gmail.com
PassWord: 123456
Has Been Registered Successfully```
````

## DisableAntiforgery()

**DisableAntiforgery()** is a method in ASP.NET Core that disables anti-forgery token validation for a specific endpoint. Normally, ASP.NET Core uses anti-forgery tokens to prevent Cross-Site Request Forgery (CSRF) attacks, which occur when a malicious site tricks a user into submitting unintended requests to a trusted site.
Why Use DisableAntiforgery()?

- **Development Convenience**: If you're testing APIs locally and don't want to deal with CSRF protection, disabling it can make debugging easier.
- **Non-Browser Clients**: If your API is consumed by non-browser clients (like mobile apps or IoT devices), CSRF protection might not be necessary.
- **Performance Optimization**: In some cases, disabling anti-forgery checks can slightly improve performance, though security should always be the priority.

## From Body for POST Json ==> PayLoad

0- Create A Class Compatible | According to our data (Here Login with two fields):

```text
public class Login
   {
       public required string Login_UserName { get; set; }
       public required string Login_PassWord { get; set; }
   }
```

1- Create a function class and put a function to return TypedResult.

```text
public static IResult HandleLogin([Microsoft.AspNetCore.Mvc.FromBody] Login data)
       {
           if (data.Login_UserName == "admin" && data.Login_PassWord == "admin")
           {
               return TypedResults.Ok(
                   $"User with this UserName: {data.Login_UserName} and this Pass: \"{data.Login_PassWord}\" has Logged in on {DateTime.Now}"
               );
           }
           else
           {
               return TypedResults.BadRequest("Just Admin is Accessible! 😁");
           }
       }
```

2- Instead using a Anonymous Function, used named function:

`app.MapPost("/api/login", Result.HandleLogin).DisableAntiforgery();`

3- EX:

**EndPoint:** <http://localhost:5020/api/login>
PayLoad (JSON):

```text
{
  "Login_UserName": "admin",
  "Login_PassWord": "admin"
}
```

OutPut:
`"User with this UserName: admin and this Pass: \"admin\" has Logged in on 06/02/2025 02:14:33"`

```text
{
  "Login_UserName": "test",
  "Login_PassWord": "admin"
}
```

OutPut:
(400 Bad Request)
`"Just Admin is Accessible! 😁"`

## Middleware

middleware is a software that's assembled into an app pipeline to handle request s and responses. Each Middleware:

- Chooses Whether to pass the request to the next component in the pipeline.
  - Can perform work before and after the next component in the pipeline.
  - Convention-based
  - Inline

Some process, functions, features on request and response until reach to service sytem or user and the end.

```text
 //? preLogin
    await next(); //? PipLine
//? PostLogin
```

## Kestrel

Application Server. ASP.net is Executes on Kestrel or by Kestrel Something Like Operation System.

## APS.Net Core Pipeline

Route of passing request and response is pipeline. Next(); ==>going to the next step on pipe line.

**Chain Line Of Responsibility Pattern:**

Is in the software.

**POINT:**

**_Global Exception Handling_** ==> all code in a try and catch to see it even works or not. ==> prevent crashing, freezing, putting out of software. If programmer forgot to use local Exception handling, this handle generally exception on all code and everywhere.

**Context** ==> anything is happening on this context and can be a http or other. The type of it as well.

**Next** ==> create pipeline and go to next step | middleware | process | function and…

**POINT:** Local Exception Handling is try and catch we always use.

**POINT:** this app.use or global exception is a middleware. It is a Middleware Inline.

**POINT:** Each Endpoint is branch middleware

**POINT:** Each middleware is a separate threads and distinct process.

`Middleware Login => Login topic`

Generally if write logger, it logs everywhere but,
Is better before exception handling | global exception handling.

**POINT:** the log of error is distinct as well.

**POINT:** Sequence and order of registration of Middleware's are important.

## Logging (Log Data)

Logging (Log Data [IP, Browser,USer, RateDate, ResDate, Operation, ...]) ==> is a Middleware

EX: **Default logger on Console**

```text
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
```

**POINT:**

```text
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
```

Context can mean HTTP Request and Response.

With POST Method ==> **Status: 404 Not Found**

## ConventionBased MiddleWare

**PONT:**
They should register.

1- Create in Distinctively:

```text
 //? Convention-Based
    public class ConventionBased
    {
        private RequestDelegate _next;

        public ConventionBased(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/middleware")
            {
                await context.Response.WriteAsync("MiddleWare . . .");
            }
            else
            {
                await _next(context);
            }
        }
    }
```

2- Register On Program

```text
//? Registration of a COnventionBased MiddleWare
app.UseMiddleware<ConventionBased>();
```

## MiddleWAre To Check Browser

```text
public class BrowserCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public BrowserCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool InvalidBrowser = context
                .Request.Headers["User-Agent"]
                .Any(static hv => hv.Contains("Edg"));

            if (InvalidBrowser)
            {
                await context.Response.WriteAsync("Can Not Support Your Browser!");
            }
            else
            {
                await _next(context);
            }
        }
    }
```

`//? Respiration of BrowserCheckMiddleWare
app.UseMiddleware<BrowserCheckMiddleware>();`

# Athentication Header

1- Define MiddleWare

```text
 public class AuthenticationHeaderMiddleWare
    {
        public async Task InvokeAsync(HttpContext context)
        {
            bool ValidAppKey = context
                .Request.Headers["User-App-Key"]
                .Any(static hv => hv.Contains("MyApp10X"));

            bool ValidAuthToken = context
                .Request.Headers["User-Auth-Token"]
                .Any(static hv => hv.Contains("1111-xxxx-yyyy-0000"));

            if (ValidAppKey && ValidAuthToken)
            {
                await context.Response.WriteAsync("WelCome Back");
            }
            else
            {
                await context.Response.WriteAsync("Authentication Failed, CAll Admin!");
            }
        }
    }
```

2- Resigner it
`//? Registration Of AuthenticationHEaderMiddleWare
app.UseMiddleware<AuthenticationHeaderMiddleWare>();`

**POINT:**It's PLase Is At First After Logger, because it is better if you do not authorize data, do not be able to connect to any endpoint.

Header on Thunder or Postman:
URL Example: <http://localhost:5020?User-App-Key=MyApp10X&User-Auth-Token=1111-xxxx-yyyy-0000>

```text
User-App-Key    MyApp10X
User-Auth-Token    1111-xxxx-yyyy-0000
```
