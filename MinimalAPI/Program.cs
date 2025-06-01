using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Default (Home) EndPoint
app.MapGet("/", () => "Application Name: KasraK10 Minimal API - Basic\nList Of EndPoints:");

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

//? Post ==> Send Data
//? Classic HTML Form ==> FromForm

app.Run();
