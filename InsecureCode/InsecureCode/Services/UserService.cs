using AutoMapper;
using InsecureCode.DTO;
using InsecureCode.Exceptions;
using InsecureCode.Interfaces.IProviders;
using InsecureCode.Interfaces.IServices;
using InsecureCode.Models;

namespace InsecureCode.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserDbProvider _userDbProvider;
        private readonly IPostDbProvider _postDbProvider;

        public UserService(IMapper mapper, IUserDbProvider userDbProvider, IPostDbProvider postDbProvider)
        {
            _mapper = mapper;
            _userDbProvider = userDbProvider;
            _postDbProvider = postDbProvider;
        }

        public async Task AddPostAsync(AddPostDto addPostDto, int userId)
        {
            Post post = _mapper.Map<Post>(addPostDto);

            post.ContributorId = userId;

            await _postDbProvider.AddPostAsync(post);
        }

        public async Task DeleteUserAsync(IdDto idDto, int userId)
        {
            User user = await _userDbProvider.FindUserByIdAsync(idDto.Id) ??
                throw new BadRequestException("This user doesn't exists.");

            if (user.Id == userId)
                throw new BadRequestException("You cannot delete your own user account.");

            await _userDbProvider.DeleteUserAsync(user.Id);
        }

        public async Task<List<GetPostDto>> GetAllPostsAsync()
        {
            IList<Post> posts = await _postDbProvider.GetAllPostsAsync();

            return _mapper.Map<List<GetPostDto>>(posts);
        }

        public async Task<List<GetUserDto>> GetUnverifiedModeratorsAsync()
        {
            IList<User> moderators = await _userDbProvider.GetUnverifiedModeratorsAsync();

            return _mapper.Map<List<GetUserDto>>(moderators);
        }

        public async Task<List<GetPostDto>> GetVerifiedPostsAsync()
        {
            IList<Post> posts = await _postDbProvider.GetVerifiedPostsAsync();

            return _mapper.Map<List<GetPostDto>>(posts);
        }

        public async Task VerifyModeratorAsync(IdDto idDto)
        {
            User moderator = await _userDbProvider.FindUserByIdAsync(idDto.Id) ??
                throw new BadRequestException("Moderator with this ID doesn't exist.");

            moderator.ModeratorVerifiedAt = DateTime.UtcNow;

            await _userDbProvider.UpdateUserAsync(moderator);
        }

        public async Task VerifyPostAsync(IdDto idDto, int userId)
        {
            Post post = await _postDbProvider.FindPostByIdAsync(idDto.Id) ??
                throw new BadRequestException("Post with this ID doesn't exist.");

            User user = await _userDbProvider.FindUserByIdAsync(userId) ??
                throw new BadRequestException("User with this ID doesn't exist.");

            if (user.UserRole == EUserRole.MODERATOR && user.ModeratorVerifiedAt == null)
                throw new BadRequestException("You need to be verified to verify a post.");

            post.MessageVerified = true;

            await _postDbProvider.VerifyPostAsync(post.Id);
        }
    }
}
