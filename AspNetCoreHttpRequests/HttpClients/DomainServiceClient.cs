using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCoreHttpRequests.HttpClients
{
    public class DomainServiceClient
    {
        private readonly HttpClient httpClient;

        public DomainServiceClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<string[]> GetCountryListAsync()
        {
            var response = await httpClient.GetAsync("countries");
            response.EnsureSuccessStatusCode();

            //var data = await response.Content.ReadAsStringAsync();
            //Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(data);

            var data = await response.Content.ReadAsAsync<string[]>();

            return data;
        }
    }
}
