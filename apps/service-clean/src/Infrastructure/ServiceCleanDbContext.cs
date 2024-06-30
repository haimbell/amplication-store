using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceClean.Infrastructure.Models;

namespace ServiceClean.Infrastructure;

public class ServiceCleanDbContext : IdentityDbContext<IdentityUser>
{
    public ServiceCleanDbContext(DbContextOptions<ServiceCleanDbContext> options)
        : base(options) { }

    public DbSet<Customer> Customers { get; set; }
}
