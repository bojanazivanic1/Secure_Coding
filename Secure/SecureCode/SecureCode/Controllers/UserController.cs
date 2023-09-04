using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureCode.DTO;
using SecureCode.Exceptions;
using SecureCode.Interfaces.IServices;
using SecureCode.Models;
using System.Data;
using System.Security.Claims;

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
                throw new BadRequestException("Bad ID. Logout and login.");

            await _userService.AddPostAsync(request, userId);

            return Ok();
        }

        [Authorize(Roles = "MODERATOR, ADMIN")]
        [HttpPost("verify-post")]
        public async Task<ActionResult> VerifyPostAsync(IdDto request)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new BadRequestException("Bad ID. Logout and login.");

            await _userService.VerifyPostAsync(request, userId);

            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-unverified-moderators")]
        public async Task<ActionResult> GetUnverifiedModeratorsAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new BadRequestException("Bad ID. Logout and login.");

            List<GetUserDto> moderators = await _userService.GetUnverifiedModeratorsAsync(userId);

            return Ok(moderators);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("verify-moderator")]
        public async Task<ActionResult> VerifyMderatorAsync(IdDto request)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new BadRequestException("Bad ID. Logout and login.");

            await _userService.VerifyModeratorAsync(request, userId);

            return Ok();
        }

        [Authorize(Roles = "CONTRIBUTOR, MODERATOR, ADMIN")]
        [HttpGet("get-verified-posts")]
        public async Task<ActionResult> GetVerifiedPostsAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new BadRequestException("Bad ID. Logout and login.");

            List<GetPostDto> posts = await _userService.GetVerifiedPostsAsync(userId);

            return Ok(posts);
        }

        [Authorize(Roles = "MODERATOR, ADMIN")]
        [HttpGet("get-all-posts")]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new BadRequestException("Bad ID. Logout and login.");

            List<GetPostDto> posts = await _userService.GetAllPostsAsync(userId);

            return Ok(posts);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new BadRequestException("Bad ID. Logout and login.");

            await _userService.DeleteUserAsync(id, userId);

            return Ok();
        }


    }
}
