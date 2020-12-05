using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace AspNetCoreHttpRequests.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeographyController : ControllerBase
    {
        private readonly ILogger<GeographyController> logger;
        private readonly HttpClients.DomainServiceClient httpClient;

        public GeographyController(ILogger<GeographyController> logger, HttpClients.DomainServiceClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
        }

        [HttpGet("countries")]
        public async Task<IEnumerable<string>> GetCountryData()
        {
           return await this.httpClient.GetCountryListAsync();
        }

    }
}
