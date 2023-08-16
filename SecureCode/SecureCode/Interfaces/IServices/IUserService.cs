using SecureCode.DTO;
using SecureCode.Models;

namespace SecureCode.Interfaces.IServices
{
    public interface IUserService
    {
        Task AddPostAsync(AddPostDto addPostDto, int userId);
        Task DeleteUserAsync(IdDto idDto, int userId);
        Task<List<GetPostDto>> GetAllPostsAsync(int userId);
        Task<List<GetUserDto>> GetUnverifiedModeratorsAsync(int userId);
        Task<List<GetPostDto>> GetVerifiedPostsAsync(int userId);
        Task VerifyModeratorAsync(IdDto idDto, int userId);
        Task VerifyPostAsync(IdDto idDto, int userId);
    }
}
