using System;
using EventBusDemo.Common.Messages;

namespace EventBusDemo.Api.Commands.Orders
{
    [MessageNameSpace("orders")]
    public class CreateOrder : ICommand
    {
        public Guid Id { get; set; }

        public CreateOrder()
        {
            Id = Guid.NewGuid();
        }
    }
}