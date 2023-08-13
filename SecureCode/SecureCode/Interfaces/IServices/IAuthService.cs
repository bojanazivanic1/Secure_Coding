using SecureCode.DTO;
using SecureCode.Models;
using SecureCode.Services.Helpers;

namespace SecureCode.Interfaces.IServices
{
    public interface IAuthService
    {
        Task RegisterUserAsync(RegisterUserDto request);
        Task ConfirmEmailAsync(CodeDto enteredCode);
        Task<TotpSetup> ResetPasswordRequestAsync(EmailDto request);
        Task ResetPasswordConfirmAsync(ResetPasswordDto request);
        Task<TotpSetup> LoginUserAsync(LoginUserDto request);
        Task<string> LoginConfirmAsync(CodeDto codeDto);
    }
}
