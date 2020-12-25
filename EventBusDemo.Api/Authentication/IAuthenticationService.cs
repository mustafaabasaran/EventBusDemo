using EventBusDemo.Api.Models;

namespace EventBusDemo.Api.Authentication
{
    public interface IAuthenticationService
    {
        string GetToken(Customer customer);
    }
}