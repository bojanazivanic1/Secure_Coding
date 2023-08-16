using SecureCode.Interfaces.IRepository;
using SecureCode.Models;

namespace SecureCode.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly SecureDbContext _dbContext;
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Post> Posts { get; }

        public UnitOfWork(SecureDbContext dbContext, IGenericRepository<User> users, IGenericRepository<Post> posts)
        {
            _dbContext = dbContext;
            Users = users;
            Posts = posts;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
