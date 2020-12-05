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
    public class CurrenciesController :ControllerBase
    {
        private readonly ILogger<CurrenciesController> _logger;
        private readonly IWebHostEnvironment env;
        public CurrenciesController(ILogger<CurrenciesController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            this.env = env;
        }
        [HttpGet]
        public IEnumerable<string> GetAllCurrencies()
        {
            string rawData = System.IO.File.ReadAllText(this.env.ContentRootPath + "/MockData/Currency.json");
            var countries = JsonSerializer.Deserialize<Currency[]>(rawData);
            return countries.Where(cur=>!String.IsNullOrEmpty(cur.currency_name)).Select(x => x.currency_name);
        }
    }
}
