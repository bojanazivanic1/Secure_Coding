using InsecureCode.DTO;

namespace InsecureCode.Interfaces.IServices
{
    public interface IUserService
    {
        Task AddPostAsync(AddPostDto addPostDto, int userId);
        Task DeleteUserAsync(IdDto idDto, int userId);
        Task<List<GetPostDto>> GetAllPostsAsync();
        Task<List<GetUserDto>> GetUnverifiedModeratorsAsync();
        Task<List<GetPostDto>> GetVerifiedPostsAsync();
        Task VerifyModeratorAsync(IdDto idDto);
        Task VerifyPostAsync(IdDto idDto, int userId);
    }
}
