using Microsoft.EntityFrameworkCore;
using testTaskServiceAPI.Models.Domain;

namespace testTaskServiceAPI.Models
{
    public class Context : DbContext
    {
        public Context() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ServiceAPI");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceDomain>()
                .HasIndex(x => x.Name)
                .IsUnique();
        }

        public DbSet<ServiceDomain> Services { get; set; }
        public DbSet<ServiceHistoryDomain> ServiceHistory { get; set; }
    }
}
