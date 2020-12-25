using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventBusDemo.Customers.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok("Customer service is up.");
        }
    }
}