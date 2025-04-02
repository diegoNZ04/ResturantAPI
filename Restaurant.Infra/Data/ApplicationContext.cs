using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;

namespace RestaurantAPI.Data
{
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
                .HasMany(r => r.Tables)
                .WithOne(t => t.Reserve)
                .HasForeignKey(t => t.ReserveId)
                .IsRequired();
        }
    }
}