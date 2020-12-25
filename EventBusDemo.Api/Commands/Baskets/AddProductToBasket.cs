using System;
using EventBusDemo.Common.Messages;
namespace EventBusDemo.Api.Commands.Baskets
{
    [MessageNameSpace("customers")]
    public class AddProductToBasket : ICommand 
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public AddProductToBasket(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}