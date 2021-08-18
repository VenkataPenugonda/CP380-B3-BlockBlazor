using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using CP380_B1_BlockList.Models;
using Microsoft.AspNetCore.Mvc;

namespace CP380_B3_BlockBlazor.Data
{
    public class BlockService
    {
        // TODO: Add variables for the dependency-injected resources
        //       - httpClient
        //       - configuration
        //
        static HttpClient clientFactory;
        public IConfiguration configuration;

        //
        // TODO: Add a constructor with IConfiguration and IHttpClientFactory arguments
        //
        public BlockService() { }
        public BlockService(IConfiguration c, IHttpClientFactory http)
        {
            clientFactory = http.CreateClient();
            configuration = c.GetSection("BlockService");
        }

        //
        // TODO: Add an async method that returns an IEnumerable<Block> (list of Blocks)
        //       from the web service
        //
        public async Task<IEnumerable<Block>> ListBlocks()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, configuration["url"]);            
            var response = await clientFactory.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<IEnumerable<Block>>(responseStream);
            }
            return Array.Empty<Block>();
        }
    }
}
