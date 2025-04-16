using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;

namespace Restaurant.Infra.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Table> Tables { get; set; } = null!;
    public DbSet<Reserve> Reserves { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Reserves)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .IsRequired();

        modelBuilder.Entity<Reserve>()
             .HasOne(r => r.Table)
             .WithMany(t => t.Reserves)
             .HasForeignKey(r => r.TableId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}