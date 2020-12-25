using System.Threading.Tasks;
using EventBusDemo.Api.HttpServices;
using Microsoft.AspNetCore.Mvc;

namespace EventBusDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductHttpService _productHttpService;

        public ProductController(IProductHttpService productHttpService)
        {
            _productHttpService = productHttpService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productHttpService.GetList());
        }
    }
}