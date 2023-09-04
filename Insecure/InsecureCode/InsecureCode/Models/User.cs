using System.ComponentModel.DataAnnotations;

namespace InsecureCode.Models
{
    public enum EUserRole { ADMIN, MODERATOR, CONTRIBUTOR }
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        [Required]
        public EUserRole? UserRole { get; set; }
        public DateTime? ModeratorVerifiedAt { get; set; } = null;
        public List<Post>? Posts { get; set; }
    }
}
