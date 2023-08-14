using AutoMapper;
using SecureCode.DTO;
using SecureCode.Interfaces.IProviders;
using SecureCode.Interfaces.IServices;
using SecureCode.Models;

namespace SecureCode.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDbProvider? _userDbProvider;
        private readonly IPostDbProvider? _postDbProvider;
        private readonly IMapper _mapper;

        public UserService(IUserDbProvider? userDbProvider, 
                            IPostDbProvider? postDbProvider, 
                            IMapper mapper)
        {
            _userDbProvider = userDbProvider;
            _postDbProvider = postDbProvider;
            _mapper = mapper;
        }

        public async Task AddPostAsync(AddPostDto addPostDto, int userId)
        {
            Post post = _mapper.Map<Post>(addPostDto);

            post.ContributorId = userId;

            await _postDbProvider.AddPostAsync(post);
        }

        public async Task DeleteUserAsync(IdDto idDto)
        {
            await _userDbProvider.DeleteUserAsync(idDto.Id);
        }

        public async Task<List<GetPostDto>> GetAllPostsAsync()
        {
            List<Post> posts = await _postDbProvider.GetAllPostsAsync();

            return _mapper.Map<List<GetPostDto>>(posts);
        }

        public async Task<List<GetUserDto>> GetUnverifiedModeratorsAsync()
        {
            List<User> moderators = await _userDbProvider.GetUnverifiedModeratorsAsync();

            return _mapper.Map<List<GetUserDto>>(moderators);
        }

        public async Task<List<GetPostDto>> GetVerifiedPostsAsync()
        {
            List<Post> posts = await _postDbProvider.GetVerifiedPostsAsync();

            return _mapper.Map<List<GetPostDto>>(posts);
        }

        public async Task VerifyModeratorAsync(IdDto idDto)
        {
            await _userDbProvider.VerifyModeratorAsync(idDto.Id);
        }

        public async Task VerifyPostAsync(IdDto idDto)
        {
            await _postDbProvider.VerifyPostAsync(idDto.Id);
        }
    }
}
