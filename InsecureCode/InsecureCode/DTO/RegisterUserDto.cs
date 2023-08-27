using InsecureCode.Models;
using System.ComponentModel.DataAnnotations;

namespace InsecureCode.DTO
{
    public class RegisterUserDto
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }
        public EUserRole? UserRole { get; set; }
    }
}
