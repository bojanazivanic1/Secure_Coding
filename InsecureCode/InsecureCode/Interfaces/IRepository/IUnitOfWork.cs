using InsecureCode.Models;

namespace InsecureCode.Interfaces.IRepository
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Post> Posts { get; }

        Task Save();
    }
}
