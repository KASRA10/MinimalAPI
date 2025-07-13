using MyExtensions.Endpoints;
using MyExtensions.Middlewares;
using MyExtensions.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseMiddlewares();

await app.RunAsync();
