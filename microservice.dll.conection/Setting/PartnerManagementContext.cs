using microservice.dll.conection.Entities;
using microservice.dll.conection.Extensions;
using Microsoft.EntityFrameworkCore;

namespace microservice.dll.conection.Setting;

public partial class PartnerManagementContext : DbContext
{
    public PartnerManagementContext()
    {
    }

    public PartnerManagementContext(DbContextOptions<PartnerManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Partner> Partners { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(Connection.GetConnection());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Partner>(entity =>
        {
            entity.Property(e => e.PartnerDatabase)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.PartnerDescription)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.PasswordBd)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.ServerBd)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.UserBd)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

