using System;
using Microsoft.Extensions.DependencyInjection;

namespace EventBusDemo.Customers.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<CustomerDbContext>();
            context.Database.EnsureCreated();
        }
    }
}