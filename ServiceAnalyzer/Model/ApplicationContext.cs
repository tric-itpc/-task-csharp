using Microsoft.EntityFrameworkCore;

namespace ServiceAnalyzer.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ServiceInfo> Infos { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}