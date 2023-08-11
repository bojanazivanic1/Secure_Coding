using System.ComponentModel.DataAnnotations;

namespace SecureCode.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!"), MaxLength(100)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Password is required!"), MaxLength(100), RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Email is required!"), MaxLength(100), EmailAddress]
        public string? Email { get; set; }
        public string? Salt { get; set; } = null;
        public string? VerificatonCode { get; set; } = null;
        public DateTime? VerifiedAt { get; set; } = null;
        public string? PasswordResetCode { get; set; } = null;
        public DateTime? ResetCodeExpires { get; set; } = null;
        public string? LoginCode { get; set; } = null;
        public DateTime? LoginCodeExpires { get; set; } = null;
    }
}
