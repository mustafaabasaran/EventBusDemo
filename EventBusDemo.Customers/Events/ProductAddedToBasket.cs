using System;
using EventBusDemo.Common.Messages;

namespace EventBusDemo.Customers.Events
{
    public class ProductAddedToBasket : IEvent
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public ProductAddedToBasket(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}