using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class CodeDto
    {
        [Required(ErrorMessage = "Code is required!"), MaxLength(6)]
        public string? Code { get; set; }
        [Required(ErrorMessage = "Email is required!"), MaxLength(100), EmailAddress]
        public string? Email { get; set; }
    }
}
