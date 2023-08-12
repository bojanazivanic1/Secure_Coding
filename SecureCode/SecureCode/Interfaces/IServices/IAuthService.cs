using SecureCode.DTO;
using SecureCode.Models;
using SecureCode.Services.Helpers;

namespace SecureCode.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto request);
        Task<bool> ConfirmEmailAsync(CodeDto enteredCode);
        Task<TotpSetup> ResetPasswordRequestAsync(EmailDto request);
        Task<bool> ResetPasswordConfirmAsync(ResetPasswordDto request);
        Task<TotpSetup> LoginUserAsync(LoginUserDto request);
        Task<string> LoginConfirmAsync(CodeDto codeDto);
    }
}
