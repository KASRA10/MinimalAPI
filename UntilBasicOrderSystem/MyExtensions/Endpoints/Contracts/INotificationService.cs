using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExtensions.Endpoints.Contracts
{
    public interface INotificationService
    {
        Task SendMessageAsync(string message, string destination);
    }
}
