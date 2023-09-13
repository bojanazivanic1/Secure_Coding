using InsecureCode.DTO;
using InsecureCode.Interfaces.IServices;
using InsecureCode.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InsecureCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJokeService _jokeService;

        public UserController(IUserService userService, IJokeService jokeService)
        {
            _userService = userService;
            _jokeService = jokeService;
        }

        [Authorize(Roles = "CONTRIBUTOR")]
        [HttpPost("add-post")]
        public async Task<ActionResult> AddPostAsync(AddPostDto request)
        {
            int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId);

            await _userService.AddPostAsync(request, userId);

            return Ok();
        }

        [Authorize(Roles = "CONTRIBUTOR, MODERATOR, ADMIN")]
        [HttpPost("verify-post")]
        public async Task<ActionResult> VerifyPostAsync(IdDto request)
        {
            int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId);
            await _userService.VerifyPostAsync(request, userId);
            return Ok();
        }

        [Authorize(Roles = "ADMIN")] 
        [HttpGet("get-unverified-moderators")]
        public async Task<ActionResult> GetUnverifiedModeratorsAsync()
        {
            List<GetUserDto> moderators = await _userService.GetUnverifiedModeratorsAsync();

            return Ok(moderators);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("verify-moderator")]
        public async Task<ActionResult> VerifyMderatorAsync(IdDto request)
        {
            await _userService.VerifyModeratorAsync(request);
            return Ok();
        }

        [HttpGet("get-all-posts")]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            List<GetPostDto> posts = await _userService.GetAllPostsAsync();
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("get-verified-posts")]
        public async Task<ActionResult> GetVerifiedPostsAsync()
        {
            List<GetPostDto> posts = await _userService.GetVerifiedPostsAsync();

            return Ok(posts);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId);

            await _userService.DeleteUserAsync(id, userId);

            return Ok();
        }

        [HttpGet("get-joke")]
        public async Task<ActionResult> GetJokeAsync()
        {
            var joke = await _jokeService.GetRandomJokeAsync();
            return Ok(joke);
        }
    }
}
