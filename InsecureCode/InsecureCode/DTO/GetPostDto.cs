using System.ComponentModel.DataAnnotations;

namespace InsecureCode.DTO
{
    public class GetPostDto
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public bool MessageVerified { get; set; }
        public int ContributorId { get; set; }
    }
}
