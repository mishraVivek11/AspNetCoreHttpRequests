using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreHttpRequests.HttpClients
{
    public class HttpRequestAuthHdrHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpRequestAuthHdrHandler(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(!request.Headers.Contains("Authorization"))
            {
                if(!this.httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Authorization header missing")
                    };
                }
                request.Headers.Add("Authorization", this.httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault());
            }
            if (!request.Headers.Contains("CorrelationId"))
            {
                if (!this.httpContextAccessor.HttpContext.Request.Headers.ContainsKey("CorrelationId"))
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("CorrelationId header missing")
                    };
                }
                request.Headers.Add("CorrelationId", this.httpContextAccessor.HttpContext.Request.Headers["CorrelationId"].FirstOrDefault());
            }
            return  await base.SendAsync(request, cancellationToken);
        }
    }
}
