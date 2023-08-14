using SecureCode.DTO;
using SecureCode.Models;

namespace SecureCode.Interfaces.IProviders
{
    public interface IUserDbProvider
    {
        Task<bool> AddUserAsync(User newUser);
        Task<User?> FindUserByEmailAsync(string email);
        Task<User?> FindUserByIdAsync(int id);
        Task<bool> UpdateUser(User user);
        Task<bool> VerifyModeratorAsync(int userId);
        Task<List<User>> GetUnverifiedModeratorsAsync();
        Task<bool> SaveChanges();
        Task<bool> DeleteUserAsync(int userId);
    }
}
