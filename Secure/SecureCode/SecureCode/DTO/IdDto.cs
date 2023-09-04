using System.ComponentModel.DataAnnotations;

namespace SecureCode.DTO
{
    public class IdDto
    {
        [Required(ErrorMessage = "Id is required!"), RegularExpression(@"^\d+$", ErrorMessage = "Value must be an integer.")]
        public int Id { get; set; }
    }
}
