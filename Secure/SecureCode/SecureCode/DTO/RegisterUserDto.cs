using SecureCode.Models;
using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Name is required!"), StringLength(100, MinimumLength = 5, ErrorMessage = "Name length must be between 5 and 100 characters.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [MaxLength(100, ErrorMessage = "The password must not exceed 100 characters.")]
        [MinLength(8, ErrorMessage = "The password must be at least 8 characters long.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
         ErrorMessage = "The password should contain a lowercase and uppercase letter, a number, and a special character.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required!"), Compare("Password", ErrorMessage = "Passwords must match!")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email is required!"), MaxLength(100), EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Role is required!"), EnumDataType(typeof(EUserRole), ErrorMessage = "Invalid user role.")]
        public EUserRole UserRole { get; set; }
    }
}
