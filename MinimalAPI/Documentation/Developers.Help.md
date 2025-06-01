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
