using Microsoft.EntityFrameworkCore;
using SecureCode.Models;
using System;

namespace SecureCode.Infrastructure
{
    public class SecureDbContext : DbContext
    {
        public SecureDbContext(DbContextOptions<SecureDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=SecureCode; Trusted_Connection=True; MultipleActiveResultSets=True; TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SecureDbContext).Assembly);
        }
    }
}
