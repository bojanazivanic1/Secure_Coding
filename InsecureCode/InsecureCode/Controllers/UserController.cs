﻿using InsecureCode.DTO;
using InsecureCode.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Authorize]
        [HttpPost("add-post")]
        public async Task<ActionResult> AddPostAsync(AddPostDto request)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int userId))
                throw new Exception("Bad ID. Logout and login.");

            await _userService.AddPostAsync(request, userId);

            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("verify-post")]
        public async Task<ActionResult> VerifyPostAsync(IdDto request)
        {
            await _userService.VerifyPostAsync(request);

            return Ok();
        }

        [Authorize]
        [HttpGet("get-unverified-moderators")]
        public async Task<ActionResult> GetUnverifiedModeratorsAsync()
        {
            List<GetUserDto> moderators = await _userService.GetUnverifiedModeratorsAsync();

            return Ok(moderators);
        }

        [HttpPost("verify-moderator")]
        public async Task<ActionResult> VerifyMderatorAsync(IdDto request)
        {
            await _userService.VerifyModeratorAsync(request);

            return Ok();
        }

        [Authorize(Roles = "CONTRIBUTOR, ADMIN")]
        [HttpGet("get-verified-posts")]
        public async Task<ActionResult> GetVerifiedPostsAsync()
        {
            List<GetPostDto> posts = await _userService.GetVerifiedPostsAsync();

            return Ok(posts);
        }

        [Authorize(Roles = "MODERATOR")]
        [HttpGet("get-all-posts")]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            List<GetPostDto> posts = await _userService.GetAllPostsAsync();

            return Ok(posts);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete-user/{userId}")]
        public async Task<ActionResult> DeleteUserAsync(IdDto request)
        {
            await _userService.DeleteUserAsync(request);

            return Ok();
        }
    }
}