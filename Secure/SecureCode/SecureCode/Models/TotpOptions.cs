using System.ComponentModel.DataAnnotations;

namespace SecureCode.Models
{
    public class TotpOptions
    {
        public const string TOTP = "TOTP";

        [Required, MinLength(16)]
        public string Secret { get; set; } = null!;
    }
}
