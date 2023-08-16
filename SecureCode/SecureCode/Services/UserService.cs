using AutoMapper;
using SecureCode.DTO;
using SecureCode.Exceptions;
using SecureCode.Interfaces.IRepository;
using SecureCode.Interfaces.IServices;
using SecureCode.Models;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace SecureCode.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddPostAsync(AddPostDto addPostDto, int userId)
        {
            if ((await _unitOfWork.Users.Get(x => x.Id == userId)) == null)
                throw new BadRequestException("Error with id in token. Logout and login again");

            Post post = _mapper.Map<Post>(addPostDto);

            post.ContributorId = userId;

            await _unitOfWork.Posts.Add(post);
            await _unitOfWork.Posts.Save();
        }

        public async Task DeleteUserAsync(IdDto idDto, int userId)
        {
            if ((await _unitOfWork.Users.Get(x => x.Id == userId)) == null)
                throw new BadRequestException("Error with id in token. Logout and login again");

            User user = await _unitOfWork.Users.Get(x => x.Id == idDto.Id) ?? 
                throw new BadRequestException("This user doesn't exists.");

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.Save();
        }

        public async Task<List<GetPostDto>> GetAllPostsAsync(int userId)
        {
            if ((await _unitOfWork.Users.Get(x => x.Id == userId)) == null)
                throw new BadRequestException("Error with id in token. Logout and login again");

            IList<Post> posts = await _unitOfWork.Posts.GetAll();

            return _mapper.Map<List<GetPostDto>>(posts);
        }

        public async Task<List<GetUserDto>> GetUnverifiedModeratorsAsync(int userId)
        {
            if ((await _unitOfWork.Users.Get(x => x.Id == userId)) == null)
                throw new BadRequestException("Error with id in token. Logout and login again");

            IList<User> moderators = await _unitOfWork.Users.GetAll(x=>x.UserRole==EUserRole.MODERATOR && x.ModeratorVerifiedAt == null);

            return _mapper.Map<List<GetUserDto>>(moderators);
        }

        public async Task<List<GetPostDto>> GetVerifiedPostsAsync(int userId)
        {
            if ((await _unitOfWork.Users.Get(x => x.Id == userId)) == null)
                throw new BadRequestException("Error with id in token. Logout and login again");

            IList<Post> posts = await _unitOfWork.Posts.GetAll(x => x.MessageVerified);

            return _mapper.Map<List<GetPostDto>>(posts);
        }

        public async Task VerifyModeratorAsync(IdDto idDto, int userId)
        {
            if ((await _unitOfWork.Users.Get(x => x.Id == userId)) == null)
                throw new BadRequestException("Error with id in token. Logout and login again");

            User moderator = await _unitOfWork.Users.Get(x => x.Id == idDto.Id) ?? 
                throw new BadRequestException("Moderator with this ID doesn't exist.");

            moderator.ModeratorVerifiedAt = DateTime.UtcNow;

            _unitOfWork.Users.Update(moderator);
            await _unitOfWork.Users.Save();
        }

        public async Task VerifyPostAsync(IdDto idDto, int userId)
        {
            if ((await _unitOfWork.Users.Get(x => x.Id == userId)) == null)
                throw new BadRequestException("Error with id in token. Logout and login again");

            Post post = await _unitOfWork.Posts.Get(x => x.Id == idDto.Id) ??
                throw new BadRequestException("Post with this ID doesn't exist.");

            post.MessageVerified = true;

            _unitOfWork.Posts.Update(post);
            await _unitOfWork.Posts.Save();
        }
    }
}