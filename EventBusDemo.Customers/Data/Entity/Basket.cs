using System;
using System.Collections.Generic;

namespace EventBusDemo.Customers.Data.Entity
{
    public class Basket
    {
        public Guid CustomerId { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}