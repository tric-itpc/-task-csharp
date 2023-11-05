using Microsoft.EntityFrameworkCore;
using MyAPI.Models;
using System.Numerics;

namespace MyAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "db_API");
        }
        public DbSet<Service> Services { get; set; }
    }
}
