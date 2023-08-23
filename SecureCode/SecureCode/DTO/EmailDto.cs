using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class EmailDto
    {
        [Required(ErrorMessage = "Email is required!"), MaxLength(100), EmailAddress]
        public string? Email { get; set; }
    }
}
