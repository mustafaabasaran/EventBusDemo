using EventBusDemo.Common.Models;

namespace EventBusDemo.Customers.Data.Entity
{
    public class Customer : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}