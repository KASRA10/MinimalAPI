namespace MyExtensions.Middlewares.Contracts
{
    public static class BrowserCheckMiddlewareExtension
    {
        public static void UseBrowserCheck2(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                app.UseMiddleware<BrowserCheckMiddleware>();
            }
        }

        public static void UseBrowserCheck1(this WebApplication app)
        {
            if (app.Environment.IsProduction())
            {
                app.UseMiddleware<BrowserCheckMiddleware>();
            }
        }
    }
}
