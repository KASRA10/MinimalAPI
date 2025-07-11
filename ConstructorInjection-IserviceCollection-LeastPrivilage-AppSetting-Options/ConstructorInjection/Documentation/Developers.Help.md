# Constructor Injection

**EX?:** Add service to the parameter and does not need to a creator.  
Anything that you need, get form Constructor. All dependencies on input.

---

## Lifecycle Management of DI

---

## 🛠️ IServiceCollection in C#—Your Dependency Injection Toolkit

In C#, particularly when using .NET Core or .NET 5/6/7+, `IServiceCollection` is a key interface that powers the built-in dependency injection (DI) system. It's part of the `Microsoft.Extensions.DependencyInjection` namespace and acts like a container for service registrations.

### ⚙️ What it does:

- It provides methods to register application services (like logging, configuration, custom classes) with specific lifetimes:
  - **Singleton**: One instance for the app's life
  - **Scoped**: One instance per request
  - **Transient**: A new instance every time it's requested
- These registrations tell the DI system how to create and manage instances of your services.

### 📦 Where it's typically used:

You'll often see it inside the `Startup.cs` or `Program.cs` file:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddSingleton<IMyService, MyService>();
    services.AddScoped<IRepository, Repository>();
}
```

---

## ASP.NET & IServiceCollection

Like:

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.Add... // AddTransient, AddScoped, AddSingleton …
```

### General Format Syntax:

```csharp
builder.Services.AddTransient<Definition/ InterFace, Implementation>();
```

- **Transient** ⇒ Each time create an instance.  
  In each layer, middleware, scope new instance is created.  
  For each request we have an instance.  
  Even different middleware and methods are new request so create new instance.  
  Each next on pipeline is a new request.

- **Singleton** ⇒ Create just one and once instance for all layers and requests.

- **Scoped** ⇒ Create just in one scope.  
  For each scope just for each new entering request.  
  First request with all process and response == 1 scope.

**POINT:** Services should be before:

```csharp
var app = builder.Build();
```

---

## 🔐 Least Privilege Principle — OWASP’s Take

The Principle of Least Privilege (PoLP) is a foundational security concept promoted by OWASP and widely adopted across cybersecurity best practices.  
It means that users, applications, and systems should be granted only the minimum access necessary to perform their tasks.

**POINT:**

---

## 📦 What is `StringValues`?

`StringValues` is a struct from the `Microsoft.Extensions.Primitives` namespace.  
It’s designed to represent zero, one, or many string values—commonly used for things like HTTP headers, query parameters, or form fields, where a key might have multiple values.

```csharp
using Microsoft.Extensions.Primitives;

public bool Validate(StringValues header)
{
    // You can treat it like an array of strings
    foreach (var value in header)
    {
        if (value.Contains("Kasra"))
            return true;
    }
    return false;
}
```

---

## Application Configuration

- `Lunch.json` ⇒ When app is run at first and after run cannot change it again at runtime!
- `setting.json` ⇒ Exactly after launch project — LunchSetting is for whole program.
- `appsettings.Development.json`
- `appsettings.json`
- `appsettings.Production.json`
- **Command Line Argument**

```json
"ASPNETCORE_ENVIRONMENT": "Development"
```

This is the place where app is run.

Add your setting and read on your programs by Configuration namespace which is for ASP.NET.

```
ASPNETCORE_ENVIRONMENT: Development = Production
```

Or create new config

**Priority is with appsettings.json**

---

## Sample of using Configuration on Constructor Injection Pattern:

```csharp
public class BrowserCheckService : IBrowserCheckService
{
    private readonly IConfiguration _configuration; // because we want to use IConfiguration to get values from appsettings.json in Constructor injection

    // UseExtensions IConfiguration by using Constructor injection
    public BrowserCheckService(IConfiguration configuration)
    {
        //? UseExtensions IConfiguration by using Constructor injection
        //? Get Configuration Value from appsettings.json
        //? For example: "browser": "Firefox"
        _configuration = configuration;
    }

    //? Privilege OWASP Principle
    public bool Validate(StringValues header)
    {
        string? browser = _configuration.GetValue<string>("Browser");
        if (string.IsNullOrEmpty(browser))
        {
            return false;
        }
        bool result = header.Any(hv => hv.Contains(browser));
        return !result;
    }
}
```

---

## POINT: New Profile on `launchSettings.json`:

```json
"httpProduction": {
  "commandName": "Project",
  "dotnetRunMessages": true,
  "launchBrowser": false,
  "applicationUrl": "http://localhost:5167",
  "environmentVariables": {
    "ASPNETCORE_ENVIRONMENT": "Development"
  }
}
```

---

## Conditional Middleware Registration

When we have different environments, we should control our middleware registration accordingly:

```csharp
if (app.Environment.IsProduction())
{
    app.UseMiddleware<BrowserCheckMiddleware>();
}
```

---

## Option Pattern

- In `Services` Folder, create `Option` Folder.
- Create a class for each section on settings that we want to read.
- Pass least setting to Services.
- Option Pattern has interface that can inject.

```csharp
// Option pattern has pattern IOptions<OptionName(our class)>
// option can inject it and add as parameter.
_option = option.Value;
```

**POINT:** Then register it in Program — define which options to use:

```csharp
builder.Services.Configure<BrowserCheckOptions>(options =>
{
    //? Inline (Dynamic)
    // options.Browser_Name = "Firefox";

    //? AppSetting (static)
    builder.Configuration.GetSection("Browser").Bind(options);
});
```
