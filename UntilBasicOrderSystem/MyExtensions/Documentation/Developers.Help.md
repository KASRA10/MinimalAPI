# Original Endpoints Guide

For Endpoint:
we can add endpoint for each things that we want.
2- we can add a MapGroup of related Endpoints:

```csharp
//? Profiles, Group of Related Endpoints
var profilesGroup = app.MapGroup("profiles");
profilesGroup.MapGet("/r1", () => "profile r1");
profilesGroup.MapGet("/r2", () => "profile r2");
profilesGroup.MapGet("/r3", () => "profile r3");
```

And on Root because it is a subthing of a groupd it's pattern is:

/profiles/r1
/profiles/r2
/profiles/r3

IEndpointRouteBuilder:
and instead manually use it or create a manual extension, for use these routed, we can use IEndpointRouteBuilder:

```csharp
interface Microsoft.AspNetCore.Routing.IEndpointRouteBuilder
Defines a contract for a route builder in an application. A route builder specifies the routes for an application.
```

```csharp
namespace MyExtensions.Endpoints
{
    public static class ProfileEnpoint
    {
        public static void MapProfile(this IEndpointRouteBuilder builder)
        {
            //? Profiles, Group of Related Endpoints
            var profilesGroup = builder.MapGroup("profiles");
            profilesGroup.MapGet("/r1", () => "profile r1");
            profilesGroup.MapGet("/r2", () => "profile r2");
            profilesGroup.MapGet("/r3", () => "profile r3");
        }
    }
}
```

And then register it on program.cs or better in middlewareExtensions.cs:

```csharp
app.MapProfile();
```

other way is do not static class and like before create a public normal class and then create an insteace of it:

```csharp
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
```

iIn middlewareExtension:

```csharp
new CustomerEndpoint().MapCustomer(app);
```

Reflection ==> Descriptor
When C# Compilor , Compile Anything create a Descriptor for them and work with Type.
Var assembly = Typeof(…).Assembly;
Assembly.GetTypes(); ==> aaray of type of all vraiables on program.

It is like query:

```csharp
public static AddEndpoints(this IServiceCollection services){

Assembly.GetTypes().Where(endpointType => Type.isClass && Type.isAssignableTo(typeof(IEndpoint)) ).Select(endpointType => ServiceDescriptor.Transient(typeof(IEndpoint), endpointType)).ToList().ForEach(endpointDescripto => services.TryAddEnumerable(endpointDescriptor));

}
```

**POINT:** means of Assembly in C# is .dll or.exe file

In MiddlewareExtension:
call or register Goup of endpoint:

```csharp
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
```

**Point:** Static classes cannot implement Interfaces.

But instance classes can use interfaces==>
for a normal class and instance method:
Create a Contracts Folder in Endpoints, and create a IEndpoint.cs (Other endpiint classes should implement this Interface class)
Void MapEndpoint(IEndpointRouteBuilder builder);

IEndpointRouteBuilder:
IEndpointRouteBuilder provides methods and properties that allow you to:
• Map endpoints like controllers, Razor Pages, gRPC services, and custom routes.
• Group routes under a common prefix using MapGroup.
• Attach metadata to endpoints (e.g., authorization policies, OpenAPI tags).
• Create sub-pipelines using CreateApplicationBuilder() for advanced scenarios.

Contracts:

```csharp
	•
	• public interface IEndpoint
	•     {
	•         void MapEndpoint(IEndpointRouteBuilder builder);
	•     }
```

main Endpiooint:

```csharp
	•
	•  public void MapEndpoint(IEndpointRouteBuilder builder)
	•         {
	•             RouteGroupBuilder orderGroup = builder.MapGroup("orders");
	•             orderGroup.MapGet("o1", () => "Order1");
	•             orderGroup.MapGet("o2", () => "Order2");
	•             orderGroup.MapGet("o3", () => "Order3");
	•         }
```

register or call it in Middlewares:

```csharp
	•
	•  //? Interface - Contract
	•             IEndpoint orderEndpoint = new OrderEndpoint();
	•             orderEndpoint.MapEndpoint(app);
```

But we want make it automatic, so we can like before create mEndpointExtensions and all Endpoint create and call. Register there.

```csharp
	• public static class EndpointExtension
	•     {
	•         public static void AddEndpoints(this IServiceCollection services) { } // Goal is register Endpoints in DI
	•     }
```

Reflection in C#:
Create a Descriptor for any flow, function, and class and interfaces and uses type class. Typeof();

Reflection is a feature provided by the System.Reflection namespace that allows you to:
• Discover type information (classes, interfaces, enums, etc.)
• Access members like methods, properties, fields—even private ones!
• Create instances dynamically
• Invoke methods or access properties without knowing them at compile time

Key Classes in System.Reflection
Class Purpose
Type Represents type metadata
MethodInfo Info about methods
PropertyInfo Info about properties
FieldInfo Info about fields
ConstructorInfo Info about constructors
Assembly Info about loaded assemblies

To use for example assembly info by Reflection:

```csharp
var assembly = typeof(EndpointExtensiion).Assembly;
assembly. ==> show all variables, classes, ….
assembly.GetTypes() ==> show all types of values in array form.
```

POINT: it likes query. We use this Reflection Feature in EndpointExtensions:

```csharp
//? This Method find our Endpoints and select them and register them in DI.

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
```

```csharp
	• Assembly assembly = typeof(EndpointExtension).Assembly ==> Assemb;y type of Reflection feature can access to all info entire Assembly of your prohram.
	•   .GetTypes() ==> Gets all types defined in this assembly.
		Returns:   An array that contains all the types that are defined in this assembly.
	•  .Where(type => type.IsClass && type.IsAssignableTo(typeof(IEndpoint))) ==> where is afeature can do it on arrays to finde something there, Isclass ==> bool, isassaignable to ==>
	type.IsAssignableTo(typeof(IEndpoint))
	This line checks if type:
		• Is a class (type.IsClass)
		• And can be assigned to a variable of type IEndpoint
	In other words, it filters for classes that implement or inherit from IEndpoint.
	.ForEach(endpointDescriptor => services.TryAddEnumerable(endpointDescriptor));
	This is a LINQ-style ForEach call on a collection of ServiceDescriptor objects (likely representing endpoint registrations). For each descriptor, it calls:
	csharp
	services.TryAddEnumerable(endpointDescriptor);
	Which means:
		• It tries to add the service descriptor to the DI container.
		• But only if that exact implementation hasn’t already been registered for the same service type.
	•
```

And now, as we used Contracts or Interfaces to use Endpint and they have One Method which gets IEndpointRouteBuilder we should create a MapEndpont Methoid in EndpointExtension.cs as well and add it to the above method:

```csharp
	•  //? MapEndpoint To Get IEndpointRouteBuilder as Parameter. register Endpoint ASP.NET Router
	•         public static void MapEndpoints(this WebApplication app)
	•         {
	•             app.Services.GetServices<IEndpoint>()
	•                 .ToList()
	•                 .ForEach(endpoint => endpoint.MapEndpoint(app));
	•         }
```

And then should add serivce to ServiceExtensions as well because we used IServiceCollections and should register serivice as well.

```csharp
public static class ServiceExtension
    {
        public static void AddServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddOpenApi();
            services.AddBrowserCheck(configuration);
            services.AddEndpoints();
        }
    }
```

in ServiceExtensions we add Endpoints to Container and in MiddleWhereExtensions we RegisterThem to RouteBuilder:

```csharp
app.MapEndpoints();
```

```csharp
public class OrderEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder builder)
        {
            RouteGroupBuilder orderGroup = builder.MapGroup("orders");
            orderGroup.MapGet("place-order", PlaceOrderAsync);
        }
        static Task<IResult> PlaceOrderAsync()
        {
            return Task.FromResult(Results.Ok("Order Placed"));
        }
        //! or we can use variable and async:
        // async Task<IResult> PlaceOrderAsync()
        // {
        //     var result = await Task.FromResult(Results.Ok("Order PLaces"));
        //     return result;
        // }
    }
```

What is Iresult Interface Class:

In C#, especially in ASP.NET Core Minimal APIs, IResult is an interface that represents the outcome of an HTTP endpoint—like a response that gets sent back to the client.
🧩 What IResult Actually Does
It defines a contract for:
• Executing a response via ExecuteAsync(HttpContext)
• Customizing status codes, headers, and body content
• Supporting flexible return types from endpoint handlers

REPR Design Pattern ==> just for a Web API Endpoint.
The REPR Design Pattern defines Web API endpoints as having three components: a Request, an Endpoint, and a Response. It simplifies the frequebtly-used MVC pattern and is more focused on API development.
Component which is your API Endpoint has 3 parts:

- Request
- Endpoint
- Response

**POINT:** it is better for data model use record type.

```csharp
public record Name(parameters);
```

Method injection pattern:

🛒 Scenario: Placing an Order with a Notification
You have:
• An IOrderService that handles order logic.
• An INotificationService that sends confirmation messages.
• You only need the notification service inside the PlaceOrder method, not across the whole class.
🧪 Step-by-Step Example

```csharp
public interface INotificationService
{
    void SendConfirmation(string message);
}
public class EmailNotificationService : INotificationService
{
    public void SendConfirmation(string message)
    {
        Console.WriteLine($"Email sent: {message}");
    }
}
public class OrderService
{
    public void PlaceOrder(string product, INotificationService notifier)
    {
        Console.WriteLine($"Order placed for: {product}");
        notifier.SendConfirmation($"Your order for {product} has been confirmed.");
    }
}
```

🧠 How It Works
• OrderService doesn’t hold a reference to INotificationService.
• Instead, the dependency is injected directly into the method PlaceOrder.
• This keeps the class lightweight and focused, especially if the dependency is only needed occasionally.

POINT: Record Type Class:

    What Is a Record Type?
    A record is a reference type that:
    	• Uses value-based equality (two records with the same data are considered equal)
    	• Supports immutability by default (init-only setters)
    	• Has built-in support for ToString(), Equals(), and GetHashCode()
    	• Can be deconstructed like tuples
    	• Supports the with expression for non-destructive mutation

```csharp
	public record OrderRequest(string ProductId, int Quantity);
```

    This creates a class with:
    	• Two read-only properties
    	• A constructor
    	• Value-based equality
    	• A nice ToString() output
    🚀 Why Use It with MapPost?
    In Minimal APIs (MapPost, MapGet, etc.), you often need to bind incoming JSON data to a model. Record types are ideal for this because:
    Feature	Benefit in MapPost
    init-only properties	Ensures immutability
    Concise syntax	Cleaner endpoint code
    Value equality	Easier testing & comparison
    with expression	Simple updates to request data
    Deconstruction	Easy access to individual fields
    •

```csharp
	 orderGroup.MapPost("place-order", PlaceOrderAsync).WithTags("Order System");
```

    Adds the Microsoft.AspNetCore.Http.Metadata.ITagsMetadata to EndpointBuilder.Metadata for all endpoints produced by builder.
    The OpenAPI specification supports a tags classification to categorize operations into related groups. These tags are typically included in the generated specification and are typically used to group operations by tags in the UI.
    Returns:
      A RouteHandlerBuilder that can be used to further customize the endpoint.

    **POINT**: for Services which are using One/ equal Interfac Like SMS and Email that use INotificationService, we should use keyed word to register them like services.AddSKeyedSingelton<>("key);

```csharp
	            services.AddKeyedSingleton<INotificationService, SmsNotification>("sms");
	            services.AddKeyedSingleton<INotificationService, EmailNotification>("Email");
```

in inHandler:

```csharp
	  [FromKeyedServices("Sms")] INotificationService notificationService
```

and orderExtension:

```csharp
	//? Notification, Family Services
	            notificationService.SendMessageAsync(
	                $"Hi, {request.CustomerNAme}, Order id: {response.OrderId}",
	                request.MobileNumber
	            );
```
