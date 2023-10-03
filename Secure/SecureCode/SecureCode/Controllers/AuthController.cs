using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureCode.DTO;
using SecureCode.Interfaces.IServices;
using SecureCode.Models;

namespace SecureCode.Controllers
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
        [HttpPost("confirm-email")]
        public async Task<ActionResult> ConfirmEmailAsync(CodeDto request)
        {
            var totp = await authService.ConfirmEmailAsync(request);
            return Ok(totp);
        }

        [AllowAnonymous]
        [HttpPost("confirm-totp")]
        public async Task<ActionResult> ConfirmTotpAsync(CodeDto request)
        {
            await authService.ConfirmTotpAsync(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginUserDto request)
        {
            await authService.LoginUserAsync(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("confirm-login")]
        public async Task<ActionResult> LoginConfirmAsync(CodeDto request)
        {
            string token = await authService.LoginConfirmAsync(request);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPasswordRequestAsync(EmailDto request)
        {
            await authService.ResetPasswordRequestAsync(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("confirm-password")]
        public async Task<ActionResult> ResetPasswordConfirmAsync(ResetPasswordDto request)
        {
            await authService.ResetPasswordConfirmAsync(request);
            return Ok();
        }
    }
}
