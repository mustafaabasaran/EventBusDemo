using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventBusDemo.Api.Controllers
{
    public class HomeController : Controller
    {
        // GET
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok("Api is up");
        }
    }
}