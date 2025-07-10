using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAPITwo.Middlewares
{
    public class ConventionBased
    {
        private readonly RequestDelegate _next;

        public ConventionBased(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "GET" && context.Request.Path == "/api/middleware")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("MiddleWare - Can Not Access");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
