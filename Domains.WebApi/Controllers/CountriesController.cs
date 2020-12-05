using Domains.WebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domains.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController :ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly IWebHostEnvironment env;

        public CountriesController(ILogger<CountriesController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            this.env = env;
        }

        [HttpGet]
        public IEnumerable<string> GetAllCountries()
        {
            string rawData = System.IO.File.ReadAllText(this.env.ContentRootPath + "/MockData/Countries.json");
            var countries = JsonSerializer.Deserialize<Country[]>(rawData);
            return countries.Select(x=>x.country);
        }
    }
}
