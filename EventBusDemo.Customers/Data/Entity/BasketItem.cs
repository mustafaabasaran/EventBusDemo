using System;
using System.Collections.Generic;
using EventBusDemo.Common.Models;

namespace EventBusDemo.Customers.Data.Entity
{
    public class BasketItem : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}