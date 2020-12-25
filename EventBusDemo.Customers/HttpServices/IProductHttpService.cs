using System;
using System.Threading.Tasks;
using EventBusDemo.Customers.Models;

namespace EventBusDemo.Customers.HttpServices
{
    public interface IProductHttpService
    {
        Task<Product> GetAsync(Guid id);
    }
}