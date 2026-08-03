namespace ECommerce.Infrastructure.Contexts;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
public class ECommerceDbContext : IdentityDbContext<User, Role, string>
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
    {

    }
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDbContext).Assembly);
    }
}

