using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class IdDto
    {
        [Required]
        public int Id { get; set; }
    }
}
