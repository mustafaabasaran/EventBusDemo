using EventBusDemo.Customers.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace EventBusDemo.Customers.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
            
        }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>().ToTable("Basket");
            modelBuilder.Entity<Basket>().HasMany(x=> x.Items);

            modelBuilder.Entity<BasketItem>().ToTable("BasketITem");
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
    }
}