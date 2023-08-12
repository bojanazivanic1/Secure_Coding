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
        public async Task<ActionResult> RegisterAsync([FromForm] RegisterUserDto request)
        {
            await authService.RegisterUserAsync(request);
            return Ok(request);
        }

        [AllowAnonymous]
        [HttpPost("confirm-email")]
        public async Task<ActionResult> ConfirmEmailAsync(CodeDto request)
        {
            await authService.ConfirmEmailAsync(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginUserDto request)
        {
            TotpSetup totp = await authService.LoginUserAsync(request);
            return Ok(totp);
        }

        [AllowAnonymous]
        [HttpPost("confirm-login")]
        public async Task<ActionResult> LoginConfirmAsync(CodeDto request)
        {
            string token = await authService.LoginConfirmAsync(request);
            return Ok(token);
        }

        [Authorize]
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPasswordRequestAsync(EmailDto request)
        {
            TotpSetup totp = await authService.ResetPasswordRequestAsync(request);
            return Ok(totp);
        }

        [Authorize]
        [HttpPost("confirm-password")]
        public async Task<ActionResult> ResetPasswordConfirmAsync(ResetPasswordDto request)
        {
            await authService.ResetPasswordConfirmAsync(request);
            return Ok();
        }
    }
}
