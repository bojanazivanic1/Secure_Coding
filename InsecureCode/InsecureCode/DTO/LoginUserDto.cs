using System.ComponentModel.DataAnnotations;

namespace InsecureCode.DTO
{
    public class LoginUserDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
