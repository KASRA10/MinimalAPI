using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAPITwo.Middlewares.Contracts;
using MAPITwo.Services;
using Microsoft.VisualBasic;

namespace MAPITwo.Middlewares
{
    public class BrowserCheckMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IBrowserCheckService service;

        public BrowserCheckMiddleware(RequestDelegate next)
        {
            _next = next;
            service = ServiceFactory.CreateCheckService();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool result = service.Validate(context);

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
