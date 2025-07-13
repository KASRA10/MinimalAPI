using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExtensions.Endpoints.Contracts;

namespace MyExtensions.Services
{
    public class SmsNotification(ILogger<SmsNotification> logger) : INotificationService
    {
        public Task SendMessageAsync(string message, string destination)
        {
            logger.LogInformation($"Send SMS Message : {message},\nMobileNumber: {destination}");
            return Task.CompletedTask;
        }
    }
}
