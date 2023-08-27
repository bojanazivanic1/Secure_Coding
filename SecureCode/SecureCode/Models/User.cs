using System.ComponentModel.DataAnnotations;

namespace SecureCode.Models
{
    public enum EUserRole { ADMIN, MODERATOR, CONTRIBUTOR }
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!"), StringLength(100, MinimumLength = 5, ErrorMessage = "Name length must be between 5 and 100 characters.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Password is required!"), MaxLength(100), RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Email is required!"), MaxLength(100), EmailAddress]
        public string? Email { get; set; }
        public string? Salt { get; set; } = null;
        public string VerificatonCode { get; set; } = null!;
        public DateTime VerifiedAt { get; set; } 
        public string TotpSecretKey { get; set; } = null!;
        [Required(ErrorMessage = "Role is required!"), EnumDataType(typeof(EUserRole), ErrorMessage = "Invalid user role.")]
        public EUserRole? UserRole { get; set; }
        public DateTime ModeratorVerifiedAt { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
