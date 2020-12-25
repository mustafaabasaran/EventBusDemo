using System;
using EventBusDemo.Common.Messages;

namespace EventBusDemo.Api.Commands.Customers
{
    [MessageNameSpace("customers")]
    public class CreateCustomer : ICommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public CreateCustomer(string email, string password, string firstName, string lastName, string address)
        {
            this.Id = Guid.NewGuid();
            this.Email = email;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
        }
    }
}