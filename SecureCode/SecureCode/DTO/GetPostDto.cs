using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class GetPostDto
    {
        public int Id { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Message is too long.")]
        public string? Message { get; set; }
        public bool MessageVerified { get; set; } = false;

        [Required]
        public int ContributorId { get; set; }
    }
}
