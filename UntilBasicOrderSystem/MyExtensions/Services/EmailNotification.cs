using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExtensions.Endpoints.Contracts;

namespace MyExtensions.Services
{
    public class EmailNotification(ILogger<EmailNotification> logger) : INotificationService
    {
        public Task SendMessageAsync(string message, string destination)
        {
            logger.LogInformation($"Send Email Message : {message},\nMAil: {destination}");
            return Task.CompletedTask;
        }
    }
}
