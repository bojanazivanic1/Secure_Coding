using InsecureCode.Models;
using Microsoft.EntityFrameworkCore;

namespace InsecureCode.Infrastructure
{
    public class InsecureDbContext : DbContext
    {
        public InsecureDbContext(DbContextOptions<InsecureDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=InsecureCode; Trusted_Connection=True; MultipleActiveResultSets=True; TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InsecureDbContext).Assembly);
        }
    }
}
