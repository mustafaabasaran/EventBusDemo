using System.Threading.Tasks;
using EventBusDemo.Api.Models;

namespace EventBusDemo.Api.HttpServices
{
    public interface ICustomerHttpService
    {
        Task<Customer> GetCustomerByEmail(string email);
    }
}