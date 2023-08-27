using Microsoft.EntityFrameworkCore;
using SecureCode.Exceptions;
using SecureCode.Models;
using System;

namespace SecureCode.Infrastructure
{
    public class SecureDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SecureDbContext(DbContextOptions<SecureDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("AppDbContext") ??
                throw new InternalServerErrorException("Cannot connect to the database.");

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SecureDbContext).Assembly);
        }
    }
}
