using System.Threading.Tasks;
using EventBusDemo.Customers.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventBusDemo.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _dbContext;

        public CustomerController(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("customerbyemail/{email}")]
        public async Task<ActionResult> Get(string email)
        {
            return Ok(await _dbContext.Customers
                .FirstOrDefaultAsync(s => s.Email == email));
        }
    }
}