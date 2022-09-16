using microservice.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservice.infrastructure.Setting;

public class MicroServiceContext : DbContext
{
    public MicroServiceContext()
    {

    }

    public MicroServiceContext(DbContextOptions<MicroServiceContext> options)
    : base(options)
    {
    }

    public virtual DbSet<Product>? Producto { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>();
    }
}
