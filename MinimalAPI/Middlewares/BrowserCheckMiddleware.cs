namespace MinimalAPI.Middlewares
{
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
                .Any(static hv => hv.Contains("Firefox"));
                // How .Any() Works: Example:
                // string[] browsers = { "Chrome", "Firefox", "Safari" };
                // bool hasFirefox = browsers.Any(b => b.Contains("Firefox")); // Returns true

            if (InvalidBrowser)
            {
                // await context.Response.WriteAsync("Can Not Support Your Browser!");
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("This Browser is Forbidden");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
