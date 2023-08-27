using InsecureCode.Models;

namespace InsecureCode.Interfaces.IProviders
{
    public interface IUserDbProvider
    {
        Task<bool> AddUserAsync(User newUser);
        Task<User?> FindUserByEmailAsync(string email);
        Task<User?> FindUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> VerifyModeratorAsync(int userId);
        Task<List<User>> GetUnverifiedModeratorsAsync();
        Task<bool> DeleteUserAsync(int userId);
    }
}
