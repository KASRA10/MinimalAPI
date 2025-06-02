namespace MinimalAPI.Middlewares
{
    //? Convention-Based
    public class ConventionBased
    {
        private readonly RequestDelegate _next;

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
}
