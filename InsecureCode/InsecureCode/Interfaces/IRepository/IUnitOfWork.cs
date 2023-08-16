using InsecureCode.Models;

namespace InsecureCode.Interfaces.IRepository
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }

        Task Save();
    }
}
