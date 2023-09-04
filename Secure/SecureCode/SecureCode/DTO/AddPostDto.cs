using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class AddPostDto
    {
        [Required, MaxLength(1000, ErrorMessage = "Message is too long.")]
        public string? Message { get; set; }
    }
}
