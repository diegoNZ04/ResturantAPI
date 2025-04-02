using Microsoft.EntityFrameworkCore;

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
                .HasOne<Reserve>()
                .WithOne()
                .HasForeignKey<Reserve>(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<Table>()
                .HasOne<Reserve>()
                .WithOne()
                .HasForeignKey<Reserve>(e => e.TableId)
                .IsRequired();
        }
    }
}