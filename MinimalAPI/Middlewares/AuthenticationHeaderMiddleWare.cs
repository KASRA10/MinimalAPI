namespace MinimalAPI.Middlewares
{
    public class AuthenticationHeaderMiddleWare
    {
        private readonly RequestDelegate _next;

        public AuthenticationHeaderMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool ValidAppKey = context
                .Request.Headers["User-App-Key"]
                .Any(static hv => hv.Contains("MyApp10X"));

            bool ValidAuthToken = context
                .Request.Headers["User-Auth-Token"]
                .Any(static hv => hv.Contains("1111-xxxx-yyyy-0000"));

            if (ValidAppKey && ValidAuthToken)
            {
                await context.Response.WriteAsync("WelCome Back");
            }
            else
            {
                await context.Response.WriteAsync("Authentication Failed, CAll Admin!");
            }
        }
    }
}
