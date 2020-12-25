using System;
using EventBusDemo.Common.Messages;

namespace EventBusDemo.Customers.Events
{
    public class OrderCompleted : IEvent
    {
        public Guid Id { get; }

        public OrderCompleted(Guid id)
        {
            Id = id;
        }
    }
}