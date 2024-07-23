using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreService.Infrastructure.Models;

namespace StoreService.Infrastructure;

public class StoreServiceDbContext : IdentityDbContext<IdentityUser>
{
    public StoreServiceDbContext(DbContextOptions<StoreServiceDbContext> options)
        : base(options) { }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<ItemDbModel> Items { get; set; }

    public DbSet<OrderDbModel> Orders { get; set; }

    public DbSet<OrderItemDbModel> OrderItems { get; set; }
}
