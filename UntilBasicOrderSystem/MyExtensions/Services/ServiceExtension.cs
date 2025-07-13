using Microsoft.Extensions.DependencyInjection;
using MyExtensions.Endpoints;
using MyExtensions.Endpoints.Contracts;

namespace MyExtensions.Services
{
    public static class ServiceExtension
    {
        public static void AddServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddOpenApi();
            services.AddBrowserCheck(configuration);
            services.AddEndpoints();

            services.AddSingleton<IOrderIdGenerator, OrderIdGenerator>();

            services.AddKeyedSingleton<INotificationService, SmsNotification>("Sms");
            services.AddKeyedSingleton<INotificationService, EmailNotification>("Email");
        }
    }
}
