using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace AspNetCoreHttpRequests.HttpClients
{
    public class HttpRequestTracingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string correlationId = request.Headers.GetValues("CorrelationId").FirstOrDefault();

            LogRequest(correlationId,request);

            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
           
            LogResponse(correlationId,response);

            return response;
        }

        private void LogResponse(string correlationId, HttpResponseMessage response)
        {
            byte[] responseMessage;

            if (response.IsSuccessStatusCode)
                responseMessage =  response.Content.ReadAsByteArrayAsync().Result;
            else
                responseMessage = System.Text.Encoding.UTF8.GetBytes(response.ReasonPhrase);

            //Trace
            var reponseInfo = string.Format("Timestamp:{0}, CorrelationId: {1}, Request {2} : {3}", DateTime.Now, correlationId, response.StatusCode, responseMessage);
        }

        private void LogRequest(string correlationId, HttpRequestMessage request)
        {
            //Trace 
            var requestInfo = string.Format("Timestamp:{0}, CorrelationId: {1}, Request {2} : {3}", DateTime.Now, correlationId, request.Method, request.RequestUri);
        }
    }
}
