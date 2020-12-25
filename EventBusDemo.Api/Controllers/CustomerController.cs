using System.Threading.Tasks;
using EventBusDemo.Api.Commands.Customers;
using EventBusDemo.Common.RabbitMq;
using Microsoft.AspNetCore.Mvc;

namespace EventBusDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        public CustomerController(IBusPublisher busPublisher) : base(busPublisher)
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomer command)
        {
            var context = GetContext(command.Id);
            await BusPublisher.SendAsync(command, context);
            return Accepted();
        }
    }
}