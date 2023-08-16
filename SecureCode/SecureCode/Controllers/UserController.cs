using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureCode.DTO;
using SecureCode.Interfaces.IServices;
using SecureCode.Models;
using System.Data;

namespace SecureCode.Controllers
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
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new Exception("Bad ID. Logout and login.");

            await _userService.AddPostAsync(request, userId);

            return Ok();
        }

        [Authorize(Roles = "MODERATOR")]
        [HttpPost("verify-post")]
        public async Task<ActionResult> VerifyPostAsync(IdDto request)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new Exception("Bad ID. Logout and login.");

            await _userService.VerifyPostAsync(request);

            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-unverified-moderators")]
        public async Task<ActionResult> GetUnverifiedModeratorsAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new Exception("Bad ID. Logout and login.");

            List<GetUserDto> moderators = await _userService.GetUnverifiedModeratorsAsync();

            return Ok(moderators);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("verify-moderator")]
        public async Task<ActionResult> VerifyMderatorAsync(IdDto request)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new Exception("Bad ID. Logout and login.");

            await _userService.VerifyModeratorAsync(request);

            return Ok();
        }

        [Authorize(Roles = "CONTRIBUTOR, MODERATOR, ADMIN")]
        [HttpGet("get-verified-posts")]
        public async Task<ActionResult> GetVerifiedPostsAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new Exception("Bad ID. Logout and login.");

            List<GetPostDto> posts = await _userService.GetVerifiedPostsAsync();

            return Ok(posts);
        }

        [Authorize(Roles = "MODERATOR, ADMIN")]
        [HttpGet("get-all-posts")]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new Exception("Bad ID. Logout and login.");

            List<GetPostDto> posts = await _userService.GetAllPostsAsync();

            return Ok(posts);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete-user/{userId}")]
        public async Task<ActionResult> DeleteUserAsync(IdDto request)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new Exception("Bad ID. Logout and login.");

            await _userService.DeleteUserAsync(request);

            return Ok();
        }

    }
}
