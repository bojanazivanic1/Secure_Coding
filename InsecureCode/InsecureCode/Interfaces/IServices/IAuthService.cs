using InsecureCode.DTO;

namespace InsecureCode.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto request);
        Task<string> LoginUserAsync(LoginUserDto request);
        Task<bool> ResetPasswordAsync(ResetPasswordDto request);
    }
}
