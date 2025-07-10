using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAPITwo.Middlewares.Contracts;
using MAPITwo.Services;
using Microsoft.VisualBasic;

namespace MAPITwo.Middlewares
{
    public class AuthenticationHeaderMiddleWare
    {
        private readonly RequestDelegate _next;

        private readonly IAuthenticationHeaderService service;

        public AuthenticationHeaderMiddleWare(RequestDelegate next)
        {
            _next = next;
            service = ServiceFactory.CreateAthService();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool result = service.ValidAppKey(context);

            if (!result)
            {
                await context.Response.WriteAsync("False AppKey");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
