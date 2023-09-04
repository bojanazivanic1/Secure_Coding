using InsecureCode.DTO;

namespace InsecureCode.Interfaces.IServices
{
    public interface IAuthService
    {
        Task RegisterUserAsync(RegisterUserDto request);
        Task<string> LoginUserAsync(LoginUserDto request);
        Task ResetPasswordAsync(ResetPasswordDto request);
    }
}
