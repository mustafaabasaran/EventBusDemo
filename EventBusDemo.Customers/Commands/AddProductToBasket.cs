using System;
using EventBusDemo.Common.Messages;

namespace EventBusDemo.Customers.Commands
{
    public class AddProductToBasket : ICommand
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public AddProductToBasket(Guid productId,
            int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}