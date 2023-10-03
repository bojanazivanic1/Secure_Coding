using SecureCode.DTO;
using SecureCode.Models;
using SecureCode.Services.Helpers;

namespace SecureCode.Interfaces.IServices
{
    public interface IAuthService
    {
        Task RegisterUserAsync(RegisterUserDto request);
        Task<TotpSetup> ConfirmEmailAsync(CodeDto enteredCode);
        Task ResetPasswordRequestAsync(EmailDto request);
        Task ResetPasswordConfirmAsync(ResetPasswordDto request);
        Task LoginUserAsync(LoginUserDto request);
        Task<string> LoginConfirmAsync(CodeDto codeDto);
        Task ConfirmTotpAsync(CodeDto codeDto);
    }
}
