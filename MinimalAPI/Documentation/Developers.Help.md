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
