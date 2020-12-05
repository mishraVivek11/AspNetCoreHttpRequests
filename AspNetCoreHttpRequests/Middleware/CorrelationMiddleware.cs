using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreHttpRequests.Middleware
{
    public class CorrelationMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.Headers.TryGetValue("CorrelationId", out var correlationId);

            if (string.IsNullOrEmpty(correlationId))
            {
                context.Request.Headers.Add("CorrelationId", Guid.NewGuid().ToString());
            }
            await _next.Invoke(context);
        }
    }
}
