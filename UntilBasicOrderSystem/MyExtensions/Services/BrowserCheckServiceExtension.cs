using MyExtensions.Middlewares.Contracts;
using MyExtensions.Services.Options;

namespace MyExtensions.Services
{
    public static class BrowserCheckServiceExtension
    {
        public static void AddBrowserCheck(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.Configure<BrowserCheckOptions>(options =>
            {
                configuration.GetSection("Browser").Bind(options);
            });

            services.AddTransient<IBrowserCheckService, BrowserCheckService>();
        }
    }
}
