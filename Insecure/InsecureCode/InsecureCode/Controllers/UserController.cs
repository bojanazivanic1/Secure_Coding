using InsecureCode.DTO;
using InsecureCode.Interfaces.IServices;
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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "CONTRIBUTOR")]
        [HttpPost("add-post")]
        public async Task<ActionResult> AddPostAsync(AddPostDto request)
        {
            int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId);

            await _userService.AddPostAsync(request, userId);

            return Ok();
        }

        [Authorize]
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

        [AllowAnonymous]   
        [HttpGet("get-verified-posts")]
        public async Task<ActionResult> GetVerifiedPostsAsync()
        {
            List<GetPostDto> posts = await _userService.GetVerifiedPostsAsync();

            return Ok(posts);
        }

        [HttpGet("get-all-posts")]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            List<GetPostDto> posts = await _userService.GetAllPostsAsync();

            return Ok(posts);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            await _userService.DeleteUserAsync(id);

            return Ok();
        }
    }
}
