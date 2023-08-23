using SecureCode.Models;
using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class GetUserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!"), StringLength(100, MinimumLength = 5, ErrorMessage = "Name length must be between 5 and 100 characters.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is required!"), MaxLength(100), EmailAddress]
        public string? Email { get; set; }
        public DateTime? VerifiedAt { get; set; } = null;
        [Required(ErrorMessage = "User Role is required!"), EnumDataType(typeof(EUserRole), ErrorMessage = "Invalid user role.")]
        public EUserRole? UserRole { get; set; }
        public DateTime? ModeratorVerifiedAt { get; set; } = null;
        public List<Post>? Posts { get; set; }
    }
}
