using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class EmailDto
    {
        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }
    }
}
