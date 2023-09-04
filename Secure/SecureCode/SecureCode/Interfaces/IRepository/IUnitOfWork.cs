using SecureCode.Models;

namespace SecureCode.Interfaces.IRepository
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get;}
        IGenericRepository<Post> Posts { get;}

        Task Save();
    }
}
