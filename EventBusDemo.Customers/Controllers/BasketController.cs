using System;
using System.Threading.Tasks;
using EventBusDemo.Customers.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventBusDemo.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly CustomerDbContext _dbContext;

        public BasketController(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("{customerId}")]
        public async Task<ActionResult> Get(Guid customerId)
        {
            return Ok(await _dbContext.Baskets.Include(i => i.Items)
                .FirstOrDefaultAsync(s => s.CustomerId == customerId));
        }
    }
}