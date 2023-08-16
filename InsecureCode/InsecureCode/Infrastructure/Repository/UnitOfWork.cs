using InsecureCode.Interfaces.IRepository;
using InsecureCode.Models;

namespace InsecureCode.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly InsecureDbContext _dbContext;
        public IGenericRepository<User> Users { get; }

        public UnitOfWork(InsecureDbContext dbContext, IGenericRepository<User> users)
        {
            _dbContext = dbContext;
            Users = users;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
