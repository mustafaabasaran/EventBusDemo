using System.Threading.Tasks;
using EventBusDemo.Api.Commands.Baskets;
using EventBusDemo.Common.RabbitMq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventBusDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : BaseController
    {
        public BasketController(IBusPublisher busPublisher) : base(busPublisher)
        {

        }

        [HttpPost]
        public async Task<IActionResult> Post(AddProductToBasket command)
        {
            var context = GetContext();
            await BusPublisher.SendAsync(command, context);
            return Accepted();
        }
    }
}