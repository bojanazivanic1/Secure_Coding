using InsecureCode.DTO;
using InsecureCode.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsecureCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterUserDto request)
        {
            await authService.RegisterUserAsync(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginUserDto request)
        {
            string token = await authService.LoginUserAsync(request);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPasswordAsync(ResetPasswordDto request)
        {
            await authService.ResetPasswordAsync(request);
            return Ok();
        }
    }
}
