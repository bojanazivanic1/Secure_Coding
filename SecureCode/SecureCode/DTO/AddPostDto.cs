using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class AddPostDto
    {
        [Required, MaxLength(200, ErrorMessage = "Message is too long.")]
        public string? Message { get; set; }
    }
}
