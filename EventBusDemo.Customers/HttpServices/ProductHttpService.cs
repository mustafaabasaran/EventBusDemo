using System;
using System.Net.Http;
using System.Threading.Tasks;
using EventBusDemo.Customers.Models;
using Newtonsoft.Json;

namespace EventBusDemo.Customers.HttpServices
{
    public class ProductHttpService : IProductHttpService
    {
        private readonly HttpClient _client;

        public ProductHttpService(HttpClient client)
        {
            _client = client;
        }
        
        public async Task<Product> GetAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"product/{id}");
            var response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                response.Content.Dispose();
                return JsonConvert.DeserializeObject<Product>(content);
            }

            throw new Exception("Product Service Connecttion Error");
        }
    }
}