using System;
using System.Net.Http;
using System.Threading.Tasks;
using EventBusDemo.Api.Models;
using Newtonsoft.Json;

namespace EventBusDemo.Api.HttpServices
{
    public class CustomerHttpService : ICustomerHttpService
    {
        private HttpClient _client;

        public CustomerHttpService(HttpClient client)
        {
            _client = client;
        }
        
        public async Task<Customer> GetCustomerByEmail(string email)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"customer/customerbyemail/{email}");
            var response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                response.Content.Dispose();
                return JsonConvert.DeserializeObject<Customer>(content);
            }

            throw new Exception("Customer service connecttion error.");
        }
    }
}