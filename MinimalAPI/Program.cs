var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Default (Home) EndPoint
app.MapGet("/", () => "Application Name: KasraK10 Minimal API - Basic\nList Of EndPoints:");

//? Get ==> Get Method, MapGet("Path| Pattern", Delegate| Lambda Function);

app.Run();
