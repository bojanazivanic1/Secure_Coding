using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Name is required!"), MaxLength(100)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Password is required!"), MaxLength(100), RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required!"), Compare("Password", ErrorMessage = "Passwords must match!")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email is required!"), MaxLength(100), EmailAddress]
        public string? Email { get; set; }
    }
}
