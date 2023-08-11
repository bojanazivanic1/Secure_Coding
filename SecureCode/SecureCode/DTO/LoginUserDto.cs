using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Email is required!"), MaxLength(100), EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required!"), MaxLength(100)]
        public string? Password { get; set; }
    }
}
