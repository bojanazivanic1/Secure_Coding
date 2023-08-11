using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class EmailDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
