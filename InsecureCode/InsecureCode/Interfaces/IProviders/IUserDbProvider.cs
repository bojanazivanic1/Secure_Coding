using InsecureCode.Models;

namespace InsecureCode.Interfaces.IProviders
{
    public interface IUserDbProvider
    {
        Task<bool> AddUserAsync(User newUser);
        Task<User?> FindUserByEmailAsync(string email);
        Task<User?> FindUserByIdAsync(int id);
        Task<bool> SaveChanges();
        Task<bool> UpdateUser(User? user);
    }
}
