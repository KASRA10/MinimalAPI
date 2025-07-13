using MyExtensions.Middlewares.Contracts;

namespace MyExtensions.Middlewares
{
    public class BrowserCheckMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IBrowserCheckService service;

        public BrowserCheckMiddleware(
            RequestDelegate next,
            IBrowserCheckService browserCheckService
        )
        {
            _next = next;
            service = browserCheckService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool result = service.Validate(context.Request.Headers["User-Agent"]);

            if (!result)
            {
                await context.Response.WriteAsync("Can not Support User-Agent");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
