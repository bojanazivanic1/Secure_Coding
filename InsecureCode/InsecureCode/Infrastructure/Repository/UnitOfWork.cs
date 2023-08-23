using InsecureCode.Interfaces.IRepository;
using InsecureCode.Models;

namespace InsecureCode.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly InsecureDbContext _dbContext;
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Post> Posts { get; }

        public UnitOfWork(InsecureDbContext dbContext, IGenericRepository<User> users, IGenericRepository<Post> posts)
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
