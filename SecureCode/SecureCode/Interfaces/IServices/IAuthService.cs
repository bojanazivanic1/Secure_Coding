using SecureCode.DTO;
using SecureCode.Models;

namespace SecureCode.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto request);
        Task<bool> ConfirmEmailAsync(CodeDto enteredCode);
        Task<bool> ResetPasswordRequestAsync(EmailDto request);
        Task<bool> ResetPasswordConfirmAsync(ResetPasswordDto request);
        Task<bool> LoginUserAsync(LoginUserDto request);
        Task<string> LoginConfirmAsync(CodeDto codeDto);
    }
}
