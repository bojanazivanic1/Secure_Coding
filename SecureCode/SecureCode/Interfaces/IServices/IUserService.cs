using SecureCode.DTO;
using SecureCode.Models;

namespace SecureCode.Interfaces.IServices
{
    public interface IUserService
    {
        Task AddPostAsync(AddPostDto addPostDto, int userId);
        Task DeleteUserAsync(IdDto idDto);
        Task<List<GetPostDto>> GetAllPostsAsync();
        Task<List<GetUserDto>> GetUnverifiedModeratorsAsync();
        Task<List<GetPostDto>> GetVerifiedPostsAsync();
        Task VerifyModeratorAsync(IdDto idDto);
        Task VerifyPostAsync(IdDto idDto);
    }
}
