using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreService.Infrastructure.Models;

namespace StoreService.Infrastructure;

public class StoreServiceDbContext : IdentityDbContext<IdentityUser>
{
    public StoreServiceDbContext(DbContextOptions<StoreServiceDbContext> options)
        : base(options) { }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }
}
